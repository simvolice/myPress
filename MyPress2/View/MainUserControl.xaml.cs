using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;

namespace MyPress2.View
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
