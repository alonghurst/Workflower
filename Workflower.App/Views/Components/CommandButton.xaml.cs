using System.Diagnostics;
using Workflower.Logic.Entities;
using Workflower.App.Extensions;

namespace Workflower.App.Views.Components;
public partial class CommandButton : ContentView
{
    public ICommand? Command => BindingContext as ICommand;

    public ImageSource? Source => Command != null ? ImageSource.FromStream(CreateIconStream) : null;

    public CommandButton()
    {
        InitializeComponent();

    }

    private MemoryStream CreateIconStream()
    {
        if (Command == null)
        {
            return null;
        }

        var bytes = IconImageCache.GetBytes(Command.Executable);


        return new MemoryStream(bytes);
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (Command != null)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Command.Executable,
                Arguments = Command.Arguments
            };

            if (Command.OpenWithDefaultProgram)
            {
                startInfo = new ProcessStartInfo
                {
                    FileName = Command.Arguments,
                    UseShellExecute = true,
                    Verb = "open"
                };
            }

            if (!string.IsNullOrWhiteSpace(Command.WorkingDirectory))
            {
                startInfo.WorkingDirectory = Command.WorkingDirectory;
            }

            using Process fileopener = new Process
            {
                StartInfo = startInfo
            };

            fileopener.Start();
        }
    }
}