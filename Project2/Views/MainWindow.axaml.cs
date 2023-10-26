using Avalonia.Controls;

namespace Project2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        Database.ConnectionStringBuilder = new()
        {
            Server = "10.10.1.24",
            Port = 3306,
            Database = "pro1_12",
            UserID = "user_01",
            Password = "user01pro"
        };
    }
}