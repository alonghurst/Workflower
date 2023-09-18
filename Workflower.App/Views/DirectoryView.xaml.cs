using System.ComponentModel;

namespace Workflower.App.Views;

public partial class DirectoryView : ContentView
{
    [Bindable(true)]
    public DirectoryViewState ViewState { get; private set; }

    public DirectoryView()
	{
		InitializeComponent();

        ViewState = new DirectoryViewState(() =>
        {
            OnPropertyChanged(nameof(ViewState));
        });
    }
}

public class DirectoryViewState
{
    public EditMode EditMode { get; set; } = EditMode.None;

    private readonly Action _notify;

    public DirectoryViewState(Action notify)
    {
        _notify = notify;
    }

    public void Modify(Action<DirectoryViewState> modify)
    {
        modify(this);
        _notify();
    }
}

public enum EditMode
{
    None,
    Commit,
    Branch
}