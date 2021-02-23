namespace Anori.ExpressionObservers.UnitTests.TestClasses
{
    public class NotifyPropertyChangedClass2 : Bindable
    {
        private int _intProperty;

        public int IntProperty
        {
            get => _intProperty;
            set
            {
                if (value == _intProperty) return;
                _intProperty = value;
                OnPropertyChanged();
            }
        }
    }
}