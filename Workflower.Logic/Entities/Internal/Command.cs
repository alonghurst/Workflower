namespace Workflower.Logic.Entities.Internal;

internal class Command : ICommand
{
    public Command(string executable, 
        string arguments, 
        string? workingDirectory, 
        params ICommandValidator[] validators)
    {
        ToolTip = executable;
        Executable = executable;
        Arguments = arguments;
        WorkingDirectory = workingDirectory;

        var valids = validators.Select(x => x.IsValid());

        IsValid = validators.Length == 0 || valids.All(x => x);
    }

    public bool IsValid { get; }

    public string ToolTip { get; internal set; }
    public string Executable { get; }

    public string Arguments { get; }

    public string? WorkingDirectory { get; }

    public virtual bool OpenWithDefaultProgram => false;
}

internal interface ICommandValidator
{
    bool IsValid();
}

internal class FileExistsCommandValidator : ICommandValidator
{
    private readonly string? _path;

    public FileExistsCommandValidator(string? path)
    {
        _path = path;
    }

    public bool IsValid() => !string.IsNullOrWhiteSpace(_path) && File.Exists(_path);
}

internal class ExplorerCommand : Command
{
    public override bool OpenWithDefaultProgram => true;

    public ExplorerCommand(string path) : base("C:/Windows/explorer.exe", path, null)
    {
        ToolTip = "Open in explorer";
    }
}

internal class FileCommand : Command
{
    public override bool OpenWithDefaultProgram => true;

    public FileCommand(string? file, string arguments) : base(file ?? string.Empty, arguments, null, new FileExistsCommandValidator(file))
    {
        ToolTip = $"Open {Path.GetFileName(file)}";
    }

    internal static FileCommand Create(string path, string extension)
    {
        var file = FindFileOfType(path, extension);
        
        return new FileCommand(file, file ?? string.Empty);
    }

    private static string? FindFileOfType(string path, string extension)
    {
        if (!extension.StartsWith("*."))
        {
            extension = $"*.{extension}";
        }

        return System.IO.Directory.GetFiles(path, extension).FirstOrDefault();
    }
}
