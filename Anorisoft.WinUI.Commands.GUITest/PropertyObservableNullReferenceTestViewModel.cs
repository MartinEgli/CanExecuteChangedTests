using System.ComponentModel;
using System.Runtime.CompilerServices;
using Anorisoft.ExpressionObservers;
using Anorisoft.WinUI.Commands.CanExecuteObservers;
using Anorisoft.WinUI.Commands.Factory;
using Anorisoft.WinUI.Commands.Interfaces;
using JetBrains.Annotations;

namespace Anorisoft.WinUI.Commands.GUITest
{
    public class PropertyObservableNullReferenceTestViewModel : INotifyPropertyChanged
    {

        public PropertyObservableNullReferenceTestViewModel()
        {

//            var canExecuteObserverAnd = new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1.Condition && this.Condition2.Condition);
            TestAndCommand = CommandFactory.Factory
                .Command(() => { })
                .ObservesCanExecute(() => this.Condition1.Condition || this.Condition2.Condition)
                .Build();
            TestAndCommand.Activate();

            var getter = ExpressionGetter.CreateValueGetter(() => this.Condition1.Condition || this.Condition2.Condition, false);
            TestOrCommand = CommandFactory.Factory
                .Command(() => { })
                .CanExecute(getter)
                .ObservesProperty(() => this.Condition1.Condition)
                .ObservesProperty(() => this.Condition2.Condition)
                .AutoActivate()
                .Build();

 //           TestAndCommand.Activate();

            //var canExecuteObserverAnd = new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1.Condition && this.Condition2.Condition);
            //TestAndCommand = new ActivatableCanExecuteObserverCommand(() => { }, canExecuteObserverAnd);
            //TestAndCommand.Activate();

            //var canExecuteObserverOr = new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1.Condition || this.Condition2.Condition);
            //TestOrCommand = new ActivatableCanExecuteObserverCommand(() => { }, canExecuteObserverOr);
            //TestOrCommand.Activate();
        }

        public IActivatableSyncCommand TestAndCommand
        {
            get;
        }

        public IActivatableSyncCommand TestOrCommand
        {
            get;
        }

        private ConditionViewModel condition1 = new ConditionViewModel();
        private ConditionViewModel condition2;// = new ConditionViewModel();

        public ConditionViewModel Condition1
        {
            get => condition1;
            set
            {
                if (Equals(value, condition1)) return;
                condition1 = value;
                OnPropertyChanged();
            }
        }

        public ConditionViewModel Condition2
        {
            get => condition2;
            set
            {
                if (Equals(value, condition2)) return;
                condition2 = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}