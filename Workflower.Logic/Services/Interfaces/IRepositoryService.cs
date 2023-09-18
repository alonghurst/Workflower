using Workflower.Logic.Entities;

namespace Workflower.Logic.Services.Interfaces;

public interface IRepositoryService
{
    IRepository? LoadRepository(string path);
}