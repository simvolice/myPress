using System;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace MyPress.Client.View
{
    public partial class MainUserControl : UserControl
    {
        public MainUserControl()
        {
            InitializeComponent();

            

            Messenger.Default.Register<Uri>(this, "Navigate",
                (uri) => ContentFrame.Navigate(uri));



        
        }

    






       
    }
}
