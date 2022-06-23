using System.Windows;
using Squirrel;

namespace SquirrelDeploymentTest
{
    public partial class MainWindow : Window
    {
        UpdateManager manager;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            manager = await UpdateManager.GitHubUpdateManager(@"https://github.com/Vicizlat/SquirrelDeploymentTest");

            Title = "Squirrel Deployment Test v." + manager.CurrentlyInstalledVersion().ToString();
            CurrentVersionTextBox.Text = manager.CurrentlyInstalledVersion().ToString();
            InstallDir.Text = manager.RootAppDirectory;
        }

        private async void CheckForUpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateInfo updateInfo = await manager.CheckForUpdate();
            UpdateButton.IsEnabled = updateInfo.ReleasesToApply.Count > 0;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await manager.UpdateApp();
            MessageBox.Show("Updated succesfuly!");
        }
    }
}