using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModuleB.Views;

public partial class TabBView : UserControl
{
    public TabBView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}