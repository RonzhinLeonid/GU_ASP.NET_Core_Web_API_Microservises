using AutoMapper;
using MetricsAgent.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SQLite;
using FluentMigrator.Runner;
using Quartz;
using Quartz.Spi;
using Quartz.Impl;
using MetricsAgent.Jobs;


namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSingleton(new SQLiteConnection(SQLConnectionString.ConnectionString));
            //ConfigureSqlLiteConnection(services);
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // ��������� ��������� SQLite 
                    .AddSQLite()
                    // ������������� ������ �����������
                    .WithGlobalConnectionString(SQLConnectionString.ConnectionString)
                    // ������������ ��� ������ ������ � ����������
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            // ��������� �������
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();
            // ��������� ���� ������
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<DotnetMetricJob>();
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricJob),
                cronExpression: "0/5 * * * * ?")); // ��������� ������ 5 ������
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DotnetMetricJob),
                cronExpression: "0/5 * * * * ?"));            
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HddMetricJob),
                cronExpression: "0/5 * * * * ?"));            
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricJob),
                cronExpression: "0/5 * * * * ?"));            
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RamMetricJob),
                cronExpression: "0/5 * * * * ?"));


        }

        //private void ConfigureSqlLiteConnection(IServiceCollection services)
        //{
        //    const string connectionString = SQLConnectionString.ConnectionString;
        //    var connection = new SQLiteConnection(connectionString);
        //    connection.Open();
        //    //PrepareSchema(connection);
        //}

        //private void PrepareSchema(SQLiteConnection connection)
        //{
            
        //    using (var command = new SQLiteCommand(connection))
        //    {
        //        // ������ ����� ����� ������� ��� ����������
        //        // ������� ������� � ��������� ���� ��� ���������� � ���� ������
        //        command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
        //        // ���������� ������ � ���� ������
        //        command.ExecuteNonQuery();
        //        //�������� ������� cpumetrics
        //        command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY,
        //            value INT, time LONG)";
        //        command.ExecuteNonQuery();
        //        // ���������� ������ ���������� ��� ������
        //        Random rnd = new Random(10);
        //        for (int i = 1; i <= 10; i++)
        //        {
        //            command.CommandText = "INSERT INTO cpumetrics(value,time) VALUES (@value,@time);";
        //            command.Parameters.AddWithValue("@value", rnd.Next(100));
        //            command.Parameters.AddWithValue("@time", new DateTimeOffset(new DateTime(2021, 5, i)).ToUnixTimeSeconds());
        //            command.Prepare();
        //            command.ExecuteNonQuery(); 
        //        }

        //        // ������� ������� � ��������� ���� ��� ���������� � ���� ������
        //        command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
        //        // ���������� ������ � ���� ������
        //        command.ExecuteNonQuery();
        //        //�������� ������� dotnetmetrics
        //        command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY,
        //            value INT, time LONG)";
        //        command.ExecuteNonQuery();
        //        // ���������� ������ ���������� ��� ������
        //        for (int i = 1; i <= 10; i++)
        //        {
        //            command.CommandText = "INSERT INTO dotnetmetrics(value,time) VALUES (@value,@time);";
        //            command.Parameters.AddWithValue("@value", rnd.Next(200));
        //            command.Parameters.AddWithValue("@time", new DateTimeOffset(new DateTime(2021, 5, i)).ToUnixTimeSeconds());
        //            command.Prepare();
        //            command.ExecuteNonQuery();
        //        }

        //        // ������� ������� � ��������� ���� ��� ���������� � ���� ������
        //        command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
        //        // ���������� ������ � ���� ������
        //        command.ExecuteNonQuery();
        //        //�������� ������� hddmetrics
        //        command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY,
        //            value INT, time LONG)";
        //        command.ExecuteNonQuery();
        //        // ���������� ������ ���������� ��� ������
        //        for (int i = 1; i <= 10; i++)
        //        {
        //            command.CommandText = "INSERT INTO hddmetrics(value,time) VALUES (@value,@time);";
        //            command.Parameters.AddWithValue("@value", rnd.Next(10000));
        //            command.Parameters.AddWithValue("@time", new DateTimeOffset(new DateTime(2021, 5, i)).ToUnixTimeSeconds());
        //            command.Prepare();
        //            command.ExecuteNonQuery();
        //        }

        //        // ������� ������� � ��������� ���� ��� ���������� � ���� ������
        //        command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
        //        // ���������� ������ � ���� ������
        //        command.ExecuteNonQuery();
        //        //�������� ������� networkmetrics
        //        command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY,
        //            value INT, time LONG)";
        //        command.ExecuteNonQuery();
        //        // ���������� ������ ���������� ��� ������
        //        for (int i = 1; i <= 10; i++)
        //        {
        //            command.CommandText = "INSERT INTO networkmetrics(value,time) VALUES (@value,@time);";
        //            command.Parameters.AddWithValue("@value", rnd.Next(100));
        //            command.Parameters.AddWithValue("@time", new DateTimeOffset(new DateTime(2021, 5, i)).ToUnixTimeSeconds());
        //            command.Prepare();
        //            command.ExecuteNonQuery();
        //        }

        //        // ������� ������� � ��������� ���� ��� ���������� � ���� ������
        //        command.CommandText = "DROP TABLE IF EXISTS rammetrics";
        //        // ���������� ������ � ���� ������
        //        command.ExecuteNonQuery();
        //        //�������� ������� rammetrics
        //        command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY,
        //            value INT, time LONG)";
        //        command.ExecuteNonQuery();
        //        // ���������� ������ ���������� ��� ������
        //        for (int i = 1; i <= 10; i++)
        //        {
        //            command.CommandText = "INSERT INTO rammetrics(value,time) VALUES (@value,@time);";
        //            command.Parameters.AddWithValue("@value", rnd.Next(1000));
        //            command.Parameters.AddWithValue("@time", new DateTimeOffset(new DateTime(2021, 5, i)).ToUnixTimeSeconds());
        //            command.Prepare();
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // ��������� ��������
            migrationRunner.MigrateUp();
        }
    }
}
