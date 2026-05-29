using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace CodeScanAI;

public partial class App : Application
{
	public App()
	{
			InitializeComponent();

			// Follow the device (system) theme by default
			Application.Current.UserAppTheme = AppTheme.Unspecified;

			// Optional: handle theme changes if you need to react at runtime
			Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
	}

		void OnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
		{
			// You can react to theme changes here if necessary.
		}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}