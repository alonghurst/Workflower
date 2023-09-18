using LibGit2Sharp;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Workflower.App.Wpf.Controls;

public partial class DirectoryControl : UserControl, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public EditMode EditMode { get; set; } = EditMode.None;

    public Visibility IsNotEdit => EditMode == EditMode.None ? Visibility.Visible : Visibility.Collapsed;
    public Visibility IsCommitEdit => EditMode == EditMode.Commit ? Visibility.Visible : Visibility.Collapsed;
    public Visibility IsBranchEdit => EditMode == EditMode.Branch ? Visibility.Visible : Visibility.Collapsed;

    public DirectoryControl()
    {
        InitializeComponent();
    }
}

public enum EditMode
{
    None,
    Commit,
    Branch
}