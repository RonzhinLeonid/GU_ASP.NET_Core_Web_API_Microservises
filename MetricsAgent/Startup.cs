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
            ConfigureSqlLiteConnection(services);
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            
            using (var command = new SQLiteCommand(connection))
            {
                // задаем новый текст команды для выполнения
                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();
                //создание таблицы cpumetrics
                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY,
                    value INT, time LONG)";
                command.ExecuteNonQuery();
                // заполнение талицы значениями для тестов
                Random rnd = new Random(10);
                for (int i = 1; i <= 10; i++)
                {
                    command.CommandText = "INSERT INTO cpumetrics(value,time) VALUES (@value,@time);";
                    command.Parameters.AddWithValue("@value", rnd.Next(100));
                    command.Parameters.AddWithValue("@time", new DateTimeOffset(new DateTime(2021, 5, i)).ToUnixTimeSeconds());
                    command.Prepare();
                    command.ExecuteNonQuery(); 
                }

                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();
                //создание таблицы dotnetmetrics
                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY,
                    value INT, time LONG)";
                command.ExecuteNonQuery();
                // заполнение талицы значениями для тестов
                for (int i = 1; i <= 10; i++)
                {
                    command.CommandText = "INSERT INTO dotnetmetrics(value,time) VALUES (@value,@time);";
                    command.Parameters.AddWithValue("@value", rnd.Next(200));
                    command.Parameters.AddWithValue("@time", new DateTimeOffset(new DateTime(2021, 5, i)).ToUnixTimeSeconds());
                    command.Prepare();
                    command.ExecuteNonQuery();
                }

                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();
                //создание таблицы hddmetrics
                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY,
                    value INT, time LONG)";
                command.ExecuteNonQuery();
                // заполнение талицы значениями для тестов
                for (int i = 1; i <= 10; i++)
                {
                    command.CommandText = "INSERT INTO hddmetrics(value,time) VALUES (@value,@time);";
                    command.Parameters.AddWithValue("@value", rnd.Next(10000));
                    command.Parameters.AddWithValue("@time", new DateTimeOffset(new DateTime(2021, 5, i)).ToUnixTimeSeconds());
                    command.Prepare();
                    command.ExecuteNonQuery();
                }

                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();
                //создание таблицы hddmetrics
                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY,
                    value INT, time LONG)";
                command.ExecuteNonQuery();
                // заполнение талицы значениями для тестов
                for (int i = 1; i <= 10; i++)
                {
                    command.CommandText = "INSERT INTO networkmetrics(value,time) VALUES (@value,@time);";
                    command.Parameters.AddWithValue("@value", rnd.Next(100));
                    command.Parameters.AddWithValue("@time", new DateTimeOffset(new DateTime(2021, 5, i)).ToUnixTimeSeconds());
                    command.Prepare();
                    command.ExecuteNonQuery();
                }

                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();
                //создание таблицы hddmetrics
                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY,
                    value INT, time LONG)";
                command.ExecuteNonQuery();
                // заполнение талицы значениями для тестов
                for (int i = 1; i <= 10; i++)
                {
                    command.CommandText = "INSERT INTO rammetrics(value,time) VALUES (@value,@time);";
                    command.Parameters.AddWithValue("@value", rnd.Next(1000));
                    command.Parameters.AddWithValue("@time", new DateTimeOffset(new DateTime(2021, 5, i)).ToUnixTimeSeconds());
                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
