using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModuleA.Views;

public partial class TabAView : UserControl
{
    public TabAView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}