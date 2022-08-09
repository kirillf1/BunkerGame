using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain.CharacterComponents;
using BunkerGameComponetns.Editor.Control;
using ComponentEditor = BunkerGameComponetns.Editor.Control.ComponentEditor;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{

    public partial class TraitDetailsViewModel : DetailsViewModelBase<CharacterTrait>
    {

        public TraitDetailsViewModel()
        {
            Title = "Trait Editor";
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
                    ItemsSource = Enum.GetValues(typeof(TraitType)).Cast<TraitType>().ToList(),
                    Title = "Тип черты характера",
                },
                Title = "Изменить тип черты характера",
                BindingContext = this

            };
            Bind(picker.Picker, "GameComponent.TraitType", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { picker };
        }
    }
}
