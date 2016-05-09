#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-05-20   Mr Luis Lee	     1.0          First release.
//* --------------------------------------------------------------
//* CLASS DESCRIPTION
//* --------------------------------------------------------------
#endregion 

using System;
using System.IO; 
using System.Security.Cryptography;

namespace Framework.Encrypter
{
	/// <summary>
	/// Summary description for Security.
	/// </summary>
	public class Security
	{
		private const string PASSWORD = "{1FCC37D8-E00B-4bef-99C3-529DC051082B}";

		public static string Encrypt(string clearText) 
		{ 
			string Password = PASSWORD;
			byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText); 
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 

			MemoryStream ms = new MemoryStream(); 

			Rijndael alg = Rijndael.Create(); 

			alg.Key = pdb.GetBytes(32); 
			alg.IV = pdb.GetBytes(16); 

			CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write); 

			cs.Write(clearBytes, 0, clearBytes.Length); 

			cs.Close(); 

			byte[] encryptedData = ms.ToArray();

			return Convert.ToBase64String(encryptedData); 
		}

		public static string Decrypt(string cipherText) 
		{ 
			try
			{
				string Password = PASSWORD;
				byte[] cipherBytes = Convert.FromBase64String(cipherText); 

				PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
					new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 

				MemoryStream ms = new MemoryStream(); 

				Rijndael alg = Rijndael.Create(); 
				alg.Key = pdb.GetBytes(32); 
				alg.IV = pdb.GetBytes(16); 

				CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write); 

				cs.Write(cipherBytes, 0, cipherBytes.Length); 
				cs.Close(); 
				byte[] decryptedData = ms.ToArray(); 

				return System.Text.Encoding.Unicode.GetString(decryptedData); 
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return string.Empty;
			}
		}
	}
}