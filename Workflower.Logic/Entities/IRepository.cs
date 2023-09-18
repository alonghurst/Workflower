namespace Workflower.Logic.Entities;

public interface IRepository : IRepositoryActions
{
    string Path { get; }
    string Branch { get; }
    bool HasUncommittedChanges { get; }
    bool IsUntracked { get; }
    int OutgoingCommits { get; }
    int IncomingCommits { get; }
}