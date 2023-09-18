using Workflower.Logic.Entities;
using Workflower.Logic.Entities.Internal;
using Workflower.Logic.Services.Interfaces;
using Directory = System.IO.Directory;

namespace Workflower.Logic.Services;

public class CommandService : ICommandService
{
    public IReadOnlyCollection<ICommand> LoadCommands(string path)
    {
        // TODO: user settings

        // Installation folders eg for windows, program files etc
        // Configured commands

        // TODO: these should come from a configurable setting 
        var vscode = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "Local", "Programs", "Microsoft VS Code", "Code.exe");

        var node = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "nodejs", "node.exe");
        
        IEnumerable<Command> CreateCommands()
        {
            yield return new ExplorerCommand(path);
            yield return new Command(vscode, path, null, new FileExistsCommandValidator(vscode))
            {
                ToolTip = "Open with VSCode"
            };

            yield return FileCommand.Create(path, "sln");
            yield return FileCommand.Create(path, "uproject");
        }

        return CreateCommands().Where(x => x.IsValid).ToArray();
    }

    private string? FindFileOfType(string path, string extension)
    {
        if (!extension.StartsWith("*."))
        {
            extension = $"*.{extension}";
        }

        return Directory.GetFiles(path, extension).FirstOrDefault();
    }
}