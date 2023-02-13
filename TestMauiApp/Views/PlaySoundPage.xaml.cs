
using Plugin.Maui.Audio;

namespace TestMauiApp.Views;

public partial class PlaySoundPage : ContentPage
{
    private readonly IAudioManager audioManager;
    IAudioPlayer audioPlayer;

    public PlaySoundPage()
    {
        InitializeComponent();
        audioManager = AudioManager.Current;
    }

    private async void PlaySoundAsync(object sender, EventArgs e)
    {
        audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("2352681572.mp3"));
        audioPlayer.Play();
    }
    private void StopSoundAsync(object sender, EventArgs e)
    {
        if (audioPlayer is not null && audioPlayer.IsPlaying)
        {
            audioPlayer.Stop();
        }
    }

    private void VibrateStartButton_Clicked(object sender, EventArgs e)
    {
        int secondsToVibrate = Random.Shared.Next(1, 7);
        TimeSpan vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);
        Vibration.Default.Vibrate(vibrationLength);
    }

    private void VibrateStopButton_Clicked(object sender, EventArgs e) =>
        Vibration.Default.Cancel();

}