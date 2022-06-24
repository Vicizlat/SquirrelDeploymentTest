using System.Windows;
using Squirrel;

namespace SquirrelDeploymentTest
{
    public partial class MainWindow : Window
    {
        UpdateManager manager;
        UpdateInfo updateInfo;
        string installedVersion;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            manager = await UpdateManager.GitHubUpdateManager(@"https://github.com/Vicizlat/SquirrelDeploymentTest");
            AppName.Text = manager.ApplicationName;
            installedVersion = manager.CurrentlyInstalledVersion().ToString();
            Title = "Squirrel Deployment Test v." + installedVersion;
            CurrentVersionTextBox.Text = installedVersion;
        }

        private async void CheckForUpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            updateInfo = await manager.CheckForUpdate();
            UpdateButton.IsEnabled = updateInfo.ReleasesToApply.Count > 0;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await manager.UpdateApp();
            MessageBox.Show($"Succesfuly Updated from v.{installedVersion} to v.{updateInfo.FutureReleaseEntry.Version}!");
            UpdateManager.RestartApp("SquirrelDeploymentTest.exe");
        }
    }
}