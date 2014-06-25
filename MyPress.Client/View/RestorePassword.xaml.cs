using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace MyPress.Client.View
{
    public partial class RestorePassword : ChildWindow
    {
        public RestorePassword()
        {
            InitializeComponent();



            Messenger.Default.Register<bool>(this, HandleDialogResults); 

        }

        private void HandleDialogResults(bool obj)
        {
            if (obj)
                this.DialogResult = true;
        }

      
    }
}

