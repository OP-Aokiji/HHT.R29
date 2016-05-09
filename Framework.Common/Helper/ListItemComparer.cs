using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Framework.Common.Helper
{
    public class ListItemComparer : IComparer
    {
        public virtual int Compare(object x, object y)
        {
            CaseInsensitiveComparer c = new CaseInsensitiveComparer();
            return c.Compare(x.ToString(), y.ToString());
        }
    }
}
