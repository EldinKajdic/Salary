using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SalaryUWP
{
    public sealed partial class AdminContent : Page
    {
        public AdminContent()
        {
            this.InitializeComponent();
            dateTextBlock.Text = DateTime.Today.ToString("dddd, dd MMMM yyyy");
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUser));
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DeleteUser));
        }

        private void UpdateUser(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateUser));
        }
    }
}
