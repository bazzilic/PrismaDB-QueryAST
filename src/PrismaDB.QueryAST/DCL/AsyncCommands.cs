﻿using PrismaDB.QueryAST.DDL;
using PrismaDB.QueryAST.DML;
using System.Collections.Generic;

namespace PrismaDB.QueryAST.DCL
{
    public abstract class AsyncCommand : Command
    {
        public bool StatusCheck;
    }

    public class KeysUpdateCommand : AsyncCommand
    {
        public KeysUpdateCommand(bool statusCheck = false) { StatusCheck = statusCheck; }

        public override List<TableRef> GetTables() => new List<TableRef>();

        public override List<ConstantContainer> GetConstants() => new List<ConstantContainer>();

        public override string ToString() => DialectResolver.Dialect.KeysUpdateCommandToString(this);

        public override object Clone() => new KeysUpdateCommand(StatusCheck);
    }

    public class EncryptColumnCommand : AsyncCommand
    {
        public ColumnRef Column;
        public ColumnEncryptionFlags EncryptionFlags;

        public EncryptColumnCommand(bool statusCheck = false)
            : this(new ColumnRef(""), ColumnEncryptionFlags.None, statusCheck) { }

        public EncryptColumnCommand(string columnName, ColumnEncryptionFlags encryptionFlags, bool statusCheck = false)
            : this(new ColumnRef(columnName), encryptionFlags, statusCheck) { }

        public EncryptColumnCommand(ColumnRef column, ColumnEncryptionFlags encryptionFlags, bool statusCheck = false)
        {
            Column = column;
            EncryptionFlags = encryptionFlags;
            StatusCheck = statusCheck;
        }

        public override List<TableRef> GetTables() => new List<TableRef>();

        public override List<ConstantContainer> GetConstants() => new List<ConstantContainer>();

        public override string ToString() => DialectResolver.Dialect.EncryptColumnCommandToString(this);

        public override object Clone() => new EncryptColumnCommand((ColumnRef)Column.Clone(), EncryptionFlags, StatusCheck);
    }

    public class DecryptColumnCommand : AsyncCommand
    {
        public ColumnRef Column;

        public DecryptColumnCommand(bool statusCheck = false)
            : this(new ColumnRef(""), statusCheck) { }

        public DecryptColumnCommand(string columnName, bool statusCheck = false)
            : this(new ColumnRef(columnName), statusCheck) { }

        public DecryptColumnCommand(ColumnRef column, bool statusCheck = false)
        {
            Column = column;
            StatusCheck = statusCheck;
        }

        public override List<TableRef> GetTables() => new List<TableRef>();

        public override List<ConstantContainer> GetConstants() => new List<ConstantContainer>();

        public override string ToString() => DialectResolver.Dialect.DecryptColumnCommandToString(this);

        public override object Clone() => new DecryptColumnCommand((ColumnRef)Column.Clone(), StatusCheck);
    }

    public class OpetreeRebuildCommand : AsyncCommand
    {
        public OpetreeRebuildCommand(bool statusCheck = false) { StatusCheck = statusCheck; }

        public override List<TableRef> GetTables() => new List<TableRef>();

        public override List<ConstantContainer> GetConstants() => new List<ConstantContainer>();

        public override string ToString() => DialectResolver.Dialect.OpetreeRebuildCommandToString(this);

        public override object Clone() => new OpetreeRebuildCommand(StatusCheck);
    }

    public class OpetreeRebalanceCommand : AsyncCommand
    {
        public RebalanceStopType StopType;
        public DecimalConstant StopAfter;

        public OpetreeRebalanceCommand(bool statusCheck = false)
        {
            StopType = RebalanceStopType.FULL;
            StopAfter = new DecimalConstant(1);
            StatusCheck = statusCheck;
        }

        public OpetreeRebalanceCommand(RebalanceStopType stopType = RebalanceStopType.FULL, decimal stopAfter = 1, bool statusCheck = false)
        {
            StopType = stopType;
            StopAfter = new DecimalConstant(stopAfter);
            StatusCheck = statusCheck;
        }

        public override List<TableRef> GetTables() => new List<TableRef>();

        public override List<ConstantContainer> GetConstants() => new List<ConstantContainer>();

        public override string ToString() => DialectResolver.Dialect.OpetreeRebalanceCommandToString(this);

        public override object Clone() => new OpetreeRebalanceCommand(StopType, StopAfter.decimalvalue, StatusCheck);
    }

    public enum RebalanceStopType
    {
        FULL,
        IMMEDIATE,
        ITERATIONS,
        HOURS,
        MINUTES
    }
}
