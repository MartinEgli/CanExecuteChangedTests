namespace Anori.ExpressionObservers.ConsoleTests
{
    public class TestClass2
    {
        public int Property { get; set; }

        public TestClass3 Test3 { get; set; }

        public TestClass3 GetTest3() => Test3;

        public TestClass3 GetTest3(int i) => Test3;

        public TestClass3 GetTest3(int i, string s) => Test3;
    }
}