namespace CodeScanAI.Views;

[QueryProperty(nameof(Result), "Result")]
[QueryProperty(nameof(Title), "Title")]
public partial class ResultPage : ContentPage
{
    public string Result { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public ResultPage() => InitializeComponent();

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        BindingContext = this;
    }

    async void OnCopyClicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(Result);
        await DisplayAlert("Listo", "Resultado copiado", "OK");
    }
}