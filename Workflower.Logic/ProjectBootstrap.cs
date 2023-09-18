using Workflower.Logic.Entities;
using Workflower.Logic.Entities.Internal;
using Workflower.Logic.Services;
using Workflower.Logic.Services.Interfaces;
using Directory = System.IO.Directory;

namespace Workflower.Logic;

public static class ProjectBootstrap
{
    private static readonly IDirectoryService DirectoryService;

    static ProjectBootstrap()
    {
        var commandService = new CommandService();
        var repositoryService = new RepositoryService();

        DirectoryService = new DirectoryService(commandService, repositoryService);
    }
    
    public static IProject Load(string path)
    {
        var directoryPaths = Directory.GetDirectories(path);
        var directories = directoryPaths.Select(LoadDirectory);

        var project = new Project(path, directories.ToDictionary(x => x.Path, x => x));

        return project;
    }

    public static IDirectory LoadDirectory(string path)
    {
        return DirectoryService.LoadDirectory(path);
    }
}