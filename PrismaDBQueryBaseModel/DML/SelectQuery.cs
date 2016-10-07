﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PrismaDBQueryBaseModel.DML
{
    public class SelectQuery : DMLQuery
    {
        public List<Expression> SelectExpressions;
        public List<TableRef> FromTables;
        public WhereClause Where;

        public SelectQuery()
        {
            SelectExpressions = new List<Expression>();
            FromTables = new List<TableRef>();
            Where = new WhereClause();
        }

        public SelectQuery(SelectQuery other)
        {
            SelectExpressions = new List<Expression>(other.SelectExpressions.Capacity);
            foreach (var expr in other.SelectExpressions)
                SelectExpressions.Add(expr.Clone());

            FromTables = new List<TableRef>(other.FromTables.Capacity);
            foreach (var tref in other.FromTables)
                FromTables.Add(tref.Clone());

            Where = new WhereClause(other.Where);
        }

        public override string ToString()
        {
            var sb = new StringBuilder("SELECT ");

            sb.Append(String.Join(", ", SelectExpressions));

            if (FromTables.Count != 0)
            {
                sb.Append(" FROM ");
                sb.Append(String.Join(", ", FromTables));
            }

            sb.Append(Where.ToString());

            return sb.ToString();
        }
    }
}
