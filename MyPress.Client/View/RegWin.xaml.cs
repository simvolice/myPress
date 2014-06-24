using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace MyPress.Client.View
{
    public partial class RegWin : ChildWindow
    {
        public RegWin()
        {
            InitializeComponent();



            Messenger.Default.Register<string>(this, HandleDialogResults); 
                
                
                
                
                
        }

        private void HandleDialogResults(string a)
        {

            if (a == "true")

            this.DialogResult = true;  

           
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
        }

      
    }
}

