using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Common.ResourceManager
{
    public class ResourceManager
    {
        protected System.Resources.ResourceManager rm;
        protected static Framework.Common.ResourceManager.ResourceManager instance;

        protected ResourceManager() 
        {
			initializeFacade();	
		}

        static ResourceManager()
        {
            instance = new ResourceManager();
        }

        protected virtual void initializeFacade()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            rm = new System.Resources.ResourceManager("Framework.Common.ResourceManager.i18n", assembly);
		}

        public static Framework.Common.ResourceManager.ResourceManager getInstance()
        {
            return instance;
        }

        public string getString(string title)
        {
            return rm.GetString(title);
        }
    }
}
