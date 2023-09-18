using Workflower.Logic.Entities;

namespace Workflower.App;

public partial class AppShell : Shell
{
    public IProject Project => _project;

    private static IProject _project = null!;
    private static AppShell _instance = null!;

    public string LoadDuration => $"Load duration: {ProjectManager.LoadDuration}ms";

	public AppShell()
    {
        _project = ProjectManager.Load("D:/Miiji");
        _instance = this;

        InitializeComponent();
	}

    public static void Reload()
    {
        _project = ProjectManager.Reload();
        _instance.OnPropertyChanged(nameof(Project));
    }

    public static IDirectory ReloadDirectory(string path)
    {
        return ProjectManager.ReloadDirectory(path);
    }
}
