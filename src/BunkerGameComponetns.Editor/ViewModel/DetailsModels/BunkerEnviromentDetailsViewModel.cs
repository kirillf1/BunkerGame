using BunkerGame.GameTypes.BunkerTypes;
using BunkerGameComponents.Domain.BunkerComponents;
using BunkerGameComponetns.Editor.Control;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    public partial class BunkerEnviromentDetailsViewModel : DetailsViewModelBase<BunkerEnviroment>
    {
        public BunkerEnviromentDetailsViewModel()
        {
            Title = "BunkerEnviroment editor";
        }
        protected override Control.ComponentEditor CreateEditor()
        {
            var editor = base.CreateEditor();
            editor.ComponentsTypeEditor = GetTypeEditor();
            return editor;
        }
        private List<PickerWithLabel> GetTypeEditor()
        {
            var pickerType = new PickerWithLabel()
            {
                Picker = new Picker()
                {
                    ItemsSource = Enum.GetValues(typeof(EnviromentType)).Cast<EnviromentType>().ToList(),
                    Title = "Тип сущности",
                },
                Title = "Изменить тип сущности",
                BindingContext = this

            };
            var pickerBehavior = new PickerWithLabel()
            {
                Picker = new Picker()
                {
                    ItemsSource = Enum.GetValues(typeof(EnviromentBehavior)).Cast<EnviromentBehavior>().ToList(),
                    Title = "Поведение сущности",
                },
                Title = "Изменить поведение сущности",
                BindingContext = this

            };
            Bind(pickerType.Picker, "GameComponent.EnviromentType", Picker.SelectedItemProperty);
            Bind(pickerBehavior.Picker, "GameComponent.EnviromentBehavior", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { pickerType, pickerBehavior };
        }
    }
}
