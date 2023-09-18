namespace Workflower.Logic.Entities;

public interface IRepositoryActions
{
    void PullBranchAndCheckoutNew(string branchToPull, string branchToCheckout);
    void StageAllAndCommit(string message);
    void Push();
    void Pull();
}