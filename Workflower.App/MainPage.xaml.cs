namespace Workflower.App;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
	}

	private void OnButtonClicked(object sender, EventArgs e)
    {
        ProjectManager.Reload();
    }
}

