using System.Collections;
using System.Collections.Generic;
using Anori.ExpressionObservers.UnitTests;
using Anori.ExpressionObservers.UnitTests.TestClasses;

namespace Anori.PropertyChain.UnitTest
{
    public class TestClass1
    {
        internal TestClass2 test2 = new TestClass2();
        internal TestClass2 test2B ;
        internal int intProperty;

        public int IntProperty
        {
            get => intProperty;
            set => intProperty = value;
        }

        public bool BoolProperty { get; set; }

        public TestClass2 GetTest2() => Test2;


        public TestClass2 Test2
        {
            get => test2;
            set => test2 = value;
        }

        public TestClass2 Test2B
        {
            get => test2B;
            set => test2B = value;
        }

        public TestStruct2 Struct2 { get; set; }

        public int? NulableProperty { get; set; }

        public IList<int> List { get;  } = new List<int>(){1,2,3,4,5};
        public IList<int> IntArrayProperty { get; set; }
    }

    public struct TestStruct1
    {
        internal int property;
        internal TestStruct2 test2;


        public int Property
        {
            get => property;
            set => property = value;
        }

        public TestStruct2 GetTest2() => Test2;


        public TestStruct2 Test2
        {
            get => test2;
            set => test2 = value;
        }
    }
    public struct TestStruct2
    {
        internal int property;
        internal TestStruct3 test3;


        public int Property
        {
            get => property;
            set => property = value;
        }

        public TestStruct3 GetTest3() => Test3;


        public TestStruct3 Test3
        {
            get => test3;
            set => test3 = value;
        }
    }

    public struct TestStruct3
    {
        internal int property;

        public int Property
        {
            get => property;
            set => property = value;
        }
    }
}