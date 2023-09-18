namespace Workflower.Logic.Entities.Internal;

internal class Directory : IDirectory
{
    internal Directory(string path, Lazy<IRepository?> repository, IReadOnlyCollection<ICommand> commands)
    {
        Path = path;
        Repository = repository;
        Commands = commands;
    }

    public string Name => System.IO.Path.GetFileName(Path);

    public string Path { get; }

    public Lazy<IRepository?> Repository { get; }

    public IReadOnlyCollection<ICommand> Commands { get; }
}