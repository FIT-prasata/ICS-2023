using TimeTracker.App.ViewModels.User;

namespace TimeTracker.App.Views.User;

public partial class UserDetailView
{
	public UserDetailView(UserDetailViewModel viewModel)
	    :base(viewModel)
	{
		InitializeComponent();
	}
}