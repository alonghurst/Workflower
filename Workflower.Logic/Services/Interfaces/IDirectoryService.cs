using Workflower.Logic.Entities;

namespace Workflower.Logic.Services.Interfaces;

public interface IDirectoryService
{
    IDirectory LoadDirectory(string path);
}