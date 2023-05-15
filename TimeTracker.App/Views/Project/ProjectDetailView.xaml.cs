using TimeTracker.App.ViewModels.Project;

namespace TimeTracker.App.Views.Project;

public partial class ProjectDetailView 
{
	public ProjectDetailView(ProjectDetailViewModel viewModel) :base(viewModel)
	{
		InitializeComponent();
	}
}