using LibGit2Sharp;
using Workflower.Logic.Entities.Internal;
using Workflower.Logic.Services.Interfaces;
using IRepository = Workflower.Logic.Entities.IRepository;
using Repository = LibGit2Sharp.Repository;

namespace Workflower.Logic.Services;

public class RepositoryService : IRepositoryService
{
    public IRepository? LoadRepository(string path)
    {
        if (Repository.IsValid(path))
        {
            using (var repository = new LibGit2Sharp.Repository(path))
            {
                var branchName = repository.Head.FriendlyName;
                var isUntracked = !repository.Head.TrackingDetails.AheadBy.HasValue || !repository.Head.TrackingDetails.BehindBy.HasValue;

                var status = repository.RetrieveStatus();

                var actions = new RepositoryActions(path);

                return new Entities.Internal.Repository(actions, path, branchName ?? "N/A", isUntracked, status.IsDirty, repository.Head.TrackingDetails.AheadBy ?? 0, repository.Head.TrackingDetails.BehindBy ?? 0);
            }
        }

        return null;
    }
}