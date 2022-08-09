using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain.CharacterComponents;
using BunkerGameComponetns.Editor.Control;
using ComponentEditor = BunkerGameComponetns.Editor.Control.ComponentEditor;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    public partial class HobbyDetailsViewModel : DetailsViewModelBase<CharacterHobby>
    {
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
                    ItemsSource = Enum.GetValues(typeof(HobbyType)).Cast<HobbyType>().ToList(),
                    Title = "Тип хобби",
                },
                Title = "Изменить тип хобби",
                BindingContext = this
                
            };
            Bind(picker.Picker, "GameComponent.HobbyType", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { picker };
        }
    }
}
