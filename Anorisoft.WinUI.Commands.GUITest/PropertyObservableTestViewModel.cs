using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Interfaces;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.GUITest
{
    public class PropertyObservableTestViewModel : INotifyPropertyChanged
    {
        private bool condition1;
        private bool condition2;
        public event PropertyChangedEventHandler PropertyChanged;

        public PropertyObservableTestViewModel()
        {

            var canExecuteObserverAnd = new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1 && this.Condition2);
            TestAndCommand = new ActivatableCanExecuteObserverCommand(() => { }, canExecuteObserverAnd);
            TestAndCommand.CanExecuteChanged += TestCommandOnCanExecuteChanged;
            TestAndCommand.Activate();

            var canExecuteObserverOr = new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1 || this.Condition2);
            TestOrCommand = new ActivatableCanExecuteObserverCommand(() => { }, canExecuteObserverOr);
            TestOrCommand.CanExecuteChanged += TestCommandOnCanExecuteChanged;
            TestOrCommand.Activate();

        }

        private void TestCommandOnCanExecuteChanged(object sender, EventArgs e)
        {
        }

        public ActivatableCanExecuteObserverCommand TestAndCommand
        {
            get;
        }

        public ActivatableCanExecuteObserverCommand TestOrCommand
        {
            get;
        }

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
