using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain.CharacterComponents;
using BunkerGameComponetns.Editor.Control;
using ComponentEditor = BunkerGameComponetns.Editor.Control.ComponentEditor;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    public class HealthDetailsViewModel : DetailsViewModelBase<CharacterHealth>
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
                    ItemsSource = Enum.GetValues(typeof(HealthType)).Cast<HealthType>().ToList(),
                    Title = "Тип здоровья",
                },
                Title = "Изменить тип здоровя",
                BindingContext = this

            };
            Bind(picker.Picker, "GameComponent.HealthType", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { picker };
        }
    }
}
