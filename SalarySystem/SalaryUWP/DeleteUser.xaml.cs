using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
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
    public sealed partial class DeleteUser : Page
    {
        public DeleteUser()
        {
            this.InitializeComponent();
            dateTextBlock.Text = DateTime.Today.ToString("dddd, dd MMMM yyyy");
        }

        private async void DeleteUserClick(object sender, RoutedEventArgs e)
        {
            string email = userEmailTb.Text;

            if (email.Equals(""))
            {
                deleteStatusText.Text = "The account could not be removed.";
                deleteStatusText.Text = "Please enter the users email.";
                deleteStatusTextTwo.Text = "";
                deleteStatusTextThree.Text = "";
                deleteStatusText.FontWeight = FontWeights.Bold;
            }
            else
            {
                deleteStatusText.Text = "Removing user...";
                LoadingBar.IsEnabled = true;
                LoadingBar.Visibility = Visibility.Visible;

                DbServiceReference.DbServiceClient dbServiceReference = new DbServiceReference.DbServiceClient();

                bool checkIfDeleted = await dbServiceReference.DeleteUserAsync(email);
                LoadingBar.IsEnabled = false;
                LoadingBar.Visibility = Visibility.Collapsed;

                if (checkIfDeleted == true)
                {
                    deleteStatusText.Text = "Account has successfully been removed.";
                    deleteStatusTextTwo.Text = "";
                    deleteStatusTextThree.Text = "";
                    deleteStatusText.FontWeight = FontWeights.Normal;
                }
                else
                {
                    deleteStatusText.Text = $"The account could not be removed.";
                    deleteStatusTextTwo.Text = $"No user with the email '{email}' was found.";
                    deleteStatusTextThree.Text = $"Did you enter the correct email address?";
                    deleteStatusText.FontWeight = FontWeights.Bold;
                }
            }

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
