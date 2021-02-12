using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.GUITest
{
    public class PropertyObservableTestViewModel : INotifyPropertyChanged
    {
        private bool condition1;
        private bool condition2;

        public PropertyObservableTestViewModel()
        {
            var canExecuteObserverAnd =
                new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1 && this.Condition2);
            TestAndCommand = new ActivatableCanExecuteObserverCommand(() => { }, canExecuteObserverAnd);
            TestAndCommand.Activate();

            var canExecuteObserverOr =
                new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1 || this.Condition2);
            TestOrCommand = new ActivatableCanExecuteObserverCommand(() => { }, canExecuteObserverOr);
            TestOrCommand.Activate();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ActivatableCanExecuteObserverCommand TestAndCommand { get; }

        public ActivatableCanExecuteObserverCommand TestOrCommand { get; }

        public bool Condition1
        {
            get => condition1;
            set
            {
                if (value == condition1) return;
                condition1 = value;
                OnPropertyChanged();
            }
        }

        public bool Condition2
        {
            get => condition2;
            set
            {
                if (value == condition2) return;
                condition2 = value;
                OnPropertyChanged();
            }
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}