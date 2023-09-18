using System.IO;
using System.Windows;
using Workflower.Logic.Entities;
namespace Workflower.App.Wpf;

public partial class MainWindow : Window
{
    public IProject Project => _project;

    private static IProject _project = null!;
    private static MainWindow _instance = null!;

    public string LoadDuration => $"Load duration: {ProjectManager.LoadDuration}ms";

    public MainWindow()
    {
        _project = ProjectManager.Load(Directory.Exists("D:/Miiji") ? "D:/Miiji" : "C:/users/andy/git/Miiji");
        _instance = this;

        DataContext = Project;

        InitializeComponent();

        Title = $"Workflower: {Project.Name}";
    }

    public static void Reload(string? path = null)
    {
        var old = _project;

        if (string.IsNullOrWhiteSpace(path))
        {
            _project = ProjectManager.Reload();
        }
        else
        {
            _project = ProjectManager.Load(path);
        }
        // TODO: notify
        //_instance.OnPropertyChanged(nameof(Project)));

        _instance.DataContext = _project;
    }

    public static IDirectory ReloadDirectory(string path)
    {
        return ProjectManager.ReloadDirectory(path);
    }
}