using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModuleC.Views;

public partial class TabCView : UserControl
{
    public TabCView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}