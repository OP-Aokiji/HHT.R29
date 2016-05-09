using System;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using Framework.Common.Exception;

/*
 * 
 * FTP Reply Codes

[for complete info look at RFC 959]
--------------------------------------------------------------------------------

110 Restart marker reply.
120 Service ready in nnn minutes.
125 Data connection already open; transfer starting.
150 File status okay; about to open data connection.

200 Command okay.
202 Command not implemented, superfluous at this site.
211 System status, or system help reply.
212 Directory status.
213 File status.
214 Help message.
215 NAME system type.
220 Service ready for new user.
221 Service closing control connection.
225 Data connection open; no transfer in progress.
226 Closing data connection.
227 Entering Passive Mode (h1,h2,h3,h4,p1,p2).
230 User logged in, proceed.
250 Requested file action okay, completed.
257 "PATHNAME" created.

331 User name okay, need password.
332 Need account for login.
350 Requested file action pending further information.

421 Service not available, closing control connection.
425 Can't open data connection.
426 Connection closed; transfer aborted.
450 Requested file action not taken.
451 Requested action aborted: local error in processing.
452 Requested action not taken.

500 Syntax error, command unrecognized.
501 Syntax error in parameters or arguments.
502 Command not implemented.
503 Bad sequence of commands.
504 Command not implemented for that parameter.
530 Not logged in.
532 Need account for storing files.
550 Requested action not taken.
551 Requested action aborted: page type unknown.
552 Requested file action aborted.
553 Requested action not taken.

 * */

namespace Framework.Common.Utility
{
    public class FtpClient
    {
        private static int BUFFER_SIZE = 512;
        private static Encoding ASCII = Encoding.ASCII;

        private bool verboseDebugging = true; //false;

        // defaults
        private string server = "localhost";
        private string remotePath = ".";
        private string username = "anonymous";
        private string password = "anonymous@anonymous.net";
        private string message = null;
        private string result = null;
        private int m_iServerOS;

        private int port = 21;
        private int bytes = 0;
        private int resultCode = 0;

        private bool loggedin = false;
        private bool binMode = false;
        //private bool connectionMode = false;

        private Byte[] buffer = new Byte[BUFFER_SIZE];
        private Byte[] receiveBuffer = new Byte[BUFFER_SIZE * 2];
        private Socket clientSocket = null;

        private int timeoutSeconds = 10;
        private decimal nSendFileSize = 0m;

        /// <summary>
        /// Default contructor
        /// </summary>
        public FtpClient()
        {
        }

        public FtpClient(string server, string username, string password)
        {
            this.server = server;
            this.username = username;
            this.password = password;
        }

        public FtpClient(string server, string username, string password, int timeoutSeconds, int port)
        {
            this.server = server;
            this.username = username;
            this.password = password;
            this.timeoutSeconds = timeoutSeconds;
            this.port = port;
        }

        /// <summary>
        /// SendFile Size
        /// </summary>
        public decimal SendFileSize
        {
            get
            {
                return nSendFileSize;
            }
        }

        /// <summary>
        /// Display Server OS
        /// </summary>
        public int ServerOS
        {
            get { return m_iServerOS; }
        }

        /// Display all communications to the debug log
        public bool VerboseDebugging
        {
            get { return this.verboseDebugging; }
            set { this.verboseDebugging = value; }
        }

        /// Remote server port. Typically TCP 21		
        public int Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        /// Timeout waiting for a response from server, in seconds.		
        public int Timeout
        {
            get { return this.timeoutSeconds; }
            set { this.timeoutSeconds = value; }
        }

        /// Gets and Sets the name of the FTP server.
        public string Server
        {
            get { return this.server; }
            set { this.server = value; }
        }

        /// Gets and Sets the port number.
        public int RemotePort
        {
            get { return this.port; }
            set { this.port = value; }
        }

        /// GetS and Sets the remote directory.
        public string RemotePath
        {
            get { return this.remotePath; }
            set { this.remotePath = value; }
        }

        /// Gets and Sets the username.
        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }

        /// Gets and Set the password.
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        /// If the value of mode is true, set binary mode for downloads, else, Ascii mode.
        public bool BinaryMode
        {
            get { return this.binMode; }
            set
            {
                if (this.binMode == value) return;

                if (value)
                {
                    sendCommand("TYPE I");		// Binary
                }
                else
                {
                    sendCommand("TYPE A");		// Ascii
                }

                if (this.resultCode != 200) FireException(result.Substring(4));
            }
        }

        //		public void ConnectionMode
        //		{
        //			get
        //			{
        //				return connectionMode;
        //			}
        //			set
        //			{
        //				connectionMode = value;
        //			}
        //		}

        public bool Logined
        {
            get { return this.loggedin; }
        }

        /// Login to the remote server.

        public void Login()
        {
            if (this.loggedin) this.Close();

            IPAddress addr = null;
            IPEndPoint ep = null;

            try
            {
                this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                addr = Dns.GetHostEntry(this.server).AddressList[0];

                ep = new IPEndPoint(addr, this.port);
                this.clientSocket.Connect(ep);
            }
            catch (System.Exception ex)
            {
                // doubtfull
                if (this.clientSocket != null && this.clientSocket.Connected) this.clientSocket.Close();

                throw new FtpException("Couldn't connect to remote server", ex);
            }

            this.readResponse();

            if (this.resultCode != 220)
            {
                this.Close();
                FireException(this.result.Substring(4));
            }

            /// <summary> 
            /// Sends a user name
            /// </summary>
            /// <remarks>Reply codes: 230 530 500 501 421 331 332</remarks>
            this.sendCommand("USER " + username);

            if (!(this.resultCode == 331 || this.resultCode == 230))
            {
                this.cleanup();
                FireException(this.result.Substring(4));
            }

            if (this.resultCode != 230)
            {
                /// <summary>
                /// send the user's password
                /// </summary>
                /// <remarks>Reply codes: 230 202 530 500 501 503 421 332</remarks>
                this.sendCommand("PASS " + password);

                if (!(this.resultCode == 230 || this.resultCode == 202))
                {
                    this.cleanup();
                    FireException(this.result.Substring(4));
                }
            }

            this.loggedin = true;

            /// <summary>
            /// Finds out the type of operating system at the server.
            /// </summary>
            /// <remarks>Reply codes: 215 500 501 502 421</remarks>
            this.sendCommand("SYST\r\n");
            if (!(this.resultCode == 215))
            {
                FireException(this.result.Substring(4));
            }

            string lstr_temp = this.result.Remove(0, 4).Substring(0, 4);
            if (lstr_temp == "UNIX")
            {
                m_iServerOS = 1;
            }
            else if (lstr_temp == "Wind")
            {
                m_iServerOS = 2;
            }
            else
            {
                m_iServerOS = 3;
                /*	Currently not supported */
                this.cleanup();
                FireException("This version of FTP Explorer supports browsing only on Windows and Unix based FTP Services. FTP browsing on other FTP services will be enabled in future versions.");
            }

            this.ChangeDir(this.remotePath);
        }

        /// <summary>
        /// Close the FTP connection.
        /// </summary>
        public void Close()
        {
            try
            {
                if (this.clientSocket != null)
                {
                    /// <summary>
                    /// This command terminates a USER and if file transfer is not in progress, the server closes the control connection. 
                    /// </summary>
                    /// <remarks>Reply codes: 221 500</remarks>
                    this.sendCommand("QUIT");
                }

                this.cleanup();
            }
            catch (System.Exception ex)
            {
                this.loggedin = false;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Return a string array containing the remote directory's file list.
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList()
        {
            return this.GetFileList("*.*");
        }

        /// <summary>
        /// Return a string array containing the remote directory's file list.
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public string[] GetFileList(string mask)
        {
            if (!this.loggedin) this.Login();

            Socket cSocket = createDataSocket();

            /// <summary>
            /// Causes a list to be sent from the server to the passive DTP. If 
            /// the pathname specifies a directory or other group of files, the server should 
            /// transfer a list of files in the specified directory. If the pathname specifies 
            /// a file then the server should send current information on the file. A null 
            /// argument implies the user's current working or default directory.
            /// </summary>
            /// <remarks>Reply codes: 125 150 226 250 425 426 451 450 500 501 502 421 530</remarks>
            this.sendCommand("LIST " + mask);

            if (!(this.resultCode == 150 || this.resultCode == 125 || this.resultCode == 250 || this.resultCode == 226))
                FireException(this.result.Substring(4));

            this.message = "";

            string l_strOutput = "", l_strTemp = "";
            int l_iRetval = 0;

            receiveBuffer.Initialize();
            l_strTemp = "";
            l_strOutput = "";

            Thread.Sleep(100);

            for (; (l_iRetval = cSocket.Receive(receiveBuffer)) > 0; )
            {
                l_strTemp = Encoding.Default.GetString(receiveBuffer, 0, l_iRetval);
                l_strOutput += l_strTemp;
                if (cSocket.Available == 0)
                {
                    break;
                }
            }

            this.message = l_strOutput;
            string[] msg = this.message.Replace("\r", "").Split('\n');

            cSocket.Close();

            if (this.message.IndexOf("No such file or directory") != -1)
                msg = new string[] { };

            return msg;
        }


        /// Return the size of a file.
        public long GetFileSize(string fileName)
        {
            if (!this.loggedin) this.Login();

            this.sendCommand("SIZE " + fileName);
            long size = 0;

            if (this.resultCode == 213)
            {
                size = long.Parse(this.result.Substring(4));
            }
            else
            {
                FireException(this.result.Substring(4));
            }

            return size;
        }


        /// <summary>
        /// Download a file to the Assembly's local directory,
        /// keeping the same file name.
        /// </summary>
        /// <param name="remFileName"></param>
        public void Download(string remFileName)
        {
            this.Download(remFileName, "", false);
        }

        /// <summary>
        /// Download a remote file to the Assembly's local directory,
        /// keeping the same file name, and set the resume flag.
        /// </summary>
        /// <param name="remFileName"></param>
        /// <param name="resume"></param>
        public void Download(string remFileName, Boolean resume)
        {
            this.Download(remFileName, "", resume);
        }

        /// <summary>
        /// Download a remote file to a local file name which can include
        /// a path. The local file name will be created or overwritten,
        /// but the path must exist.
        /// </summary>
        /// <param name="remFileName"></param>
        /// <param name="locFileName"></param>
        public void Download(string remFileName, string locFileName)
        {
            this.Download(remFileName, locFileName, false);
        }

        /// <summary>
        /// Download a remote file to a local file name which can include
        /// a path, and set the resume flag. The local file name will be
        /// created or overwritten, but the path must exist.
        /// </summary>
        /// <param name="remFileName"></param>
        /// <param name="locFileName"></param>
        /// <param name="resume"></param>
        public void Download(string remFileName, string locFileName, Boolean resume)
        {
            if (!this.loggedin) this.Login();

            this.BinaryMode = true;

            if (locFileName.Equals(""))
            {
                locFileName = remFileName;
            }

            FileStream output = null;

            if (!File.Exists(locFileName))
                output = File.Create(locFileName);

            else
                output = new FileStream(locFileName, FileMode.Open);

            Socket cSocket = createDataSocket();

            long offset = 0;

            if (resume)
            {
                offset = output.Length;

                if (offset > 0)
                {

                    /// <summary>
                    /// The argument field represents the server marker at which file transfer is to be
                    /// restarted. This command does not cause file transfer but skips over the file to
                    /// the specified data checkpoint.
                    /// </summary>
                    /// <remarks>Reply codes: 500 501 502 421 530 350</remarks>
                    this.sendCommand("REST " + offset);
                    if (this.resultCode != 350)
                    {
                        //Server dosnt support resuming
                        offset = 0;
                    }
                    else
                    {
                        output.Seek(offset, SeekOrigin.Begin);
                    }
                }
            }

            /// <summary>
            /// Causes the server-DTP to transfer a copy of the file, specified 
            /// in the pathname, to the server- or user-DTP at the other end of the data 
            /// connection. The status and contents of the file at the server site shall be unaffected.
            /// </summary>
            /// <remarks>Reply codes: 125 150 110 226 250 425 426 451 450 550 500 501 421 530</remarks>
            this.sendCommand("RETR " + remFileName);

            /*	125,150,110,250,226 (success) */
            if (this.resultCode != 150 && this.resultCode != 125 && this.resultCode != 110 && this.resultCode != 250 && this.resultCode != 226)
            {
                FireException(this.result.Substring(4));
            }

            DateTime timeout = DateTime.Now.AddSeconds(this.timeoutSeconds);

            while (timeout > DateTime.Now)
            {
                this.bytes = cSocket.Receive(buffer, buffer.Length, 0);
                output.Write(this.buffer, 0, this.bytes);

                if (this.bytes <= 0)
                {
                    break;
                }
            }

            output.Close();

            if (cSocket.Connected) cSocket.Close();

            //this.readResponse();

            if (this.resultCode != 226 && this.resultCode != 250)
            {
                FireException(this.result.Substring(4));
            }
        }


        /// <summary>
        /// Upload a file.
        /// </summary>
        /// <param name="fileName"></param>
        public void Upload(string fileName)
        {
            this.Upload(fileName, false);
        }


        /// <summary>
        /// Upload a file and set the resume flag.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="resume"></param>
        public void Upload(string fileName, bool resume)
        {
            FileStream input = null;
            try
            {
                if (!this.loggedin)
                {
                    this.Login();
                }

                Socket cSocket = null;
                long offset = 0;

                if (resume)
                {
                    try
                    {
                        this.BinaryMode = true;

                        offset = GetFileSize(Path.GetFileName(fileName));
                    }
                    catch (System.Exception)
                    {
                        // file not exist
                        offset = 0;
                    }
                }

                // open stream to read file
                input = new FileStream(fileName, FileMode.Open, System.IO.FileAccess.Read);

                if (resume && input.Length < offset)
                {
                    // different file size
                    offset = 0;
                }
                else if (resume && input.Length == offset)
                {
                    // file done
                    input.Close();
                    return;
                }

                // dont create untill we know that we need it
                cSocket = this.createDataSocket();

                if (offset > 0)
                {
                    /// <summary>
                    /// The argument field represents the server marker at which file transfer is to be
                    /// restarted. This command does not cause file transfer but skips over the file to
                    /// the specified data checkpoint.
                    /// </summary>
                    /// <remarks>Reply codes: 500 501 502 421 530 350</remarks>
                    this.sendCommand("REST " + offset);
                    if (this.resultCode != 350)
                    {
                        offset = 0;
                    }
                }

                /// <summary>
                /// Causes the server-DTP to accept the data transferred via the data connection 
                /// and to store the data as a file at the server site. If the file specified in 
                /// the pathname exists at the server site, then its contents shall be replaced 
                /// by the data being transferred. A new file is created at the server site if 
                /// the file specified in the pathname does not already exist.
                /// </summary>
                /// <remarks>Reply codes: 125 150 110 226 250 425 426 451 551 552 532 450 452 553 500 501 421 530</remarks>
                this.sendCommand("STOR " + Path.GetFileName(fileName));

                if (this.resultCode != 125 && this.resultCode != 150)
                {
                    FireException(result.Substring(4));
                }

                if (offset != 0)
                {

                    input.Seek(offset, SeekOrigin.Begin);
                }

                while ((bytes = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    cSocket.Send(buffer, bytes, 0);
                    nSendFileSize = Convert.ToDecimal(input.Position);
                }

                input.Close();

                if (cSocket.Connected)
                {
                    cSocket.Close();
                }

                this.readResponse();

                if (this.resultCode != 226 && this.resultCode != 250)
                {
                    FireException(this.result.Substring(4));
                }
            }
            catch (System.Exception ex)
            {
                input.Close();
                input = null;
                throw new FtpException("File Trans Error", ex);
            }
        }

        /// <summary>
        /// Upload a directory and its file contents
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recurse">Whether to recurse sub directories</param>
        public void UploadDirectory(string path, bool recurse)
        {
            this.UploadDirectory(path, recurse, "*.*");
        }

        /// <summary>
        /// Upload a directory and its file contents
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recurse">Whether to recurse sub directories</param>
        /// <param name="mask">Only upload files of the given mask - everything is '*.*'</param>
        public void UploadDirectory(string path, bool recurse, string mask)
        {
            string[] dirs = path.Replace("/", @"\").Split('\\');
            string rootDir = dirs[dirs.Length - 1];

            // make the root dir if it doed not exist
            try
            {
                // try to retrieve files
                this.GetFileList(rootDir);
            }
            catch
            {
                // if receive an error
                this.MakeDir(rootDir);
            }

            this.ChangeDir(rootDir);

            foreach (string file in Directory.GetFiles(path, mask))
            {
                this.Upload(file, true);
            }
            if (recurse)
            {
                foreach (string directory in Directory.GetDirectories(path))
                {
                    this.UploadDirectory(directory, recurse, mask);
                }
            }

            this.ChangeDir("..");
        }

        /// <summary>
        /// Delete a file from the remote FTP server.
        /// </summary>
        /// <param name="fileName"></param>
        public void DeleteFile(string fileName)
        {
            if (!this.loggedin) this.Login();

            /// <summary>
            /// Causes the file specified in the pathname to be deleted at the server site
            /// </summary>
            /// <remarks>Reply codes: 250 450 550 500 501 502 421 530</remarks>
            this.sendCommand("DELE " + fileName);

            if (this.resultCode != 250)
            {
                FireException(this.result.Substring(4));
            }

        }

        /// <summary>
        /// Rename a file on the remote FTP server.
        /// </summary>
        /// <param name="oldFileName"></param>
        /// <param name="newFileName"></param>
        /// <param name="overwrite">setting to false will throw exception if it exists</param>
        public void RenameFile(string oldFileName, string newFileName, bool overwrite)
        {
            if (!this.loggedin) this.Login();

            /// <summary>
            /// Specifies the old pathname of the file which is to be renamed. This 
            /// command must be immediately followed by a "rename to" command specifying the new 
            /// file pathname.
            /// </summary>
            /// <remarks>Reply codes: 450 550 500 501 502 421 530 350</remarks>
            this.sendCommand("RNFR " + oldFileName);

            if (this.resultCode != 350)
            {
                FireException(this.result.Substring(4));
            }

            if (!overwrite)
            {
                this.sendCommand("SIZE " + newFileName);
                if (this.resultCode == 213)
                {
                    FireException("File already exists");
                }
            }

            /// <summary>
            /// Specifies the new pathname of the file specified in the immediately 
            /// preceding "rename from" command. Together the two commands cause a file to be renamed.
            /// </summary>
            /// <remarks>Reply codes: 250 532 553 500 501 502 503 421 530</remarks>
            this.sendCommand("RNTO " + newFileName);

            if (this.resultCode != 250)
            {
                FireException(this.result.Substring(4));
            }
        }

        /// <summary>
        /// Create a directory on the remote FTP server.
        /// </summary>
        /// <param name="dirName"></param>
        public void MakeDir(string dirName)
        {
            if (!this.loggedin) this.Login();

            /// <summary>
            /// Causes the directory specified in the pathname to be created as 
            /// a directory (if the pathname is absolute) or as a subdirectory of the current 
            /// working directory (if the pathname is relative).
            /// </summary>
            /// <remarks>Reply codes: 257 500 501 502 421 530 550</remarks>
            this.sendCommand("MKD " + dirName);

            if (this.resultCode != 250 && this.resultCode != 257) FireException(this.result.Substring(4));
        }

        /// <summary>
        /// Delete a directory on the remote FTP server.
        /// </summary>
        /// <param name="dirName"></param>
        public void RemoveDir(string dirName)
        {
            if (!this.loggedin) this.Login();

            /// <summary>
            /// Causes the directory specified in the pathname to be removed as 
            /// a directory (if the pathname is absolute) or as a subdirectory of the current 
            /// working directory (if the pathname is relative).
            /// </summary>
            /// <remarks>Reply codes: 250 500 501 502 421 530 550</remarks>
            this.sendCommand("RMD " + dirName);

            if (this.resultCode != 250) FireException(this.result.Substring(4));
        }


        // Change the current working directory on the remote FTP server.
        public string ChangeDir(string dirName)
        {
            if (dirName == null || dirName.Equals(".") || dirName.Length == 0)
            {
                return "";
            }

            if (!this.loggedin) this.Login();

            // Changes the current directory			
            // <remarks>Reply codes: 250 500 501 502 421 530 550</remarks>
            this.sendCommand("CWD " + dirName);

            //if ( ( this.resultCode != 250 ) && ( this.resultCode != 150 ) )

            if ((this.resultCode == 500) || (this.resultCode == 501) || (this.resultCode == 502) || (this.resultCode == 421) || (this.resultCode == 530) || (this.resultCode == 550))
                FireException(result.Substring(4));

            // Causes the name of the current working directory to be returned in the reply.
            // reply codes: 257 500 501 502 421 550</remarks>
            this.sendCommand("PWD");

            if ((this.resultCode == 500) || (this.resultCode == 501) || (this.resultCode == 502) || (this.resultCode == 421) || (this.resultCode == 550))
                FireException(result.Substring(4));

            // gonna have to do better than this....
            this.remotePath = this.message.Split('"')[1];
            return this.remotePath;
        }


        private void readResponse()
        {
            this.message = "";
            this.result = this.readLine();

            if (this.result.Length > 3)
            {
                this.resultCode = int.Parse(this.result.Substring(0, 3));
            }
            else
            {
                this.result = null;
            }
        }

        private string readLine()
        {
            string l_strOutput = "", l_strTemp = "";
            int l_iRetval = 0;

            receiveBuffer.Initialize();
            l_strTemp = "";
            l_strOutput = "";

            Thread.Sleep(100);

            /* Receive data in a loop until the FTP server sends it */
            for (; (l_iRetval = clientSocket.Receive(receiveBuffer)) > 0; )
            {
                l_strTemp = Encoding.Default.GetString(receiveBuffer, 0, l_iRetval);
                l_strOutput += l_strTemp;
                if (clientSocket.Available == 0)
                {
                    break;
                }
            }
            this.message = l_strOutput;

            string[] msg = this.message.Split('\n');

            if (this.message.Length > 2)
            {
                this.message = msg[msg.Length - 2];
            }
            else
            {
                this.message = msg[0];
            }

            if (this.message.Length > 4 && !this.message.Substring(3, 1).Equals(" "))
            {
                return this.readLine();
            }

            return message;
        }

        /// <summary>
        /// sendCommand
        /// </summary>
        /// <param name="command"></param>
        private void sendCommand(String command)
        {
            int l_iRetval = 0;

            try
            {
                Byte[] cmdBytes = Encoding.Default.GetBytes((command.Trim() + "\r\n").ToCharArray());
                l_iRetval = clientSocket.Send(cmdBytes, cmdBytes.Length, 0);
                this.readResponse();
            }
            catch (System.Exception ex)
            {
                FireException(ex.Message, ex);
            }
        }

        /// <summary>
        /// when doing data transfers, we need to open another socket for it.
        /// </summary>
        /// <returns>Connected socket</returns>
        private Socket createDataSocket()
        {
            /// <summary>
            /// Requests the server-DTP to "listen" on a data port (which is not
            /// its default data port) and to wait for a connection rather than initiate one
            /// upon receipt of a transfer command. The response to this command includes the 
            /// host and port address this server is listening on. 
            /// </summary>
            /// <remarks>Reply codes: 227 500 501 502 421 530</remarks>

            this.sendCommand("PASV");

            if (this.resultCode != 227)
            {
                FireException(this.result.Substring(4));
            }

            int index1 = this.result.IndexOf('(');
            int index2 = this.result.IndexOf(')');

            string ipData = this.result.Substring(index1 + 1, index2 - index1 - 1);

            int[] parts = new int[6];

            int len = ipData.Length;
            int partCount = 0;
            string buf = "";

            for (int i = 0; i < len && partCount <= 6; i++)
            {
                //char ch = char.Parse(ipData.Substring(i, 1));
                char ch = (char)ipData.Substring(i, 1).ToCharArray()[0];
                
                if (char.IsDigit(ch))
                {
                    buf += ch;
                }
                else if (ch != ',')
                {
                    FireException("Malformed PASV result: " + result);
                }

                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = int.Parse(buf);
                        buf = "";
                    }
                    catch (System.Exception ex)
                    {
                        FireException("Malformed PASV result (not supported?): " + this.result, ex);
                    }
                }
            }

            string ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];

            int port = (parts[4] << 8) + parts[5];

            Socket socket = null;
            IPEndPoint ep = null;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ep = new IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList[0], port);
                socket.Connect(ep);
            }
            catch (System.Exception ex)
            {
                // doubtfull....
                if (socket != null && socket.Connected) socket.Close();

                FireException("Can't connect to remote server", ex);
            }

            return socket;
        }

        private void cleanup()
        {
            if (this.clientSocket != null)
            {
                this.clientSocket.Close();
                this.clientSocket = null;
            }
            this.loggedin = false;
        }

        ~FtpClient()
        {
            this.cleanup();
        }

        private void FireException(string message, System.Exception innerException)
        {
            throw new FtpException(message, innerException);
        }

        private void FireException(string message)
        {
            throw new FtpException(message);
        }
    }
}