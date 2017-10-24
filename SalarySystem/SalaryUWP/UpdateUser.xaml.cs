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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateUser : Page
    {
        public UpdateUser()
        {
            this.InitializeComponent();
            dateTextBlock.Text = DateTime.Today.ToString("dddd, dd MMMM yyyy");
        }

        private async void UpdateUserClick(object sender, RoutedEventArgs e)
        {

            string email = userEmailTb.Text;
            string password = userPasswordTb.Password.ToString();
            string confirmedPassword = userConfirmPasswordTb.Password.ToString();

            if (password.Length <= 6)
            {
                updateStatusText.Text = "The password could not be updated.";
                updateStatusTextTwo.Text = "The length of your password must be longer than 6 characters.";
                updateStatusTextThree.Text = "";
                updateStatusText.FontWeight = FontWeights.Bold;
            }

            if (email.Equals("") || password.Equals("") || confirmedPassword.Equals(""))
            {
                updateStatusText.Text = "The password could not be updated.";
                updateStatusText.Text = "Please enter information in all fields.";
                updateStatusTextTwo.Text = "";
                updateStatusTextThree.Text = "";
                updateStatusText.FontWeight = FontWeights.Bold;
            }
            else
            {
                if (password != confirmedPassword)
                {
                    updateStatusText.Text = "The password could not be updated.";
                    updateStatusTextTwo.Text = "The two passwords did not match.";
                    updateStatusTextThree.Text = "Did you enter the two passwords differently?";
                }
                else
                {
                    if (password.Length < 6)
                    {
                        updateStatusText.Text = "The account could not be created.";
                        updateStatusTextTwo.Text = "The length of your password must be longer than 6 characters.";
                        updateStatusText.FontWeight = FontWeights.Bold;
                    }
                    else
                    {

                        updateStatusText.Text = "Updating password...";
                        LoadingBar.IsEnabled = true;
                        LoadingBar.Visibility = Visibility.Visible;

                        DbServiceReference.DbServiceClient dbServiceReference = new DbServiceReference.DbServiceClient();


                        bool checkIfUpdated = await dbServiceReference.UpdatePasswordAsync(email, confirmedPassword);
                        LoadingBar.IsEnabled = false;
                        LoadingBar.Visibility = Visibility.Collapsed;

                        if (checkIfUpdated == true)
                        {
                            updateStatusText.Text = "Password has successfully been updated.";
                            updateStatusTextTwo.Text = "";
                            updateStatusTextThree.Text = "";
                            updateStatusText.FontWeight = FontWeights.Normal;
                        }
                        else
                        {
                            updateStatusText.Text = $"The password could not be updated.";
                            updateStatusTextTwo.Text = $"No user with the email '{email}' was found.";
                            updateStatusTextThree.Text = $"Did you enter the correct email address?";
                            updateStatusText.FontWeight = FontWeights.Bold;
                        }
                    }

                }
            }

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
