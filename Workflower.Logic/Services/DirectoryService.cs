using Workflower.Logic.Entities;
using Workflower.Logic.Services.Interfaces;
using Directory = Workflower.Logic.Entities.Internal.Directory;

namespace Workflower.Logic.Services;

public class DirectoryService : IDirectoryService
{
    private readonly ICommandService _commandService;
    private readonly IRepositoryService _repositoryService;

    public DirectoryService(ICommandService commandService, IRepositoryService repositoryService)
    {
        _commandService = commandService;
        _repositoryService = repositoryService;
    }

    public IDirectory LoadDirectory(string path)
    {
        var commands = _commandService.LoadCommands(path);
        var repository = new Lazy<IRepository?>(() => _repositoryService.LoadRepository(path));

        return new Directory(path, repository, commands);
    }
}