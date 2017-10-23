﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SalaryUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(500, 800);
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(400, 800));
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private async void Connect_Click(object sender, RoutedEventArgs e)
        {
            DbServiceReference.DbServiceClient dbServiceReference = new DbServiceReference.DbServiceClient();

            LoadingBar.IsEnabled = true;
            LoadingBar.Visibility = Visibility.Visible;

            loginStatusText.Text = "You are logging in...";

            string usernameInput = usernameTxtBox.Text;
            string passwordInput = passwordTxtBox.Password.ToString();
            bool loginStatus = await dbServiceReference.adminAuthAsync(usernameInput, passwordInput);
            LoadingBar.IsEnabled = false;
            LoadingBar.Visibility = Visibility.Collapsed;
            if (loginStatus == true)
            {
                loginStatusText.Text = "You have successfully logged in!";
            }
            else
            {
                loginStatusText.Text = " The account or password is incorrect.\nAre you sure you entered the right one?";
                loginStatusText.FontWeight = FontWeights.Bold;
            }
        }
    }
}
