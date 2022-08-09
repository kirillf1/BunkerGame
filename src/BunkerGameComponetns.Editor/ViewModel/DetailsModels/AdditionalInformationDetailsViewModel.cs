using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain.CharacterComponents;
using BunkerGameComponetns.Editor.Control;
using ComponentEditor = BunkerGameComponetns.Editor.Control.ComponentEditor;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    public partial class AdditionalInformationDetailsViewModel : DetailsViewModelBase<CharacterAdditionalInformation>
    {
        public AdditionalInformationDetailsViewModel()
        {
            Title = "AddInformation Editor";
        }
        protected override ComponentEditor CreateEditor()
        {
            var editor = base.CreateEditor();
            editor.ComponentsTypeEditor = GetTypeEditor();
            return editor;
        }
        private List<PickerWithLabel> GetTypeEditor()
        {
            var picker = new PickerWithLabel()
            {
                Picker = new Picker()
                {
                    ItemsSource = Enum.GetValues(typeof(AddInfType)).Cast<AddInfType>().ToList(),
                    Title = "Тип доп информации",
                },
                Title = "Изменить тип доп информацию",
                BindingContext = this

            };
            Bind(picker.Picker, "GameComponent.AddInfType", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { picker };
        }
    }
}
