
namespace BunkerGameComponetns.Editor.Control;

public partial class ComponentEditor : ContentView
{
    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(ComponentEditor), string.Empty);
    public static readonly BindableProperty ComponentsTypeEditorProperty = BindableProperty.Create(nameof(ComponentsTypeEditor), typeof(List<PickerWithLabel>), typeof(ComponentEditor), new List<PickerWithLabel>());
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(ComponentEditor), new double());
    public List<PickerWithLabel> ComponentsTypeEditor
    {
        get => (List<PickerWithLabel>)GetValue(ComponentsTypeEditorProperty);
        set => SetValue(ComponentsTypeEditorProperty, value);
    }
    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
    public ComponentEditor()
	{
		InitializeComponent();
	}
}