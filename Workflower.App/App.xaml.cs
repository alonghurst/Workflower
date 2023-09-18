namespace Workflower.App;

public partial class App : Application
{
    public App()
    {
        Current.UserAppTheme = AppTheme.Dark;

        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Window window = base.CreateWindow(activationState);

        window.Activated += (s, e) =>
        {
            //AppShell.Reload();
        };

        return window;
    }
}
