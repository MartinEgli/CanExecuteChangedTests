using System;
using System.Diagnostics;
using static Anori.ExpressionObservers.ExpressionGetter;

namespace Anori.ExpressionObservers.ConsoleTests
{
    internal class Program
    {
        private static void Main()
        {
            var test = new TestClass1()
            {
                Property = 1,
                Test2 = new TestClass2
                {
                    Property = 2,
                    Test3 = new TestClass3 { Property = 3 }
                }
            };

            var test2 = new TestClass1()
            {
                Property = 1,
                Test2 = new TestClass2
                {
                    Property = 2,
                }
            };

            Console.WriteLine("Reference Getter (TestClass1 t) => t.Test2.Test3");
            var referenceGetter = CreateReferenceGetter((TestClass1 t) => t.Test2.Test3);
            var reference = referenceGetter(test);
            Console.WriteLine("Reference: (test)" + (reference != null ? reference.ToString() : "null"));
            reference = referenceGetter(new TestClass1());
            Console.WriteLine("Reference (new TestClass1()): " + (reference != null ? reference.ToString() : "null"));

            var f3 = CreateValueGetter((TestClass1 t) => t.Test2.GetTest3(1, null).Property);
            var s = f3(test);
            Console.WriteLine(s.HasValue ? s.ToString() : "null");

            var f2 = CreateValueGetter((TestClass1 t) => t.Test2.GetTest3(1).Property);
            s = f2(test);
            Console.WriteLine(s.HasValue ? s.ToString() : "null");

            var f1 = CreateValueGetter((TestClass1 t) => t.Test2.GetTest3().Property);
            s = f1(test);
            Console.WriteLine(s.HasValue ? s.ToString() : "null");

            Console.WriteLine("test.Test2.Test3.Property = 3");
            var f5 = CreateValueGetter(
                (TestClass1 t1, TestClass1 t2) => t2.Test2.Test3.Property == 3 || t1.Test2.Test3.Property == 3);
            var s2 = f5(test, test2);
            Console.WriteLine(s.HasValue ? s2.ToString() : "null");

            Console.WriteLine("test.Test2.Test3.Property = 0");
            test.Test2.Test3.Property = 0;
            s = f1(test);
            Console.WriteLine(s.HasValue ? s.ToString() : "null");

            Console.WriteLine("test.Test2.Test3.Property = 99");
            test.Test2.Test3.Property = 99;
            s = f1(test);
            Console.WriteLine(s.HasValue ? s.ToString() : "null");

            Console.WriteLine("test = null");
            test = null;
            s = f1(test);
            Console.WriteLine(s.HasValue ? s.ToString() : "null");

            Console.WriteLine("test = new TestClass1 { Property = 5 }");
            test = new TestClass1 { Property = 5 };
            s = f1(test);
            Console.WriteLine(s.HasValue ? s.ToString() : "null");

            Console.WriteLine("test = new TestClass1 { Test2 = new TestClass2() }");
            test = new TestClass1 { Test2 = new TestClass2() };
            s = f1(test);
            Console.WriteLine(s.HasValue ? s.ToString() : "null");

            test = new TestClass1()
            {
                Property = 1,
                Test2 = new TestClass2
                {
                    Property = 2,
                    Test3 = new TestClass3 { Property = 3 }
                }
            };

            var count = 1;

            Console.WriteLine("ValueGetter");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = f1(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            Console.WriteLine("Test1Method");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = Test1Method(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            Console.WriteLine("Test2Method");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = Test2Method(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            Console.WriteLine("Test3MethodExcepion");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = Test3MethodExcepion(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            count = 1000;
            Console.WriteLine("ValueGetter");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = f1(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            Console.WriteLine("Test1Method");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = Test1Method(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            Console.WriteLine("Test2Method");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = Test2Method(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            Console.WriteLine("Test3MethodExcepion");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = Test3MethodExcepion(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            Console.WriteLine("ValueGetter");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = f1(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            Console.WriteLine("Test1Method");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = Test1Method(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            Console.WriteLine("Test2Method");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                s = Test2Method(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);

            Console.WriteLine("Test3MethodExcepion");
            stopwatch.Reset();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                var r = Test3MethodExcepion(test);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);
        }

        private static int? Test3MethodExcepion(TestClass1 test)
        {
            try
            {
                return test.Test2.Test3.Property;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        private static int? Test2Method(TestClass1 test)
        {
            return test?.Test2?.Test3?.Property;
        }

        private static int? Test1Method(TestClass1 test)
        {
            if (test == null)
            {
                return null;
            }

            var s1 = test.Test2;
            if (s1 == null)
            {
                return null;
            }

            var s2 = s1.Test3;
            if (s2 == null)
            {
                return null;
            }

            return s2.Property;
        }
    }
}