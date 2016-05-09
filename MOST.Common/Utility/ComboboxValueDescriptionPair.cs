using System;
using System.Collections.Generic;
using System.Text;

namespace MOST.Common.Utility
{
    public partial class ComboboxValueDescriptionPair
    {
        // description
        private object description;

        // value
        private object value;

        public object Description
        {
            get 
            {
                if (this.description == null)
                {
                    return new object();
                }
                else
                {
                    return description; 
                }
            }
            set { description = value; }
        }

        public object Value
        {
            get 
            { 
                if (this.value == null)
                {
                    return new object();
                }
                else
                {
                    return this.value; 
                }
            }
            set { this.value = value; }
        }

        public ComboboxValueDescriptionPair(object value, object description)
        {
            this.description = description;
            this.value = value;
        }

        public ComboboxValueDescriptionPair(object value) : this(value, value)
        {
        }

        public override string ToString()
        {
            if (description != null)
            {
                return description.ToString();
            }
            return "";
        }
    }
}
