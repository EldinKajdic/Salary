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
    public sealed partial class AddUser : Page
    {
        public AddUser()
        {
            this.InitializeComponent();
            dateTextBlock.Text = DateTime.Today.ToString("dddd, dd MMMM yyyy");
        }

        private async void AddUserClick(object sender, RoutedEventArgs e)
        {

            string name = userNameTb.Text;
            string email = userEmailTb.Text;
            string password = userPasswordTb.Password.ToString();

            if (name.Equals("") || email.Equals("") || password.Equals(""))
            {
                createStatusText.Text = "The account could not be created.";
                createStatusTextTwo.Text = "Please enter information in all fields.";
                createStatusText.FontWeight = FontWeights.Bold;
            }
            else
            {
                if (password.Length <= 6)
                {
                    createStatusText.Text = "The account could not be created.";
                    createStatusTextTwo.Text = "The length of your password must be longer than 6 characters.";
                    createStatusText.FontWeight = FontWeights.Bold;
                }
                else
                {
                    if (email.Contains("@"))
                    {
                        createStatusText.Text = "Creating user...";
                        LoadingBar.IsEnabled = true;
                        LoadingBar.Visibility = Visibility.Visible;

                        DbServiceReference.DbServiceClient dbServiceReference = new DbServiceReference.DbServiceClient();

                        bool checkIfCreated = await dbServiceReference.CreateUserAsync(name, email, password);
                        LoadingBar.IsEnabled = false;
                        LoadingBar.Visibility = Visibility.Collapsed;

                        if (checkIfCreated == true)
                        {
                            createStatusText.Text = "Account has successfully been created!";
                            createStatusTextTwo.Text = "";
                            createStatusText.FontWeight = FontWeights.Normal;
                        }
                        else
                        {
                            createStatusText.Text = $"The account could not be created.";
                            createStatusTextTwo.Text = $"An account already exists with the email '{email}'.";
                            createStatusText.FontWeight = FontWeights.Bold;
                        }
                    }

                    else
                    {
                        createStatusText.Text = $"The account could not be created.";
                        createStatusTextTwo.Text = $"'{email}' is not a valid format for an email.";
                        createStatusText.FontWeight = FontWeights.Bold;
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
