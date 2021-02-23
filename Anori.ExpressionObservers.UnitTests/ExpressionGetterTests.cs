using System;
using System.Linq.Expressions;
using Anori.ExpressionObservers.UnitTests.TestClasses;
using Anori.PropertyChain.UnitTest;
using NUnit.Framework;

namespace Anori.ExpressionObservers.UnitTests
{
    public class ExpressionGetterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_Property_Return3()
        {
            var test = CreateTestInstanceA();
            const int expected = 3;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_test_Test2_Test3_Property_Return3()
        {
            var test = CreateTestInstanceA();
            const int expected = 3;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_new_TestClass1_Return0()
        {
            var test = CreateTestInstanceA();
            const int expected = 0;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => new TestClass1().IntProperty);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_new_TestClass1_IntProperty_1_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => new TestClass1() { IntProperty = 1 }.IntProperty);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_new_TestClass1_IntProperty_test_IntProperty_Return1()
        {
            var test = CreateTestInstanceA();

            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => new TestClass1() { IntProperty = test.IntProperty }.IntProperty);
            test.IntProperty = 3;
            const int expected = 3;
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_MemberInit_new_TestClass1_IntProperty_test_IntProperty_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => new TestClass1() { IntProperty = test.IntProperty }.IntProperty);
            int? expected = null;
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_MemberInit_new_TestClass1_IntProperty_test_IntArrayProperty_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => new TestClass1() { IntArrayProperty = { test.IntProperty, test.IntProperty } }.IntProperty);
            int? expected = null;
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_MemberInit_new_TestClass1_IntTest2_IntProperty_Return2()
        {
            var test = CreateTestInstanceA();
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => new TestClass1() { Test2 = new TestClass2() { Property = 7 } }.Test2.Property);
            int? expected = 7;
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_MemberInit_new_TestClass1_IntTest2_IntProperty_test1_null_ReturnNull()
        {
            var test1 = CreateTestInstanceA();
            var test2 = CreateTestInstanceA();
            test1 = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => new TestClass1() { Test2 = new TestClass2() { Property = test1.Test2.Property } }.Test2.Property);
            int? expected = null;
            var actual = getValue(test2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_MemberInit_new_TestClass1_IntTest2_IntProperty_test2_null_ReturnNull()
        {
            var test1 = CreateTestInstanceA();
            var test2 = CreateTestInstanceA();
            test2 = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => new TestClass1() { Test2 = new TestClass2() { Property = test1.Test2.Property } }.Test2.Property);
            int? expected = 2;
            var actual = getValue(test2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_MemberInit_new_TestClass1_IntTest2_IntProperty_t_null_ReturnNull()
        {
            var test2 = CreateTestInstanceA();
            test2 = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => new TestClass1() { Test2 = new TestClass2() { Property = t.Test2.Property } }.Test2.Property);
            int? expected = null;
            var actual = getValue(test2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_MemberInit_new_TestClass1_IntTest2_IntProperty_t_Return2()
        {
            var test1 = CreateTestInstanceA();
            test1.Test2.Property = 4;
            var val = new TestClass1() { Test2 = { Property = test1.Test2.Property } }.Test2.Property;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => new TestClass1() { Test2 = { Property = t.Test2.Property } }.Test2.Property);
            int? expected = 4;
            var actual = getValue(test1);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_MemberInit_new_TestClass1_IntTest2_IntProperty_t_Test2B_Return2()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                var test1 = CreateTestInstanceA();
                test1.Test2.Property = 4;
                var getValue = ExpressionGetter.CreateValueGetter(
                    (TestClass1 t) => new TestClass1() { Test2B = { Property = t.Test2.Property } }.Test2.Property);
                var actual = getValue(test1);
            });
        }

        [Test]
        public void CreateValueGetter_B_t_Test2_Test3_Property_Return3()
        {
            var test = CreateTestInstanceB();
            const int expected = 3;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestStruct1 t) => t.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_B_test_Test2_Test3_Property_Return3()
        {
            var test = CreateTestInstanceB();
            const int expected = 3;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestStruct1 t) => test.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_B_a_Test2_Test3_Property_Equal_B_Test2_Test3_Property_Return_True()
        {
            var testA = CreateTestInstanceA();
            var testB = CreateTestInstanceB();
            const bool expected = true;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a, TestStruct1 b) => a.Test2.Test3.Property == b.Test2.Test3.Property);
            var actual = getValue(testA, testB);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_B_a_Test2_Test3_Property_Equal_b_Test2_Test3_Property_Not_Return_False()
        {
            var testA = CreateTestInstanceA();
            var testB = CreateTestInstanceB();
            const bool expected = false;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a, TestStruct1 b) => !(a.Test2.Test3.Property == b.Test2.Test3.Property));
            var actual = getValue(testA, testB);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_B_a_Test2_Test3_Property_NotEqual_b_Test2_Test3_Property_Return_False()
        {
            var testA = CreateTestInstanceA();
            var testB = CreateTestInstanceB();
            const bool expected = false;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a, TestStruct1 b) => (a.Test2.Test3.Property != b.Test2.Test3.Property));
            var actual = getValue(testA, testB);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_B_a_Test2_Test3_Property_Equal_B_Test2_Test3_Property_ToString_Return3()
        {
            var testA = CreateTestInstanceA();
            var testB = CreateTestInstanceB();
            var expected = true.ToString();
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 a, TestStruct1 b) => (a.Test2.Test3.Property == b.Test2.Test3.Property).ToString());
            var actual = getValue(testA, testB);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_B_t_Property_Return2()
        {
            var testA = CreateTestInstanceA();
            var testB = CreateTestInstanceB();
            const int expected = 2;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a, TestStruct1 b) => a.IntProperty + b.Property);
            var actual = getValue(testA, testB);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_B_a_Property_Return1()
        {
            var testA = CreateTestInstanceA();
            var testB = CreateTestInstanceB();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a, TestStruct1 b) => a.IntProperty);
            var actual = getValue(testA, testB);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_Addition_a1_Property_a2_Property_a3_Property_Return3()
        {
            var testA1 = CreateTestInstanceA();
            var testA2 = CreateTestInstanceA();
            var testA3 = CreateTestInstanceA();
            const int expected = 3;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.IntProperty + a2.IntProperty + a3.IntProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_Addition_a1_Property_a2_Property_a3_Property_A1_null_ReturnNull()
        {
            var testA1 = CreateTestInstanceA();
            var testA2 = CreateTestInstanceA();
            var testA3 = CreateTestInstanceA();
            testA1 = null;
            int? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.IntProperty + a2.IntProperty + a3.IntProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_OrElse_a1_Property_a2_Property_a3_Property_A1_Property_True_A2_null_ReturnTrue()
        {
            var testA1 = CreateTestInstanceA();
            var testA2 = CreateTestInstanceA();
            var testA3 = CreateTestInstanceA();
            testA1.BoolProperty = true;
            testA2 = null;
            bool? expected = true;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.BoolProperty || a2.BoolProperty || a3.BoolProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_OrElse_a1_Property_a2_Property_a3_Property_A2_Property_True_A3_null_ReturnTrue()
        {
            var testA1 = CreateTestInstanceA();
            var testA2 = CreateTestInstanceA();
            var testA3 = CreateTestInstanceA();
            testA2.BoolProperty = true;
            testA3 = null;
            bool? expected = true;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.BoolProperty || a2.BoolProperty || a3.BoolProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_OrElse_a1_Property_a2_Property_a3_Property_A2_Property_True_A1_null_ReturnNull()
        {
            var testA1 = CreateTestInstanceA();
            var testA2 = CreateTestInstanceA();
            var testA3 = CreateTestInstanceA();
            testA2.BoolProperty = true;
            testA1 = null;
            bool? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.BoolProperty || a2.BoolProperty || a3.BoolProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_OrElse_a1_Property_a2_Property_a3_Property_ReturnFalse()
        {
            var testA1 = CreateTestInstanceA();
            var testA2 = CreateTestInstanceA();
            var testA3 = CreateTestInstanceA();
            bool? expected = false;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.BoolProperty || a2.BoolProperty || a3.BoolProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_AndAlso_a1_Property_a2_Property_a3_Property_ReturnFalse()
        {
            var testA1 = CreateTestInstanceA();
            var testA2 = CreateTestInstanceA();
            var testA3 = CreateTestInstanceA();
            bool? expected = false;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.BoolProperty && a2.BoolProperty && a3.BoolProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_AndAlso_a1_Property_a2_Property_a3_Property_a1_null_ReturnNull()
        {
            var testA1 = CreateTestInstanceA();
            var testA2 = CreateTestInstanceA();
            var testA3 = CreateTestInstanceA();
            testA1 = null;
            bool? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.BoolProperty && a2.BoolProperty && a3.BoolProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_AndAlso_a1_Property_a2_Property_a3_Property_a2_null_ReturnFalse()
        {
            var testA1 = CreateTestInstanceA();
            TestClass1 testA2 = null;
            var testA3 = CreateTestInstanceA();
            bool? expected = false;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.BoolProperty && a2.BoolProperty && a3.BoolProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_AndAlso_a1_Property_a2_Property_a3_Property_a1_BoolProperty_true__a2_null_ReturnNull()
        {
            var testA1 = CreateTestInstanceA();
            TestClass1 testA2 = null;
            var testA3 = CreateTestInstanceA();
            testA1.BoolProperty = true;
            bool? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.BoolProperty && a2.BoolProperty && a3.BoolProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_AndAlso_a1_Property_a2_Property_a3_Property_ReturnTrue()
        {
            var testA1 = CreateTestInstanceA();
            var testA2 = CreateTestInstanceA();
            var testA3 = CreateTestInstanceA();
            testA1.BoolProperty = true;
            testA2.BoolProperty = true;
            testA3.BoolProperty = true;
            bool? expected = true;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.BoolProperty && a2.BoolProperty && a3.BoolProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A1_A2_A3_Addition_a1_Property_a2_Property_a3_Property_A3_null_ReturnNull()
        {
            var testA1 = CreateTestInstanceA();
            var testA2 = CreateTestInstanceA();
            TestClass1 testA3 = null;
            int? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a1, TestClass1 a2, TestClass1 a3) => a1.IntProperty + a2.IntProperty + a3.IntProperty);
            var actual = getValue(testA1, testA2, testA3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_B_b_Property_Return1()
        {
            var testA = CreateTestInstanceA();
            var testB = CreateTestInstanceB();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a, TestStruct1 b) => b.Property);
            var actual = getValue(testA, testB);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_B_b_Property_Return3()
        {
            var testA = CreateTestInstanceA();
            var testB = CreateTestInstanceB();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 a, TestStruct1 b) => b.Property);
            var actual = getValue(testA, testB);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_List2_Return3()
        {
            var test = CreateTestInstanceA();
            const int expected = 3;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.List[2]);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_List2_t_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test = null;
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.List[2]);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_List2_t_Test2_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2 = null;
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.List[2]);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_List2_t_Test2_Test3_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2.Test3 = null;
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.List[2]);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Property_List2_Return3()
        {
            var test = CreateTestInstanceA();
            const int expected = 3;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.List[2]);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Tests1_Property_Return2()
        {
            var test = CreateTestInstanceA();
            const int expected = 2;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Tests[1].Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Tests0_Property_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Tests[0].Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Tests1_Property_t_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test = null;
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Tests[1].Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Tests2_Property_ReturnNull()
        {
            var test = CreateTestInstanceA();
            var expected = (int?)null;
            var actual = expected;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Tests[2].Property);

            Assert.Throws<ArgumentOutOfRangeException>(() => actual = getValue(test));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Tests_ElementAtOrNull2_Property_ReturnNull()
        {
            var test = CreateTestInstanceA();
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Tests.ElementAtOrNull(2).Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Property_List2_t_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test = null;
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.List[2]);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Property_List_ElementAtOrNull2_Value_t_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test = null;
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.List.ElementAtOrNull(2).Value);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Property_List2_t_Test2_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2 = null;
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.List[2]);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Property_List_ElementAtOrNull2_Value_t_Test2_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2 = null;
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.List.ElementAtOrNull(2).Value);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Property_List2_Return3()
        {
            var test = CreateTestInstanceA();
            const int expected = 3;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.List[2]);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Property_List_ElementAtOrNull2_Value_Return3()
        {
            var test = CreateTestInstanceA();
            const int expected = 3;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.List.ElementAtOrNull(2).Value);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Property_List_ElementAtOrNull9_Value_ReturnNull()
        {
            var test = CreateTestInstanceA();
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.List.ElementAtOrNull(9).Value);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Property_List9_ReturnNull()
        {
            var test = CreateTestInstanceA();
            var expected = (int?)null;
            var actual = expected;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.List[9]);

            Assert.Throws<ArgumentOutOfRangeException>(() => actual = getValue(test));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Property_List2_t_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test = null;
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.List[2]);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_Property_ChangeProperty_Return3And4()
        {
            var test = CreateTestInstanceA();
            const int expected1 = 3;
            const int expected2 = 4;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected1, actual);

            test.Test2.Test3.Property = 4;
            actual = getValue(test);

            Assert.AreEqual(expected2, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_Property_ChangeTest3_Return3And5()
        {
            var test = CreateTestInstanceA();
            const int expected1 = 3;
            const int expected2 = 5;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected1, actual);

            test.Test2.Test3 = new TestClass3 { Property = 5 };
            actual = getValue(test);

            Assert.AreEqual(expected2, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_Property_ChangeTest2_Return3And6()
        {
            var test = CreateTestInstanceA();
            const int expected1 = 3;
            const int expected2 = 6;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected1, actual);

            test.Test2 = new TestClass2 { Test3 = new TestClass3() { Property = 6 } };
            actual = getValue(test);

            Assert.AreEqual(expected2, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_Property_ChangeTest1_Return3And7()
        {
            var test = CreateTestInstanceA();
            const int expected1 = 3;
            const int expected2 = 7;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected1, actual);

            test = new TestClass1() { Test2 = new TestClass2 { Test3 = new TestClass3() { Property = 7 } } };
            actual = getValue(test);

            Assert.AreEqual(expected2, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_Property_Test_null_ReturnNull()
        {
            var test = (TestClass1)null;
            int? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_Test3_Property_ToString_Test_null_ReturnNull()
        {
            var test = (TestClass1)null;
            int? expected = null;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.Test3.Property.ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_Property_Test_Test2_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2 = null;
            int? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_Test3_Property_ToString_Test_Test2_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2 = null;
            var expected = (string)null;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.Test3.Property.ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Test3_Property_Test_Test2_Test3_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2.Test3 = null;
            int? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Test3.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_Test3_Property_ToString_Test_Test2_Test3_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2.Test3 = null;
            var expected = (string)null;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.Test3.Property.ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Property_Return2()
        {
            var test = CreateTestInstanceA();
            const int expected = 2;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_Property_ToString_Return2()
        {
            var test = CreateTestInstanceA();
            var expected = 2.ToString();
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.Property.ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Property_test_Null_ReturnNull()
        {
            var test = (TestClass1)null;
            int? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_Property_ToString_test_Null_ReturnNull()
        {
            var test = (TestClass1)null;
            var expected = (string)null;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.Property.ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Test2_Property_test_Test2_Null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2 = null;
            int? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.Test2.Property);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_Property_ToString_test_Test2_Null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2 = null;
            const string expected = (string)null;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.Property.ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Property_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.IntProperty);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_Fallback_A_t_Property_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.IntProperty, 10);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_1_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => 1);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_BoolProperty_IIf_BoolProperty_1_2_ToString_Return1()
        {
            var test = CreateTestInstanceA();
            var expected = 2.ToString();
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => (t.BoolProperty ? 1 : 2).ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_BoolProperty_false_IIf_BoolProperty_t_Test2_Property_t_Test2_Test3_Property_ToString_Return2()
        {
            var test = CreateTestInstanceA();
            test.BoolProperty = false;
            var expected = 3.ToString();
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => (t.BoolProperty ? t.Test2.Property : t.Test2.Test3.Property).ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_BoolProperty_true_IIf_BoolProperty_t_Test2_Property_t_Test2_Test3_Property_ToString_Return2()
        {
            var test = CreateTestInstanceA();
            test.BoolProperty = true;
            var expected = 2.ToString();
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => (t.BoolProperty ? t.Test2.Property : t.Test2.Test3.Property).ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_BoolProperty_true_IIf_BoolProperty_t_Test2_Property_t_Test2_Test3_Property_t_Test2_Test3_null_ToString_Return2()
        {
            var test = CreateTestInstanceA();
            test.BoolProperty = true;
            test.Test2.Test3 = null;
            var expected = 2.ToString();
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => (t.BoolProperty ? t.Test2.Property : t.Test2.Test3.Property).ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_BoolProperty_false_IIf_BoolProperty_t_Test2_Property_t_Test2_Test3_Property_t_Test2_Test3_null_ToString_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.BoolProperty = false;
            test.Test2.Test3 = null;
            string expected = null;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => (t.BoolProperty ? t.Test2.Property : t.Test2.Test3.Property).ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_BoolProperty_IIf_BoolProperty_1_2_Return1()
        {
            var test = CreateTestInstanceA();
            test.BoolProperty = false;
            const int expected = 2;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.BoolProperty ? 1 : 2);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_BoolProperty_True_IIf_BoolProperty_1_2_Return1()
        {
            var test = CreateTestInstanceA();
            test.BoolProperty = true;
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.BoolProperty ? 1 : 2);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            object expected = null;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => (object)null);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_Fallback_A_t_Property_null_Return10()
        {
            var test = CreateTestInstanceA();
            test = null;
            int expected = 10;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.intProperty, 10);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_GetValue_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => GetValue());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_GetValue_1_Return2()
        {
            var test = CreateTestInstanceA();
            const int expected = 2;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => GetValue1Param(1));
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_GetValue_t_Return2()
        {
            var test = CreateTestInstanceA();
            const int expected = 2;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => GetValue1Param(t));
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_GetValue_GetValue_Return2()
        {
            var test = CreateTestInstanceA();
            const int expected = 2;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => GetValue1Param(GetValue()));
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_GetValue_t_Property_Return2()
        {
            var test = CreateTestInstanceA();
            const int expected = 2;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => GetValue1Param(t.IntProperty));
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_GetStaticValue_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => GetStaticValue());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_GetStaticValue1Param_1_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 2;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => GetStaticValue1Param(1));
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_GetStaticValue1Param_t_Property_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 2;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => GetStaticValue1Param(t.IntProperty));
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_GetStaticValue1Param_t_Property_Test_null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test = null;
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => GetStaticValue1Param(t.IntProperty));
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_int_1_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (int t) => t);
            var actual = getValue(1);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_intProperty_Return1()
        {
            var test = CreateTestInstanceA();
            const int expected = 1;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.intProperty);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_boolProperty_ReturnFalse()
        {
            var test = CreateTestInstanceA();
            const bool expected = false;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.BoolProperty);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_BoolProperty_Not_ReturnTrue()
        {
            var test = CreateTestInstanceA();
            const bool expected = true;

            Expression<Func<TestClass1, bool>> propertyExpression =
                (t) => !t.BoolProperty;
            Assert.AreEqual(expected, propertyExpression.Compile()(test));

            var getValue = ExpressionGetter.CreateValueGetter(propertyExpression);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_BoolProperty_Not_ToString_ReturnTrue()
        {
            var test = CreateTestInstanceA();
            var expected = true.ToString();
            Expression<Func<TestClass1, string>> propertyExpression =
                (t) => (!t.BoolProperty).ToString();
            var getValue = ExpressionGetter.CreateReferenceGetter(propertyExpression);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Property_ToString_Return1()
        {
            var test = CreateTestInstanceA();
            var expected = 1.ToString();
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.IntProperty.ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_NullableProperty_ReturnNull()
        {
            var test = CreateTestInstanceA();
            var expected = (int?)null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.NulableProperty.Value);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_NullableProperty_ToString_ReturnNull()
        {
            var test = CreateTestInstanceA();
            var expected = (string)null;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.NulableProperty.Value.ToString());
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_NullableProperty_Return4()
        {
            var test = CreateTestInstanceA();
            test.NulableProperty = 4;
            var expected = (int?)4;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.NulableProperty.Value);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_ReturnInstanceOfTestClass1()
        {
            var test = CreateTestInstanceA();
            var expected = test;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_null_coalescing_test_ReturnInstanceOfTestClass1()
        {
            var test = CreateTestInstanceA();
            var expected = test;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t ?? test);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_null_coalescing_test_t_null_ReturnInstanceOfTestClass1()
        {
            var test = CreateTestInstanceA();
            var expected = test;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t ?? test);
            var actual = getValue(null);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_null_coalescing_test2_t_null_ReturnInstanceOfTestClass2()
        {
            var test = CreateTestInstanceA();
            var test2 = new TestClass2();
            var expected = test2;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2 ?? test2);
            var actual = getValue(null);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_null_coalescing_test21_null_coalescing_test22_t_null_ReturnInstanceOfTestClass2()
        {
            var test = CreateTestInstanceA();
            TestClass2 test21 = null;
            var test22 = new TestClass2();
            var x = ((TestClass1)null)?.Test2 ?? test21 ?? test22;
            var expected = test22;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2 ?? test21 ?? test22);
            var actual = getValue(null);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_null_coalescing_test2_ReturnInstanceOfTestClass2()
        {
            var test = CreateTestInstanceA();
            var test2 = new TestClass2();
            var expected = test.Test2;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2 ?? test2);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_ReturnInstanceOfTestClass2()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2);
            var actual = getValue(test);

            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_Test3_ReturnInstanceOfTestClass3()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.Test3);
            var actual = getValue(test);

            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_test2_Test3_ReturnInstanceOfTestClass3()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.test2.Test3);
            var actual = getValue(test);

            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_test2_ReturnInstanceOfTestClass2()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.test2);
            var actual = getValue(test);

            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_test2_test3_ReturnInstanceOfTestClass3()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.test2.test3);
            var actual = getValue(test);

            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_test3_ReturnInstanceOfTestClass3()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.test3);
            var actual = getValue(test);

            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_GetTest2_test3_ReturnInstanceOfTestClass3()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.GetTest2().test3);
            var actual = getValue(test);

            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_GetTest2_GetTest3_ReturnInstanceOfTestClass3()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.GetTest2().GetTest3());
            var actual = getValue(test);

            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_test2_Test3_Property_Return()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3.Property;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.test2.Test3.Property);
            var actual = getValue(test);

            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_test2_test3_Property_Return()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3.Property;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.test2.test3.Property);
            var actual = getValue(test);

            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_test2_GetTest3_Property_Return()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3.Property;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.test2.GetTest3().Property);
            var actual = getValue(test);
            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_GetTest2_test3_Property_Return()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3.Property;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.GetTest2().test3.Property);
            var actual = getValue(test);
            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_GetTest2_GetTest3_Property_Return()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3.Property;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.GetTest2().GetTest3().Property);
            var actual = getValue(test);
            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_test2_Property_Return()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Property;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.test2.Property);
            var actual = getValue(test);
            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_GetTest3_Return()
        {
            var test = CreateTestInstanceA();
            var expected = test.Test2.Test3;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.GetTest3(1));
            var actual = getValue(test);
            Assert.NotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_test_Test2_Null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2 = null;
            var expected = test.Test2;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2);
            var actual = getValue(test);
            Assert.Null(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_Test3_test_Test2_Test3_Null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2.Test3 = null;
            var expected = test.Test2.Test3;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.Test3);
            var actual = getValue(test);
            Assert.Null(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateReferenceGetter_A_t_Test2_Test3_test_Test2_Null_ReturnNull()
        {
            var test = CreateTestInstanceA();
            test.Test2 = null;
            var expected = (TestClass2)null;
            var getValue = ExpressionGetter.CreateReferenceGetter(
                (TestClass1 t) => t.Test2.Test3);
            var actual = getValue(test);

            Assert.Null(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateValueGetter_A_t_Property_t_Null_ReturnNull()
        {
            var test = (TestClass1)null;
            int? expected = null;
            var getValue = ExpressionGetter.CreateValueGetter(
                (TestClass1 t) => t.IntProperty);
            var actual = getValue(test);

            Assert.AreEqual(expected, actual);
        }

        private static int GetStaticValue()
        {
            return 1;
        }

        private static int GetStaticValue1Param(int parameter1)
        {
            return 1 + parameter1;
        }

        private static TestClass1 CreateTestInstanceA()
        {
            return new TestClass1
            {
                IntProperty = 1,
                Test2 = new TestClass2
                {
                    Property = 2,
                    Test3 = new TestClass3 { Property = 3 }
                }
            };
        }

        private static TestStruct1 CreateTestInstanceB()
        {
            return new TestStruct1
            {
                Property = 1,
                Test2 = new TestStruct2
                {
                    Property = 2,
                    Test3 = new TestStruct3 { Property = 3 }
                }
            };
        }

        private int GetValue() => 1;

        private int GetValue1Param(int parameter1) => 1 + parameter1;

        private int GetValue1Param(TestClass1 parameter1) => 1 + parameter1.IntProperty;
    }
}