using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Kompanion.Domain.Abstracts;

namespace Kompanion.Application.Extensions;

public static class QueryableExtensions
{
    private const string OrderByConst = nameof(Enumerable.OrderBy);
    private const string OrderByDescendingConst = nameof(Enumerable.OrderByDescending);

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool desc)
    {
        Type type = typeof(T);
        string methodName = desc ? OrderByDescendingConst : OrderByConst;
        PropertyInfo property = !string.IsNullOrEmpty(propertyName) ? type.GetProperty(propertyName) : null;
        if (property == null)
        {
            return source;
        }

        ParameterExpression parameter = Expression.Parameter(type, "p");
        MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);
        LambdaExpression orderByExp = Expression.Lambda(propertyAccess, parameter);
        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
        return source.Provider.CreateQuery<T>(resultExp);
    }

    public static IQueryable<T> WithAsNoTracking<T>(this IQueryable<T> queryable, bool withAsNoTracking) where T : BaseEntity
    {
        return withAsNoTracking ? queryable.AsNoTracking() : queryable;
    }
}