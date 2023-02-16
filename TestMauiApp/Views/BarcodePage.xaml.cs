using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;
using Plugin.Maui.Audio;

namespace TestMauiApp.Views;

public partial class BarcodePage : ContentPage
{
    public BarcodePage()
    {
        InitializeComponent();
        barcodeView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };
    }

    protected async void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("detected.mp3")).Play();
        foreach (var barcode in e.Results)
            Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");

        var first = e.Results?.FirstOrDefault();
        if (first is not null)
        {
            Dispatcher.Dispatch(() =>
            {
                barcodeGenerator.ClearValue(BarcodeGeneratorView.ValueProperty);
                barcodeGenerator.Format = first.Format;
                barcodeGenerator.Value = first.Value;
                barcodeLabel.Text = first.Value;
            });
        }
    }

    void SwitchCameraButton_Clicked(object sender, EventArgs e)
    {
        barcodeView.CameraLocation = barcodeView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }

    void TorchButton_Clicked(object sender, EventArgs e)
    {
        barcodeView.IsTorchOn = !barcodeView.IsTorchOn;
    }
}
