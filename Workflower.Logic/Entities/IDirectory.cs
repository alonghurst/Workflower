namespace Workflower.Logic.Entities;

public interface IDirectory
{
    string Name { get; }
    string Path { get; }
    Lazy<IRepository?> Repository { get; }
    IReadOnlyCollection<ICommand> Commands { get; }
}