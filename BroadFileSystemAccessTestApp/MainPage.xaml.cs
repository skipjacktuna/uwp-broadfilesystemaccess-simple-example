using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace BroadFileSystemAccessTestApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void FileOpen_Click(object sender, RoutedEventArgs e)
        {
            var installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            // set file path
            // ex: var path = @"C:\targetfile"
            var path = @"C:\Windows\DirectX.log";
            this.textBlock.Text = String.Format("### Open {0} ###\n", path);
            BroadFileOpen(path);
        }

        private async void BroadFileOpen(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);
            string text = await FileIO.ReadTextAsync(file);
            this.textBlock.Text = text;
        }

        private void FolderOpen_Click(object sender, RoutedEventArgs e)
        {
            var installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            // set folder path
            // ex: var path = @"C:\"
            var path = installFolder.Path;
            this.textBlock.Text = String.Format("### Open {0} ###\n", path);
            BroadFolderOpen(path);
        }

        private async void BroadFolderOpen(string path)
        {
            var folder = await StorageFolder.GetFolderFromPathAsync(path);
            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                this.textBlock.Text += file.Path + "\n";
            }
        }
    }
}
