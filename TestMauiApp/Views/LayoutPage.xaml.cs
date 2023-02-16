namespace TestMauiApp.Views;

public partial class LayoutPage : ContentPage
{
    public string Location = "Dispaly Location";
    public LayoutPage()
    {
        InitializeComponent();
    }
    public async Task<string> GetCachedLocation()
    {
        try
        {
            Location location = await Geolocation.Default.GetLastKnownLocationAsync();

            if (location != null)
                return $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // Handle not supported on device exception
            return fnsEx.Message;
        }
        catch (FeatureNotEnabledException fneEx)
        {
            // Handle not enabled on device exception
            return fneEx.Message;
        }
        catch (PermissionException pEx)
        {
            // Handle permission exception
            return pEx.Message;
        }
        catch (Exception ex)
        {
            // Unable to get location
            return ex.Message;
        }

        return "None";
    }

    public async Task NavigateToBuilding25()
    {
        var location = new Location(47.645160, -122.1306032);
        var options = new MapLaunchOptions { Name = "Microsoft Building 25" };

        try
        {
            await Map.Default.OpenAsync(location, options);
        }
        catch (Exception ex)
        {
            // No map application available to open
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }
    private async void GetLocationButton_Clicked(object sender, EventArgs e)
    {
        Location = await GetCachedLocation();
        location.Text = Location;
    }

    private async void SetLocationToMapButton_Clicked(object sender, EventArgs e)
    {
        await NavigateToBuilding25();
    }
}
public class HyperlinkSpan : Span
{
    public static readonly BindableProperty UrlProperty =
        BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkSpan), null);

    public string Url
    {
        get { return (string)GetValue(UrlProperty); }
        set { SetValue(UrlProperty, value); }
    }

    public HyperlinkSpan()
    {
        TextDecorations = TextDecorations.Underline;
        TextColor = Colors.Blue;
        GestureRecognizers.Add(new TapGestureRecognizer
        {
            // Launcher.OpenAsync is provided by Essentials.
            Command = new Command(async () => await Launcher.OpenAsync(Url))
        });
    }
}