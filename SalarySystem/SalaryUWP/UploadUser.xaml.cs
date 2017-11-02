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
    public sealed partial class UploadUser : Page
    {
        public UploadUser()
        {
            InitializeComponent();
        }

        private async void UploadUsersClick(object sender, RoutedEventArgs e)
        {
            string path = uploadLinkTb.Text;

            if (path.Equals(""))
            {
                uploadStatusText.Text = "The file could not be uploaded.";
                uploadStatusText.Text = "Please enter a link to the text file.";
                uploadStatusTextTwo.Text = "";
                uploadStatusTextThree.Text = "";
                uploadStatusText.FontWeight = FontWeights.Bold;
            }
            else
            {
                uploadStatusText.Text = "Uploading file...";
                LoadingBar.IsEnabled = true;
                LoadingBar.Visibility = Visibility.Visible;

                DbServiceReference.DbServiceClient dbServiceReference = new DbServiceReference.DbServiceClient();

                bool fileUploaded = await dbServiceReference.CreateUserFromTxtFileAsync(path);
                LoadingBar.IsEnabled = false;
                LoadingBar.Visibility = Visibility.Collapsed;

                if (fileUploaded == true)
                {
                    uploadStatusText.Text = "The users have successfully been added.";
                    uploadStatusTextTwo.Text = "";
                    uploadStatusTextThree.Text = "";
                    uploadStatusText.FontWeight = FontWeights.Normal;
                }
                else
                {
                    uploadStatusText.Text = "The file could not be uploaded.";
                    uploadStatusTextTwo.Text = $"Couldn't upload file '{path}'.";
                    uploadStatusTextThree.Text = "The uploaded file was not valid.";
                    uploadStatusText.FontWeight = FontWeights.Bold;
                }
            }

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
