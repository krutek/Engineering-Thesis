using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Management.Automation;
using System.Diagnostics;
using System.Threading;
using Klient.Models;
using System.IO;

namespace Klient.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public ActualBaseViewModel _act;
        public ShellViewModel( ActualBaseViewModel act)
        {
            ApiConnectModel.InitializeClient();
            _act = act;
            ActivateItem(_act);
        }
        public void loadMainMenu()
        {
            ActivateItem(new ActualBaseViewModel());
        }
        public void loadModifyData()
        {
            ActivateItem(new ModifyDataViewModel());
        }
        public void loadSearchMenu()
        {
            ActivateItem(new SavedBaseViewModel());
        }
        public void exit()
        {
            this.TryClose();
        }
    }
}
