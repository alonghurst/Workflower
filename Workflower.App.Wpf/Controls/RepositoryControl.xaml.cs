using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Humanizer;
using Workflower.App.Wpf.Extensions;
using Workflower.Logic.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace Workflower.App.Wpf.Controls;

public partial class RepositoryControl : UserControl, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public Visibility RepositoryVisibility => HasRepository ? Visibility.Visible : Visibility.Collapsed;

    public Visibility HasEdits => HasRepository && Repository!.HasUncommittedChanges ? Visibility.Visible : Visibility.Collapsed;
    public Visibility HasIncoming => HasRepository && Repository!.IncomingCommits > 0 ? Visibility.Visible : Visibility.Collapsed;
    public Visibility HasOutgoing => HasRepository && (Repository!.IsUntracked || Repository!.OutgoingCommits > 0) ? Visibility.Visible : Visibility.Collapsed;

    public string IncomingToolTip => HasRepository ? $"Pull {"commit".ToQuantity(Repository!.IncomingCommits)} from origin" : string.Empty;
    public string OutgoingToolTip => HasRepository ?
            Repository!.IsUntracked ? "Push branch to origin" :
                $"Push {"commit".ToQuantity(Repository!.OutgoingCommits)} to origin"
                    : string.Empty;

    public bool HasRepository => Repository != null;

    public IRepository? Repository { get; set; }


    public RepositoryControl()
    {
        InitializeComponent();
    }
    private void SetEditMode(EditMode mode)
    {
        var parent = Parent.FindAncestor<DirectoryControl>();
        if (parent != null)
        {
            parent.EditMode = mode;
        }
    }

    private void RepositoryControl_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        Repository = DataContext as IRepository;
    }

    private void ButtonCommit_OnMouseLeftButtonUp(object sender, EventArgs e)
    {
        SetEditMode(EditMode.Commit);
        EditCommit.Focus();
    }

    private void ButtonBranch_OnMouseLeftButtonUp(object sender, EventArgs e)
    {
        SetEditMode(EditMode.Branch);
        EditBranch.Focus();
    }

    private void EditControl_OnCancelClick(object? sender, EventArgs e)
    {
        SetEditMode(EditMode.None);
    }

    private void CommitEditControl_OnConfirmClick(object? sender, EventArgs e, string text)
    {
        SetEditMode(EditMode.None);

        Repository?.StageAllAndCommit(text);
        Reload();
    }

    private void BranchEditControl_OnConfirmClick(object? sender, EventArgs e, string text)
    {
        SetEditMode(EditMode.None);

        Repository?.PullBranchAndCheckoutNew("main", text);
        Reload();
    }

    private void Reload()
    {
        if (Repository != null)
        {
            var directory = ProjectManager.ReloadDirectory(Repository.Path);

            DataContext = directory.Repository.Value;
        }
    }

    private void PushButton_OnClick(object? sender, EventArgs e)
    {
        Repository?.Push();
        Reload();
    }

    private void PullButton_OnClick(object? sender, EventArgs e)
    {
        Repository?.Pull();
        Reload();
    }

    private void Label_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        Reload();
    }
}