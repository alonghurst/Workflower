using Workflower.App.Extensions;
using Workflower.Logic.Entities;

namespace Workflower.App.Views;

public partial class RepositoryView : ContentView
{
    public bool HasRepository => BindingContext is Lazy<IRepository> { Value: { } };

    public IRepository? Repository => (BindingContext as Lazy<IRepository>)?.Value;

    public RepositoryView()
    {
        InitializeComponent();
    }

    private void SetEditMode(EditMode editMode)
    {
        var parent = this.FindAncestor<DirectoryView>();

        parent?.ViewState.Modify(x => x.EditMode = editMode);
    }

    private void OnButtonCancelCommitClicked(object sender, EventArgs e)
    {
        SetEditMode(EditMode.None);
    }

    private void OnButtonCommitClicked(object sender, EventArgs e)
    {
        SetEditMode(EditMode.Commit);
        CommitMessage.Focus();
    }

    private void OnButtonConfirmCommitClicked(object sender, EventArgs e)
    {
        SetEditMode(EditMode.None);
        Repository?.StageAllAndCommit(CommitMessage.Text);
        Reload();
    }

    private void OnButtonPushClicked(object sender, EventArgs e)
    {
        Repository?.Push();
        Reload();
    }

    private void OnButtonPullClicked(object sender, EventArgs e)
    {
        Repository?.Pull();
        Reload();
    }

    private void OnCommitMessageCompleted(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(CommitMessage.Text))
        {
            SetEditMode(EditMode.None);
            Repository?.StageAllAndCommit(CommitMessage.Text);
            Reload();
        }
    }

    private void Reload()
    {
        if (Repository != null)
        {
            var directory = AppShell.ReloadDirectory(Repository.Path);

            BindingContext = directory.Repository;
        }
    }
}