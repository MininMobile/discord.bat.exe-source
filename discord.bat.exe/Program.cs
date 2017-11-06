using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace discord.bat.exe
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new Program().StartAsync().GetAwaiter().GetResult();
            }
            catch
            {
                Console.WriteLine("Connection Error");
                Console.WriteLine("You might have entered the wrong token!");

                Console.ReadKey();
            }
        }

        private DiscordSocketClient _client;
        private CommandHandler _handler;

        public async Task StartAsync()
        {
            _client = new DiscordSocketClient();
            new CommandHandler();

            await _client.LoginAsync(TokenType.Bot, File.ReadAllLines("..\\TEMP\\bot.token.dat")[0]);
            await _client.StartAsync();

            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);

            Console.WriteLine("Connected!");
            await Task.Delay(-1);
        }
    }
}
