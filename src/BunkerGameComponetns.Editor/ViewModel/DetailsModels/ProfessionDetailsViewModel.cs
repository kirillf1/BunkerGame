using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain.CharacterComponents;
using BunkerGameComponetns.Editor.Control;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{

    public partial class ProfessionDetailsViewModel : DetailsViewModelBase<CharacterProfession>
    {
       
        protected override Control.ComponentEditor CreateEditor()
        {
            var editor = base.CreateEditor();
            editor.ComponentsTypeEditor = GetTypeEditor();
            return editor;
        }
        private List<PickerWithLabel> GetTypeEditor()
        {
            var pickerSkill = new PickerWithLabel()
            {
                Picker = new Picker()
                {
                    ItemsSource = Enum.GetValues(typeof(ProfessionSkill)).Cast<ProfessionSkill>().ToList(),
                    Title = "Вид навыка",
                },
                Title = "Изменить навык",
                BindingContext = this

            };
            var pickerType = new PickerWithLabel()
            {
                Picker = new Picker()
                {
                    ItemsSource = Enum.GetValues(typeof(ProfessionType)).Cast<ProfessionType>().ToList(),
                    Title = "Тип профессии",
                },
                Title = "Изменить тип профессии",
                BindingContext = this

            };
            Bind(pickerSkill.Picker, "GameComponent.ProfessionSkill", Picker.SelectedItemProperty);
            Bind(pickerType.Picker,"GameComponent.ProfessionType",Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { pickerType,pickerSkill };
        }
    }
}
