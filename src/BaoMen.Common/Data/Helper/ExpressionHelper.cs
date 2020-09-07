using BaoMen.Common.Constant;
using System;
using System.Linq.Expressions;

namespace BaoMen.Common.Data.Helper
{
    /// <summary>
    /// 表达式帮助类
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// 创建比较运算表达式
        /// </summary>
        /// <param name="compareOperator">比较操作</param>
        /// <param name="leftExpression">一个表示左操作数的 Expression</param>
        /// <param name="rightExpression">一个表示右操作数的 Expression</param>
        /// <returns></returns>
        public static BinaryExpression CreateCompareExpression(DbCompareOperator compareOperator, Expression leftExpression, Expression rightExpression)
        {
            BinaryExpression e = null;
            switch (compareOperator)
            {
                case DbCompareOperator.IsNull:
                    e = Expression.Equal(leftExpression, null);
                    break;
                case DbCompareOperator.IsNotNull:
                    e = Expression.NotEqual(leftExpression, null);
                    break;
                case DbCompareOperator.Equal:
                    e = Expression.Equal(leftExpression, rightExpression);
                    break;
                case DbCompareOperator.GreaterThan:
                    e = Expression.GreaterThan(leftExpression, rightExpression);
                    break;
                case DbCompareOperator.GreaterThanOrEqual:
                    e = Expression.GreaterThanOrEqual(leftExpression, rightExpression);
                    break;
                case DbCompareOperator.LessThan:
                    e = Expression.LessThan(leftExpression, rightExpression);
                    break;
                case DbCompareOperator.LessThanOrEqual:
                    e = Expression.LessThanOrEqual(leftExpression, rightExpression);
                    break;
                case DbCompareOperator.NotEqual:
                    e = Expression.NotEqual(leftExpression, rightExpression);
                    break;
            }
            return e;
            //ExpressionType type = (ExpressionType)Enum.Parse(typeof(ExpressionType), compareOperator.ToString());
            //return Expression.MakeBinary(type, leftExpression, rightExpression);
        }

        /// <summary>
        /// 创建逻辑运算表达式
        /// </summary>
        /// <param name="logicOpertor">逻辑操作</param>
        /// <param name="leftExpression">一个表示左操作数的 Expression</param>
        /// <param name="rightExpression">一个表示右操作数的 Expression</param>
        /// <returns></returns>
        public static BinaryExpression CreateLogicExpression(LogicOperator logicOpertor, Expression leftExpression, Expression rightExpression)
        {
            BinaryExpression e = null;
            switch (logicOpertor)
            {
                case LogicOperator.ConditionAnd:
                    e = Expression.AndAlso(leftExpression, rightExpression);
                    break;
                case LogicOperator.ConditionOr:
                    e = Expression.OrElse(leftExpression, rightExpression);
                    break;
                case LogicOperator.LogicAnd:
                    e = Expression.And(leftExpression, rightExpression);
                    break;
                case LogicOperator.LogicOr:
                    e = Expression.Or(leftExpression, rightExpression);
                    break;
                case LogicOperator.LogicExclusiveOr:
                    e = Expression.ExclusiveOr(leftExpression, rightExpression);
                    break;
            }
            return e;
        }

        /// <summary>
        /// 转换CompareOperator到ExpressionType
        /// </summary>
        /// <param name="compareOperator">一个CompareOperator</param>
        /// <returns></returns>
        public static ExpressionType ConvertCompareOperator(DbCompareOperator compareOperator)
        {
            switch (compareOperator)
            {
                case DbCompareOperator.Equal:
                case DbCompareOperator.IsNull:
                    return ExpressionType.Equal;
                case DbCompareOperator.GreaterThan:
                    return ExpressionType.GreaterThan;
                case DbCompareOperator.GreaterThanOrEqual:
                    return ExpressionType.GreaterThanOrEqual;
                case DbCompareOperator.LessThan:
                    return ExpressionType.LessThan;
                case DbCompareOperator.LessThanOrEqual:
                    return ExpressionType.LessThanOrEqual;
                case DbCompareOperator.NotEqual:
                case DbCompareOperator.IsNotNull:
                    return ExpressionType.NotEqual;
                default:
                    return ExpressionType.Default;
                    //throw new ArgumentException("unknow compareOperator");
            }
        }

        /// <summary>
        /// 转换LogicOperator到ExpressionType
        /// </summary>
        /// <param name="logicOperator">一个LogicOperator</param>
        /// <returns></returns>
        public static ExpressionType CovertLogicOperator(LogicOperator logicOperator)
        {
            switch (logicOperator)
            {
                case LogicOperator.ConditionAnd:
                    return ExpressionType.AndAlso;
                case LogicOperator.ConditionOr:
                    return ExpressionType.OrElse;
                case LogicOperator.LogicAnd:
                    return ExpressionType.And;
                case LogicOperator.LogicOr:
                    return ExpressionType.Or;
                case LogicOperator.LogicExclusiveOr:
                    return ExpressionType.ExclusiveOr;
                default:
                    throw new ArgumentException("unknow logicOperator");
            }
        }

        /// <summary>
        /// 转换DbLogicOperator到ExpressionType
        /// </summary>
        /// <param name="dbLogicOperator">一个LogicOperator</param>
        /// <returns></returns>
        public static ExpressionType CovertDbLogicOperator(DbLogicOperator dbLogicOperator)
        {
            switch (dbLogicOperator)
            {
                case DbLogicOperator.And:
                    return ExpressionType.AndAlso;
                case DbLogicOperator.Or:
                    return ExpressionType.OrElse;
                default:
                    throw new ArgumentException("unknow logicOperator");
            }
        }
    }
}
