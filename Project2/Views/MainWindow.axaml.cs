using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Project2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        Database.ConnectionStringBuilder = new()
        {
            Server = "localhost",
            Port = 3306,
            Database = "ResCom",
            UserID = "root",
            Password = "password"
        };
    }

    private void LivingSpaceButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var livingSpacesWindow = new LivingSpaceViews.LivingSpacesWindow();
        livingSpacesWindow.ShowDialog(this);
    }
}