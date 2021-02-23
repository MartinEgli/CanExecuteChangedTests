namespace Anori.ExpressionObservers.UnitTests.TestClasses
{
    public class NotifyPropertyChangedClass1 : Bindable
    {
        private int _intProperty;
        private NotifyPropertyChangedClass2 _class2 = new NotifyPropertyChangedClass2();

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

        public NotifyPropertyChangedClass2 Class2
        {
            get => _class2;
            set
            {
                if (Equals(value, _class2)) return;
                _class2 = value;
                OnPropertyChanged();
            }
        }
    }
}