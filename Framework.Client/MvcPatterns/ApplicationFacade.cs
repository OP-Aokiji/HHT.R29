using System;
using System.Collections.Generic;
using System.Text;
using MvcPatterns.Command;
using System.Windows.Forms;

namespace MvcPatterns
{
    public class ApplicationFacade : MvcPatterns.Core.Facade, MvcPatterns.Interfaces.IFacade
    {
        private string STARTUP = "view";

        static ApplicationFacade()
        {
            instance = new ApplicationFacade();
        }

        public new static ApplicationFacade getInstance()
        {
            if (instance == null) instance = new ApplicationFacade();
            return instance as ApplicationFacade;
        }

        protected override void initializeController()
        {
            base.initializeController();
            registerCommand(STARTUP, typeof(FormCommand));
        }

        public override void startup(object app)
        {
           Application.Run(app as Form);
        }
    }
}
