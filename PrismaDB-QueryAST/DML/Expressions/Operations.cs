﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrismaDB.QueryAST.DML
{
    public abstract class Operation : Expression
    {
        protected Expression _left, _right;

        public Expression left
        {
            get { return _left; }
            set
            {
                _left = value;
                _left.Parent = this;
            }
        }

        public Expression right
        {
            get { return _right; }
            set
            {
                _right = value;
                _right.Parent = this;
            }
        }
    }

    public class Addition : Operation
    {
        public Addition(Expression left, Expression right)
        {
            setValue(left, right, "");
        }

        public Addition(Expression left, Expression right, string ColumnName)
        {
            setValue(left, right, ColumnName);
        }

        public override void setValue(params object[] value)
        {
            if (value.Length < 2)
                throw new ArgumentException("Addition constructor expects 2 or 3 arguments");

            left = (Expression)value[0];
            right = (Expression)value[1];

            if (value.Length > 2)
                ColumnName = (string)value[2];
        }

        public override Expression Clone()
        {
            var left_clone = left.Clone();
            var right_clone = right.Clone();

            var clone = new Addition(left_clone, right_clone, ColumnName);

            return clone;
        }

        public override object Eval(DataRow r)
        {
            return (int)left.Eval(r) + (int)right.Eval(r);
        }

        public override List<ColumnRef> GetColumns()
        {
            var res = new List<ColumnRef>();
            res.AddRange(left.GetColumns());
            res.AddRange(right.GetColumns());
            return res;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(" ( ");
            sb.Append(left.ToString());
            sb.Append(" + ");
            sb.Append(right.ToString());
            sb.Append(" ) ");

            if (ColumnName.Length > 0)
            {
                sb.Append("AS [");
                sb.Append(ColumnName);
                sb.Append("] ");
            }

            return sb.ToString();
        }
    }

    public class Multiplication : Operation
    {
        public Multiplication(Expression left, Expression right)
        {
            setValue(left, right, "");
        }

        public Multiplication(Expression left, Expression right, string ColumnName)
        {
            setValue(left, right, ColumnName);
        }

        public override void setValue(params object[] value)
        {
            if (value.Length < 2)
                throw new ArgumentException("Multiplication constructor expects 2 or 3 arguments");

            left = (Expression)value[0];
            right = (Expression)value[1];

            if (value.Length > 2)
                ColumnName = (string)value[2];
        }

        public override Expression Clone()
        {
            var left_clone = left.Clone();
            var right_clone = right.Clone();

            var clone = new Multiplication(left_clone, right_clone, ColumnName);

            return clone;
        }

        public override object Eval(DataRow r)
        {
            return (int)left.Eval(r) * (int)right.Eval(r);
        }

        public override List<ColumnRef> GetColumns()
        {
            var res = new List<ColumnRef>();
            res.AddRange(left.GetColumns());
            res.AddRange(right.GetColumns());
            return res;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(" ( ");
            sb.Append(left.ToString());
            sb.Append(" * ");
            sb.Append(right.ToString());
            sb.Append(" ) ");

            if (ColumnName.Length > 0)
            {
                sb.Append("AS [");
                sb.Append(ColumnName);
                sb.Append("] ");
            }

            return sb.ToString();
        }
    }

    public class PaillierAddition : Operation
    {
        public Expression N;

        public PaillierAddition(Expression left, Expression right, Expression N)
        {
            setValue(left, right, N, "");
        }

        public PaillierAddition(Expression left, Expression right, Expression N, string ColumnName)
        {
            setValue(left, right, N, ColumnName);
        }

        public override void setValue(params object[] value)
        {
            if (value.Length < 3)
                throw new ArgumentException("PaillierAddition constructor expects 3 or 4 arguments");

            left = (Expression)value[0];
            right = (Expression)value[1];

            N = (Expression)value[2];

            if (value.Length > 3)
                ColumnName = (string)value[3];
        }

        public override Expression Clone()
        {
            var left_clone = left.Clone();
            var right_clone = right.Clone();
            var N_clone = N.Clone();

            var clone = new PaillierAddition(left_clone, right_clone, N_clone, ColumnName);

            return clone;
        }

        public override object Eval(DataRow r)
        {
            throw new NotImplementedException("This method should not be called.");
        }

        public override List<ColumnRef> GetColumns()
        {
            throw new NotImplementedException("This method should not be called.");
        }

        public override string ToString()
        {
            var sb = new StringBuilder("[dbo].[PaillierAddition] ( ");
            sb.Append(left.ToString());
            sb.Append(" , ");
            sb.Append(right.ToString());
            sb.Append(" , ");
            sb.Append(N.ToString());

            if (ColumnName.Length > 0)
            {
                sb.Append(" ) AS [");
                sb.Append(ColumnName);
                sb.Append("] ");
            }
            else
                sb.Append(" ) ");

            return sb.ToString();
        }
    }

    public class ElGamalMultiplication : Operation
    {
        public Expression P;

        public ElGamalMultiplication(Expression left, Expression right, Expression P)
        {
            setValue(left, right, P, "");
        }

        public ElGamalMultiplication(Expression left, Expression right, Expression P, string ColumnName)
        {
            setValue(left, right, P, ColumnName);
        }

        public override void setValue(params object[] value)
        {
            if (value.Length < 3)
                throw new ArgumentException("ElGamalMultiplication constructor expects 3 or 4 arguments");

            left = (Expression)value[0];
            right = (Expression)value[1];

            P = (Expression)value[2];

            if (value.Length > 3)
                ColumnName = (string)value[3];
        }

        public override Expression Clone()
        {
            var left_clone = left.Clone();
            var right_clone = right.Clone();
            var P_clone = P.Clone();

            var clone = new ElGamalMultiplication(left_clone, right_clone, P_clone, ColumnName);

            return clone;
        }

        public override object Eval(DataRow r)
        {
            throw new NotImplementedException("This method should not be called.");
        }

        public override List<ColumnRef> GetColumns()
        {
            throw new NotImplementedException("This method should not be called.");
        }

        public override string ToString()
        {
            var sb = new StringBuilder("[dbo].[ElGamalMultiplication] ( ");
            sb.Append(left.ToString());
            sb.Append(" , ");
            sb.Append(right.ToString());
            sb.Append(" , ");
            sb.Append(P.ToString());

            if (ColumnName.Length > 0)
            {
                sb.Append(" ) AS [");
                sb.Append(ColumnName);
                sb.Append("] ");
            }
            else
                sb.Append(" ) ");

            return sb.ToString();
        }
    }
}