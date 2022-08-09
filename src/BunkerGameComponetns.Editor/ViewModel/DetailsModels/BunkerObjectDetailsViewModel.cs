using BunkerGame.GameTypes.BunkerTypes;
using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain.BunkerComponents;
using BunkerGameComponetns.Editor.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    public partial class BunkerObjectDetailsViewModel : DetailsViewModelBase<BunkerObject>
    {
        protected override Control.ComponentEditor CreateEditor()
        {
            var editor = base.CreateEditor();
            editor.ComponentsTypeEditor = GetItemTypeEditor();
            return editor;
        }
        private List<PickerWithLabel> GetItemTypeEditor()
        {
            var picker = new PickerWithLabel()
            {
                Picker = new Picker()
                {
                    ItemsSource = Enum.GetValues(typeof(BunkerObjectType)).Cast<BunkerObjectType>().ToList(),
                    Title = "Тип объекта",
                },
                Title = "Изменить тип объекта",
                BindingContext = this

            };
            Bind(picker.Picker, "GameComponent.BunkerObjectType", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { picker };
        }
    }
}
