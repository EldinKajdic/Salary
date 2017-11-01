using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FolderControl
{
    public partial class FolderService : ServiceBase
    {
        public FolderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine(CheckFolders());

        }

        private string CheckFolders()
        {
            DirectoryInfo listOfFiles = new DirectoryInfo(@"D:\Repos\Salary\SalarySystem\Textfiles\SavedFiles\");
            string SavedFilesPath = @"D:\Repos\Salary\SalarySystem\Textfiles\SavedFiles\Old-files\";
            string result = "No files were found.";

            FileInfo[] filer = listOfFiles.GetFiles();

            if (listOfFiles != null)
            {
                foreach (FileInfo item in filer)
                {
                    item.MoveTo(SavedFilesPath + item.Name);
                    result = $"The files were moved to {SavedFilesPath}.";
                }
            }
            else
            {
                result = "No files were found.";
            }
            return result;
        }

        protected override void OnStop()
        {
        }
    }
}
