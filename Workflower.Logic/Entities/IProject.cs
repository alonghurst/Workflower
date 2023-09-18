namespace Workflower.Logic.Entities;

public interface IProject
{
    string Name { get; }
    string Path { get; }
    Dictionary<string, IDirectory> Directories { get; }
}