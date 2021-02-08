using System.Collections.Generic;

namespace Anorisoft.ExpressionObservers.UnitTests.TestClasses
{
    public class TestClass3
    {
        public int Property { get; set; }

        public IList<int> List { get; } = new List<int>() { 1, 2, 3, 4, 5 };

    }
}