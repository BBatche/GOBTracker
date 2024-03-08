using GOBTrackerUI.APIMethods;

namespace GOBTrackerUI;

public partial class StatsModePage : ContentPage
{

    public ApiService apiService;
	public StatsModePage()
	{
		InitializeComponent();
        apiService = new ApiService();
	}

    private async void OnSearchButtonPressed(object sender, EventArgs e)
    {
        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            string lastName = ((SearchBar)sender).Text;

        //call api method
        var stats = await apiService.GetRawStatsAsync(lastName);

        MainThread.BeginInvokeOnMainThread(() => { playerStatCollectionView.ItemsSource = stats; });
        }
        else
        {
            await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
            return;
        }
    }
}