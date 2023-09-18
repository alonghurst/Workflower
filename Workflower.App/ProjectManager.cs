using Workflower.App.Extensions;
using Workflower.Logic;
using Workflower.Logic.Entities;

namespace Workflower.App;

public static class ProjectManager
{
    // ReSharper disable once MemberCanBePrivate.Global
    public static IProject Project { get; private set; }

    public static double LoadDuration { get; private set; }

    private static string _path;
    private static DateTime _lastReload = DateTime.UtcNow;

    public static IProject Load(string path)
    {
        _path = path;
        return Reload();
    }

    public static IProject Reload()
    {
        var sw = new System.Diagnostics.Stopwatch();

        sw.Start();

        Project = ProjectBootstrap.Load(_path);
        _lastReload = DateTime.UtcNow;

        foreach (var projectDirectory in Project.Directories)
        {
            foreach (var command in projectDirectory.Value.Commands)
            {
                IconImageCache.GetBytes(command.Executable);
            }
        }

        sw.Stop();

        LoadDuration = sw.ElapsedMilliseconds;

        return Project;
    }

    public static IDirectory ReloadDirectory(string path)
    {
        var directory = ProjectBootstrap.LoadDirectory(path);

        if (Project.Directories.ContainsKey(path))
        {
            Project.Directories.Remove(path);
        }

        Project.Directories.Add(path, directory);

        return directory;
    }
}