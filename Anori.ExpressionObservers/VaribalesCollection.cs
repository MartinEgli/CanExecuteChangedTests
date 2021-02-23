using System.Collections.Generic;
using System.Linq.Expressions;

namespace Anori.ExpressionObservers
{
    public class VaribalesCollection : List<ParameterExpression>
    {
        private int index;

        public VaribalesCollection()
        {
            index = 1;
        }

        public int GetNextIndex() => ++index;
    }
}