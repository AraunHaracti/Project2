using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Project2.Views.LivingSpaceViews.ResidentialComplexViews;

public partial class ResidentialComplexWindow : Window
{
    public ResidentialComplexWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}