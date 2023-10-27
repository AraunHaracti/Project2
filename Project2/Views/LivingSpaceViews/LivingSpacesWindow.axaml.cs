using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Project2.ViewModels;

namespace Project2.Views;

public partial class LivingSpacesWindow : Window
{
    public LivingSpacesWindow()
    {
        InitializeComponent();

        DataContext = new LivingSpacesWindowViewModel(this);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}