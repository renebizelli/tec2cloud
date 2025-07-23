﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Extensions;

public static class QueryExtension
{
    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, List<(string, bool)> sortCriteria, Dictionary<string, Expression<Func<T, object>>> allowedOrderFields)
    {
        ArgumentNullException.ThrowIfNull(sortCriteria);

        if (!sortCriteria.Any()) return query;

        IOrderedQueryable<T>? orderedQuery = null;

        foreach (var (field, ascending) in sortCriteria)
        {
            if (!allowedOrderFields.TryGetValue(field, out var expression))
                continue;

            if (orderedQuery == null)
            {
                orderedQuery = ascending ? query.OrderBy(expression) : query.OrderByDescending(expression);
            }
            else
            {
                orderedQuery = ascending ? orderedQuery.ThenBy(expression) : orderedQuery.ThenByDescending(expression);
            }
        }

        return orderedQuery ?? query;
    }

    public static IQueryable<T> ApplyWhereLike<T>(this IQueryable<T> query, string? criteria, Expression<Func<T, string>> selector)
    {
        if (string.IsNullOrWhiteSpace(criteria))
            return query;

        string trimmed = criteria.Trim('*');
        var param = selector.Parameters[0];
        var member = selector.Body;

        var call = criteria switch
        {
            var s when s.StartsWith("*") && s.EndsWith("*") =>
                Expression.Call(member, nameof(string.Contains), null, Expression.Constant(trimmed)),

            var s when s.StartsWith("*") =>
                Expression.Call(member, nameof(string.EndsWith), null, Expression.Constant(trimmed)),

            var s when s.EndsWith("*") =>
                Expression.Call(member, nameof(string.StartsWith), null, Expression.Constant(trimmed)),

            _ => Expression.Call(Expression.Call(member, typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!), typeof(string).GetMethod(nameof(string.Equals), new[] { typeof(string) })!, Expression.Constant(criteria.ToLower()))
        };

        var lambda = Expression.Lambda<Func<T, bool>>(call, param);
        return query.Where(lambda);
    }

    public static IQueryable<T> ApplyWhereRange<T>(this IQueryable<T> query, decimal minValue, decimal maxValue, Expression<Func<T, decimal>> selector)
    {
        var param = selector.Parameters[0];
        Expression body = null!;

        if (minValue > 0)
        {
            var minConstant = Expression.Constant(minValue, typeof(decimal));
            var greaterOrEqual = Expression.GreaterThanOrEqual(selector.Body, minConstant);
            body = greaterOrEqual;
        }

        if (maxValue > 0)
        {
            var maxConstant = Expression.Constant(maxValue, typeof(decimal));
            var lessOrEqual = Expression.LessThanOrEqual(selector.Body, maxConstant);

            body = body == null ? lessOrEqual : Expression.AndAlso(body, lessOrEqual);
        }

        if (body == null) return query;

        var lambda = Expression.Lambda<Func<T, bool>>(body, param);

        return query.Where(lambda);
    }

    public static IQueryable<T> ApplyWhereRange<T>(this IQueryable<T> query, DateTime minValue, DateTime maxValue, Expression<Func<T, DateTime>> selector)
    {
        var param = selector.Parameters[0];
        Expression body = null!;

        if (!minValue.Equals(DateTime.MinValue))
        {
            var minValueUTC = DateTime.SpecifyKind(minValue.Date, DateTimeKind.Local).ToUniversalTime();

            var minConstant = Expression.Constant(minValueUTC, typeof(DateTime));
            var greaterOrEqual = Expression.GreaterThanOrEqual(selector.Body, minConstant);
            body = greaterOrEqual;
        }

        if (!maxValue.Equals(DateTime.MinValue))
        {
            var maxValueUTC = DateTime.SpecifyKind(maxValue.Date, DateTimeKind.Local).ToUniversalTime();
            var maxUTC = maxValueUTC.AddDays(1).AddSeconds(-1);

            var maxConstant = Expression.Constant(maxUTC, typeof(DateTime));
            var lessOrEqual = Expression.LessThanOrEqual(selector.Body, maxConstant);

            body = body == null ? lessOrEqual : Expression.AndAlso(body, lessOrEqual);
        }

        if (body == null) return query;

        var lambda = Expression.Lambda<Func<T, bool>>(body, param);

        return query.Where(lambda);
    }



    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int page, int pageSize)
    {
        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}
