using Microsoft.Extensions.DependencyInjection;
using TimeTracker.App.Shells;

namespace TimeTracker.App
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = serviceProvider.GetRequiredService<AppShell>();
        }
    }
}