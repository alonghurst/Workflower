using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Workflower.App.Wpf.Controls;

public partial class CommandButton : UserControl
{
    public CommandButton()
    {
        InitializeComponent();
    }
    
    public ImageSource? ImageSource
    {
        get
        {
            var command = DataContext as Workflower.Logic.Entities.ICommand;

            if (command == null)
            {
                return null;
            }

            var icon = System.Drawing.Icon.ExtractAssociatedIcon(command.Executable);

            if (icon == null)
            {
                return null;
            }

            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var command = DataContext as Workflower.Logic.Entities.ICommand;

        if (command == null)
        {
            return ;
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = command.Executable,
            Arguments = command.Arguments
        };

        if (command.OpenWithDefaultProgram)
        {
            startInfo = new ProcessStartInfo
            {
                FileName = command.Arguments,
                UseShellExecute = true,
                Verb = "open"
            };
        }

        if (!string.IsNullOrWhiteSpace(command.WorkingDirectory))
        {
            startInfo.WorkingDirectory = command.WorkingDirectory;
        }

        using Process process = new Process
        {
            StartInfo = startInfo
        };

        process.Start();
    }
}