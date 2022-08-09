using BunkerGame.GameTypes.BunkerTypes;
using BunkerGame.GameTypes.GameComponentTypes;
using BunkerGameComponents.Domain.ExternalSurroundings;
using BunkerGameComponetns.Editor.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    public class ExternalSurroundingDetailsViewModel : DetailsViewModelBase<GameExternalSurrounding>
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
                    ItemsSource = Enum.GetValues(typeof(SurroundingType)).Cast<SurroundingType>().ToList(),
                    Title = "Тип окружения",
                },
                Title = "Изменить тип окружения",
                BindingContext = this

            };
            Bind(picker.Picker, "GameComponent.SurroundingType", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { picker };
        }
    }
}
