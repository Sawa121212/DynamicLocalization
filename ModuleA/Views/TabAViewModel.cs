using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;

namespace ModuleA.Views
{
    public class TabAViewModel : BindableBase
    {
        private string _message;
        private int _value;

        public TabAViewModel()
        {
            Message = "View A from your Prism Module";
            IncrementCommand = new DelegateCommand(() => { Value++; });
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public ICommand IncrementCommand { get; }
    }
}