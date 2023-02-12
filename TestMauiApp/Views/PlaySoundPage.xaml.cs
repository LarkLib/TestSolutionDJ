
using Plugin.Maui.Audio;

namespace TestMauiApp.Views;

public partial class PlaySoundPage : ContentPage
{
    public PlaySoundPage()
    {
        InitializeComponent();
    }

    private async void PlaySoundAsync(object sender, EventArgs e)
    {
        var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("2352681572.mp3"));

        audioPlayer.Play();

    }
}