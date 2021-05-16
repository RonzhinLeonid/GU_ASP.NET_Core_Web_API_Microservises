using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace MetricsAgent.DAL.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            //создание таблицы cpumetrics
            Create.Table("cpumetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            //создание таблицы dotnetmetrics
            Create.Table("dotnetmetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            //создание таблицы networkmetrics
            Create.Table("networkmetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            //создание таблицы hddmetrics
            Create.Table("hddmetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            //создание таблицы rammetrics
            Create.Table("rammetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
        }

        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("rammetrics");
        }
    }
}
