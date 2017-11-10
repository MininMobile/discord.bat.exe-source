using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Discord;

namespace discord.bat.exe
{
    class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            await _service.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);

            Process myProcess = new Process();

            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = "eventManager.bat";
                myProcess.StartInfo.Arguments = "message \""+ msg.Content + "\"";

                myProcess.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await Task.Delay(100);
            if (File.Exists("update.dat"))
            {
                var update = File.ReadAllLines("update.dat")[0];
                File.Delete("update.dat");

                if (update.StartsWith("send"))
                {
                    update = update.Substring(4);
                    await msg.Channel.SendMessageAsync(update);
                } else if (update.StartsWith("dm"))
                {
                    update = update.Substring(2);
                    await msg.Author.SendMessageAsync(update);
                }
            }
        }
    }
}
