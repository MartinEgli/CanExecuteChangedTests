using System.Collections.Generic;

namespace Anori.ExpressionObservers.UnitTests.TestClasses
{
    public class TestClass2
    {
        internal TestClass3 test3;
        public int Property { get; set; }

        public TestClass3 Test3
        {
            get => test3;
            set => test3 = value;
        }

        public TestClass3 GetTest3() => Test3;

        public TestClass3 GetTest3(int i)
        {
            if (i != 1)
            {
                return null;
            }
            return Test3;
        }

        public IList<int> List { get; } = new List<int>() { 1, 2, 3, 4, 5 };
        public IList<TestClass3> Tests { get; } = new List<TestClass3>() { new TestClass3(){Property = 1}, new TestClass3() { Property = 2 } };


        public TestClass3 GetTest3(int i, string s) => Test3;
    }
}