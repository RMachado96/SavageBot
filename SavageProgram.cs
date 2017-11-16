using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Web;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;


namespace DspBot
{
    class Program
    {
        public DiscordClient Client { get; set; }

        public CommandsNextModule Commands { get; set; }


        static void Main(string[] args)
        {
           
            var prog = new Program();
            prog.Run().GetAwaiter().GetResult();
        }

        public async Task Run()
        {

            var savage_config = new DiscordConfig
            {
                AutoReconnect = true,
                DiscordBranch = Branch.Stable,
                LargeThreshold = 250,
                LogLevel = LogLevel.Info,
                Token = "",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = false

            };
    
            this.Client = new DiscordClient(savage_config);

            var CommandConfig = new CommandsNextConfiguration
            {
                StringPrefix = "#",

                EnableMentionPrefix = true,

                EnableDms = false
            };

            this.Commands = this.Client.UseCommandsNext(CommandConfig);

            this.Commands.RegisterCommands<Commands_Base>();

            this.Commands.RegisterCommands<Commands_Admin>();

            var discord_conn = new DiscordConnection();

            this.Client.SetWebSocketClient<WebSocket4NetClient>();

            await this.Client.ConnectAsync();

            await Task.Delay(-1);

        }
    }
}
