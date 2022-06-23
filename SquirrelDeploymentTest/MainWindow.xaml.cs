using System.Windows;
using Squirrel;

namespace SquirrelDeploymentTest
{
    public partial class MainWindow : Window
    {
        UpdateManager manager;
        UpdateInfo updateInfo;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            manager = await UpdateManager.GitHubUpdateManager(@"https://github.com/Vicizlat/SquirrelDeploymentTest");

            Title = "Squirrel Deployment Test v." + manager.CurrentlyInstalledVersion().ToString();
            CurrentVersionTextBox.Text = manager.CurrentlyInstalledVersion().ToString();
            AppName.Text = manager.ApplicationName;
        }

        private async void CheckForUpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            updateInfo = await manager.CheckForUpdate();
            UpdateButton.IsEnabled = updateInfo.ReleasesToApply.Count > 0;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await manager.UpdateApp();
            MessageBox.Show("Updated succesfuly!");
        }
    }
}