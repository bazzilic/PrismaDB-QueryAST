﻿using PrismaDB.Commons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismaDB.QueryAST.DML
{
    public class OrderByClause : ICloneable
    {
        public List<Pair<ColumnRef, OrderDirection>> OrderColumns;

        public OrderByClause()
        {
            OrderColumns = new List<Pair<ColumnRef, OrderDirection>>();
        }

        public OrderByClause(OrderByClause other)
        {
            OrderColumns = new List<Pair<ColumnRef, OrderDirection>>(other.OrderColumns.Capacity);
            OrderColumns.AddRange(other.OrderColumns.Select(
                x => new Pair<ColumnRef, OrderDirection>(x.First.Clone() as ColumnRef, x.Second)));
        }

        public object Clone()
        {
            return new OrderByClause(this);
        }

        public override string ToString()
        {
            return DialectResolver.Dialect.OrderByClauseToString(this);
        }

        public List<ColumnRef> GetOrderByColumns()
        {
            var orderByCols = OrderColumns.SelectMany(x => x.First.GetColumns());
            return orderByCols.Distinct().ToList();
        }
    }

    public enum OrderDirection
    {
        ASC,
        DESC
    }
}