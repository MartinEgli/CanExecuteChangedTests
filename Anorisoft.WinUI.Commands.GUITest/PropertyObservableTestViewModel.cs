using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Anorisoft.WinUI.Commands.Builder;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Commands;
using Anorisoft.WinUI.Commands.Interfaces;
using Anorisoft.WinUI.Common;
using JetBrains.Annotations;
using ICommand = System.Windows.Input.ICommand;

namespace Anorisoft.WinUI.Commands.GUITest
{
    public class PropertyObservableTestViewModel : INotifyPropertyChanged
    {
        private bool condition1;
        private bool condition2;

        public PropertyObservableTestViewModel()
        {
            var commandFactory = CommandBuilder.Builder;
            //var canExecuteObserverAnd =
            //    new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1 && this.Condition2);
            //TestAndCommand = new ActivatableCanExecuteObserverCommand(() => { }, canExecuteObserverAnd);

            Action syncExecution = () => { };
            Action<CancellationToken> concurrencySyncExecution = (c) => { };
            Func<CancellationToken, Task> concurrencyAsyncExecution = async (c) => await Task.Yield();

            IConcurrencyAsyncCommand testAndCommand;
            TestAndCommand = testAndCommand = commandFactory
                .Command(concurrencyAsyncExecution)
                .ObservesCanExecute(() => this.Condition1 && this.Condition2)
                .Build();

            ((IActivatable)testAndCommand).Activate();

            var canExecuteObserverOr =
                new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1 || this.Condition2);
            TestOrCommand = new ActivatableCanExecuteObserverCommand(() => { }, canExecuteObserverOr);
            TestOrCommand.Activate();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand TestAndCommand { get; }

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