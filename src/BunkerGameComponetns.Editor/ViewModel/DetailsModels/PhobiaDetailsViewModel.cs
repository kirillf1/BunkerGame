using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain.CharacterComponents;
using BunkerGameComponetns.Editor.Control;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    public class PhobiaDetailsViewModel : DetailsViewModelBase<CharacterPhobia>
    {
        protected override Control.ComponentEditor CreateEditor()
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
                    ItemsSource = Enum.GetValues(typeof(PhobiaDebuffType)).Cast<PhobiaDebuffType>().ToList(),
                    Title = "Тип фобии",
                },
                Title = "Изменить тип фобии",
                BindingContext = this

            };
            Bind(picker.Picker, "GameComponent.PhobiaDebuffType", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { picker };
        }
    }
}
