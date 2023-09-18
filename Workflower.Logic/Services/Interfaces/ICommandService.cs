using Workflower.Logic.Entities;

namespace Workflower.Logic.Services.Interfaces;

public interface ICommandService
{
    IReadOnlyCollection<ICommand> LoadCommands(string path);
}