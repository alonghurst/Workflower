namespace Workflower.Logic.Entities.Internal;

internal class Repository : IRepository
{
    private readonly IRepositoryActions _actions;

    internal Repository(IRepositoryActions actions, string path, string branch, bool isUntracked, bool hasUncommittedChanges, int outgoingCommits, int incomingCommits)
    {
        _actions = actions;
        Path = path;
        Branch = branch;
        IsUntracked = isUntracked;
        HasUncommittedChanges = hasUncommittedChanges;
        OutgoingCommits = outgoingCommits;
        IncomingCommits = incomingCommits;
    }

    public string Path { get; }
    public string Branch { get; }

    public bool IsUntracked { get; }
    public bool HasUncommittedChanges { get; }
    public int OutgoingCommits { get; }
    public int IncomingCommits { get; }

    public void StageAllAndCommit(string message) => _actions.StageAllAndCommit(message);
    public void Push() => _actions.Push();
    public void Pull() => _actions.Pull();
    public void PullBranchAndCheckoutNew(string branchToPull, string branchToCheckout) => _actions.PullBranchAndCheckoutNew(branchToPull, branchToCheckout);
}