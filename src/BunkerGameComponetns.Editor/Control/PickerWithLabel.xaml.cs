namespace BunkerGameComponetns.Editor.Control;

public partial class PickerWithLabel : ContentView
{
    public static readonly BindableProperty TitleBindProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(PickerWithLabel), string.Empty);
    public static readonly BindableProperty PickerBindProperty = BindableProperty.Create(nameof(Picker), typeof(Picker), typeof(PickerWithLabel), new Picker());
    public string Title
    {
        get => (string)GetValue(TitleBindProperty);
        set => SetValue(TitleBindProperty, value);
    }
    public Picker Picker
    {
        get => (Picker)GetValue(PickerBindProperty);
        set => SetValue(PickerBindProperty, value);
    }
    public PickerWithLabel()
	{
		InitializeComponent();
	}
}