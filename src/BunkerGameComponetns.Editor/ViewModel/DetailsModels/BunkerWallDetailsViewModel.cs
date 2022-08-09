using BunkerGame.GameTypes.BunkerTypes;
using BunkerGame.GameTypes.CharacterTypes;
using BunkerGame.GameTypes.GameComponentTypes;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Domain.BunkerComponents;
using BunkerGameComponents.Domain.Catastrophes;
using BunkerGameComponetns.Editor.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    public partial class BunkerWallDetailsViewModel : DetailsViewModelBase<BunkerWall>
    {
       
        public BunkerWallDetailsViewModel()
        {
            Title = "BunkerWall Update";
        }
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
                    ItemsSource = Enum.GetValues(typeof(BunkerState)).Cast<BunkerState>().ToList(),
                    Title = "Состояение бункера",
                },
                Title = "Изменить состояние бункера",
                BindingContext = this

            };
            Bind(picker.Picker,"GameComponent.BunkerState", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { picker };
        }
    }
}
