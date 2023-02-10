namespace TestMauiApp.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
    public string ItemId
    {
        set { LoadNote(value); }
    }
    public NotePage()
    {
        InitializeComponent();

        //if (File.Exists(_fileName))
        //    TextEditor.Text = File.ReadAllText(_fileName);
        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));
    }
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        // Save the file.
        //File.WriteAllText(_fileName, TextEditor.Text);
        if (BindingContext is Models.Note note && ((Button)sender).Text.Equals("cancel", StringComparison.OrdinalIgnoreCase))
            File.WriteAllText(note.Filename, TextEditor.Text);

        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        // Delete the file.
        //if (File.Exists(_fileName)) File.Delete(_fileName);
        //TextEditor.Text = string.Empty;

        if (BindingContext is Models.Note note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    private void LoadNote(string fileName)
    {
        Models.Note noteModel = new Models.Note();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }
}