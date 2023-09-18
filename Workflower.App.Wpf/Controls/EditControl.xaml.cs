using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Workflower.App.Wpf.Controls;

public partial class EditControl : UserControl
{
    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(EditControl));
    public static readonly DependencyProperty ConfirmTooltipProperty = DependencyProperty.Register(nameof(ConfirmTooltip), typeof(string), typeof(EditControl));

    public event EditCompleteHandler? ConfirmClick;
    public event EventHandler? CancelClick;

    public delegate void EditCompleteHandler(object? sender, EventArgs e, string text);

    public new void Focus()
    {
        TextBox.Focus();
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public string ConfirmTooltip
    {
        get => (string)GetValue(ConfirmTooltipProperty);
        set => SetValue(ConfirmTooltipProperty, value);
    }

    private void Confirm(object? sender, EventArgs e)
    {
        if (ConfirmClick != null)
        {
            var text = TextBox.Text;
            TextBox.Text = string.Empty;

            ConfirmClick(sender, e, text);
        }
    }

    public EditControl()
    {
        InitializeComponent();
    }

    private void CancelButton_OnClick(object? sender, EventArgs e)
    {
        Cancel(sender, e);
    }

    private void Cancel(object? sender, EventArgs e)
    {
        if (CancelClick != null)
        {
            CancelClick(sender, e);
        }
    }

    private void ConfirmButton_OnClick(object? sender, EventArgs e)
    {
        Confirm(sender, e);
    }


    private void TextBox_OnKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Confirm(sender, e);
        }
        else if (e.Key == Key.Escape)
        {
            Cancel(sender, e);
        }
    }
}