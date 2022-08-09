using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain.CharacterComponents;
using BunkerGameComponetns.Editor.Control;
using ComponentEditor = BunkerGameComponetns.Editor.Control.ComponentEditor;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
 
    public partial class CharacterItemDetailsViewModel : DetailsViewModelBase<CharacterItem>
    {
       

        public CharacterItemDetailsViewModel()
        {
            Title = "Character item update";
            
        }
        protected override ComponentEditor CreateEditor()
        {
            var editor = new ComponentEditor
            {
                ComponentsTypeEditor = GetItemTypeEditor()
            };
            Bind(editor, "GameComponent.Value", ComponentEditor.ValueProperty);
            Bind(editor, "GameComponent.Description", ComponentEditor.DescriptionProperty);
            return editor;
        }
        
        private List<PickerWithLabel> GetItemTypeEditor()
        {
            var picker = new PickerWithLabel()
            {
                Picker = new Picker()
                {
                    ItemsSource = Enum.GetValues(typeof(CharacterItemType)).Cast<CharacterItemType>().ToList(),
                    Title = "Тип предмета",
                },
                Title = "Изменить тип предмета",
                BindingContext = this
                
            };
            Bind(picker.Picker, "GameComponent.CharacterItemType", Picker.SelectedItemProperty); 
            return new List<PickerWithLabel> { picker };
        }

        
    }
}
