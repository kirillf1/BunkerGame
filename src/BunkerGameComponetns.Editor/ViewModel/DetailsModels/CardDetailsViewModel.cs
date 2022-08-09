using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain.CharacterComponents.Cards;
using BunkerGameComponetns.Editor.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    public partial class CardDetailsViewModel : DetailsViewModelBase<CharacterCard>
    {
        private int? coponentId;
        public int? ComponentId
        {
            get
            {
                if (GameComponent?.CardMethod?.ItemId != null && GameComponent.CardMethod.ItemId != null)
                    return GameComponent.CardMethod.ItemId.Id;
                return coponentId;
                    
            }
            set
            {
                if (coponentId != null && coponentId <= 0)
                    return;
                coponentId = value;
                GameComponent.CardMethod.ItemId = new ComponentId(coponentId.Value);
            }
        }
        protected override Control.ComponentEditor CreateEditor()
        {
            var editor = base.CreateEditor();
            editor.ComponentsTypeEditor = GetTypeEditor();
            return editor;
        }
        private List<PickerWithLabel> GetTypeEditor()
        {
            var pickerMethodType = new PickerWithLabel()
            {
                Picker = new Picker()
                {
                    ItemsSource = Enum.GetValues(typeof(MethodType)).Cast<MethodType>().ToList(),
                    Title = "Тип карты",
                },
                Title = "Изменить тип карты",
                BindingContext = this

            };
            var pickerDirection = new PickerWithLabel()
            {
                Picker = new Picker()
                {
                    ItemsSource = Enum.GetValues(typeof(MethodDirection)).Cast<MethodDirection>().ToList(),
                    Title = "Направление карты",
                },
                Title = "Изменить направление карты",
                BindingContext = this

            };
            Bind(pickerMethodType.Picker, "GameComponent.CardMethod.MethodType", Picker.SelectedItemProperty);
            Bind(pickerDirection.Picker, "GameComponent.CardMethod.MethodDirection", Picker.SelectedItemProperty);
            return new List<PickerWithLabel> { pickerDirection, pickerMethodType };
        }
    }
}
