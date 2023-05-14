using TimeTracker.App.ViewModels;

namespace TimeTracker.App.Views;

public partial class ContentBasePage
{
    protected IViewModel ViewModel { get; }

    public ContentBasePage(IViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await ViewModel.OnAppearingAsync();
    }
}