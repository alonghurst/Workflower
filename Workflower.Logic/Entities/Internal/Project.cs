namespace Workflower.Logic.Entities.Internal;

internal class Project : IProject
{
    internal Project(string path, Dictionary<string, IDirectory> directories)
    {
        Path = path;
        Directories = directories;
    }

    public string Name => System.IO.Path.GetFileName(Path);

    public string Path { get; }

    public Dictionary<string,IDirectory> Directories { get; }
}