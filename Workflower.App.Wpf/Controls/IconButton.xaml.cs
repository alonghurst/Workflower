using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Workflower.App.Wpf.Controls;

public partial class IconButton : UserControl
{
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Geometry), typeof(IconButton));

    public event EventHandler? Click;

    public Geometry Icon
    {
        get => (Geometry)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
        
    public IconButton()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (Click != null)
        {
            Click(this, EventArgs.Empty);
        }
    }
}