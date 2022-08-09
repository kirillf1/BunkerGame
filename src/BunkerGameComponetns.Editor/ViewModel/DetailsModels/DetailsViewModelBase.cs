using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentEditor = BunkerGameComponetns.Editor.Control.ComponentEditor;

namespace BunkerGameComponetns.Editor.ViewModel.DetailsModels
{
    [QueryProperty(nameof(GameComponent), nameof(GameComponent))]
    public abstract partial class DetailsViewModelBase<T> : BaseViewModel where T : IGameComponent
    {
        protected T gameComponent;
        public T GameComponent 
        { 
            get => gameComponent;
            set
            {
                gameComponent = value;
                OnPropertyChanged(nameof(GameComponent));
            }
        }
        public ComponentEditor ComponentEditor { get => CreateEditor(); }
        protected virtual ComponentEditor CreateEditor()
        {
            var editor = new ComponentEditor();
            Bind(editor, "GameComponent.Value", ComponentEditor.ValueProperty);
            Bind(editor, "GameComponent.Description", ComponentEditor.DescriptionProperty);
            return editor;
        }
        protected void Bind(BindableObject bindableObject, string path, BindableProperty bindableProperty)
        {
            var binding = new Binding
            {
                Source = this,
                Path = path,
                Mode = BindingMode.TwoWay
            };
            bindableObject.SetBinding(bindableProperty, binding);
        }
    }
}
