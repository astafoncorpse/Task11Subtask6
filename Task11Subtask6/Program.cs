using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using VoiceTexterBot.Configuration;
using VoiceTexterBot.Controllers;
using VoiceTexterBot.Services;

namespace VoiceTexterBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddSingleton<IStorage, MemoryStorage>();

            services.AddTransient<DefaultMessageControler>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
           
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient
            ("5985099361:AAHKR5yquG5kjCMkD0a569xxem2P9G2w-Y0"));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }
        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "5985099361:AAHKR5yquG5kjCMkD0a569xxem2P9G2w-Y0"
            };
        }
    }
}