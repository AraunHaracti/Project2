using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Project2.ViewModels.LivingSpaceViewModels.ResidentialComplexViewModels;

namespace Project2.Views.LivingSpaceViews.ResidentialComplexViews;

public partial class ResidentialComplexWindow : Window
{
    public ResidentialComplexWindow()
    {
        InitializeComponent();

        DataContext = new ResidentialComplexWindowViewModel(this);
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