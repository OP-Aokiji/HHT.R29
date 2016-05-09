using System;
using System.Collections.Generic;
using System.Text;

namespace MOST.Common.UserAttribute
{
    [AttributeUsage(AttributeTargets.All,
        AllowMultiple=false,
        Inherited=false)]
    public class AuthAccessAttribute : Attribute
    {
        private string programID;
        private string programName;
        public AuthAccessAttribute(string programID, string programName)
        {
            this.programID = programID;
            this.programName = programName;
        }

        public string ProgramID
        {
            get { return programID; }
        }

        public string ProgramName
        {
            get { return programName; }
        }
    }
}

