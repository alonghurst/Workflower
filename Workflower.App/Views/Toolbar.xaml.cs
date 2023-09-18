namespace Workflower.App.Views;

public partial class Toolbar : ContentView
{
	public Toolbar()
	{
		InitializeComponent();
	}

	private void OnButtonRefreshClicked(object sender, EventArgs e)
	{
		AppShell.Reload();
	}
}