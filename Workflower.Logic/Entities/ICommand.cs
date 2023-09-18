namespace Workflower.Logic.Entities;

public interface ICommand
{
    public string ToolTip { get; }
    public string Executable { get; }
    public string Arguments { get; }
    public string? WorkingDirectory { get; }
    bool OpenWithDefaultProgram { get; }
}