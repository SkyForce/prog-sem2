using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Accessor
{
    class Accessor
    {
        string[] path;

        public Func<T, Object> GetDelegate<T>(string _path)
        {
            this.path = _path.Substring(_path.IndexOf('.') + 1).Split('.');

            var nullLabel = Expression.Label(typeof(object), "null");
            var valueLabel = Expression.Label(typeof(object), "value");

            List<BinaryExpression> list = new List<BinaryExpression>();

            ParameterExpression param = Expression.Parameter(typeof(T));
            MemberExpression member = Expression.PropertyOrField(param, path[0]);

            for (int i = 1; i < path.Length; i++)
            {
                list.Add(Expression.Equal(member, Expression.Constant(null)));
                member = Expression.PropertyOrField(member, path[i]);
            }

            var tempCondition = Expression.IfThenElse(
                list[list.Count - 1],
                Expression.Return(nullLabel, Expression.Constant(null)),
                Expression.Return(valueLabel, member));

            for (int i = list.Count - 2; i >= 0; i--)
            {
                tempCondition = Expression.IfThenElse(
                    list[i],
                    Expression.Return(nullLabel, Expression.Constant(null)),
                    tempCondition);
            }

            var block = Expression.Block(
                tempCondition, Expression.Label(nullLabel, Expression.Default(typeof(object))), Expression.Label(valueLabel, Expression.Default(typeof(object))));

            return Expression.Lambda<Func<T, object>>(block, param).Compile();
        }
    }
}
