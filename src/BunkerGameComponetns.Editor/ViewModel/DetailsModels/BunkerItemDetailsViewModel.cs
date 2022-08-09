using BunkerGame.GameTypes.BunkerTypes;
using BunkerGameComponents.Domain.BunkerComponents;
using BunkerGameComponetns.Editor.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    public class BunkerItemDetailsViewModel : DetailsViewModelBase<ItemBunker>
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
                    ItemsSource = Enum.GetValues(typeof(ItemBunkerType)).Cast<ItemBunkerType>().ToList(),
                    Title = "Тип предмета",
                },
                Title = "Изменить тип предмета",
                BindingContext = this

            };
            Bind(picker.Picker, "GameComponent.ItemBunkerType", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { picker };
        }
    }
}
