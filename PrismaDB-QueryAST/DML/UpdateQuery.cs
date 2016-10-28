﻿using PrismaDB.Commons;
using System.Collections.Generic;
using System.Text;

namespace PrismaDB.QueryAST.DML
{
    public class UpdateQuery : DMLQuery
    {
        public TableRef UpdateTable;
        public List<Pair<ColumnRef, Constant>> UpdateExpressions;
        public WhereClause Where;

        public UpdateQuery()
        {
            UpdateTable = new TableRef("");
            UpdateExpressions = new List<Pair<ColumnRef, Constant>>();
            Where = new WhereClause();
        }

        public UpdateQuery(UpdateQuery other)
        {
            UpdateTable = other.UpdateTable.Clone();

            UpdateExpressions = new List<Pair<ColumnRef, Constant>>(other.UpdateExpressions.Count);
            foreach (var pr in other.UpdateExpressions)
            {
                var newpr = new Pair<ColumnRef, Constant>((ColumnRef)(pr.First.Clone()), (Constant)pr.Second.Clone());
                UpdateExpressions.Add(newpr);
            }

            Where = new WhereClause(other.Where);
        }

        public override string ToString()
        {
            var sb = new StringBuilder("UPDATE ");

            sb.Append(UpdateTable.ToString());

            sb.Append(" SET ");

            var first = true;
            foreach (var pr in UpdateExpressions)
            {
                if (!first)
                    sb.Append(" , ");
                first = false;
                sb.Append(pr.First.ToString());
                sb.Append(" = ");
                sb.Append(pr.Second.ToString());
            }
            sb.Append(" ");

            sb.Append(Where.ToString());

            return sb.ToString();
        }
    }
}