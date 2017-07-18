using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;


namespace Savage_Bot
{
    class Program
    {
        #region Vars and Objects
        public bool Is_Win_7 = false;

        public string Savage_Config_Path = "D:\\C#\\Savage Bot\\Alpha_0_1\\SavageCommands\\SavageCommands\\Configs\\Savage_Config.json";

        public DiscordClient Client { get; set; }

        public CommandsNextModule Commands { get; set; }


        #endregion Vars and Objects

        #region Main
        static void Main(string[] args)
        {

            var prog = new Program();
            prog.Run().GetAwaiter().GetResult();
        }
        #endregion Main

        #region Json Loader
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        #endregion Json Loader

        #region Program Logs
        public void Log(string source, string level, string log)
        {
            string log_time = "[" + DateTime.Now + "]";
            string log_source = "[" + source + "]";
            if (level == "Warning" || level == "warning")
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (level == "Critical" || level == "critical")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            Console.WriteLine(log_time + log_source + log);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Blue;
        }
        #endregion Program Logs

        #region Running System
        public async Task Run()
        {
            Log("Server", "Critical", "Savage bot is starting.");

            #region Misc Variables
            Log("Server", "Critical", "Initializing misc. variables.");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;           
            Console.Title = "Savage Bot";
            Log("Server", "Critical", "Variables initialized.");
            #endregion Misc Variables

            #region Client Config setup
            Log("Server", "Critical", "Setting up client config.");
            DiscordConfig savage_config = new DiscordConfig
            {
                AutoReconnect = true,
                DiscordBranch = Branch.Stable,
                LargeThreshold = 250,
                LogLevel = LogLevel.Debug,
                Token = "MzM1MDg3ODgyNTM0Mzg3NzEy.DElJDw.RSeek-v_OL9Q0DEsJuvMMpixMv8",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = false
            };          
            Log("Server", "Critical", "Loading configs.");            
            this.Client = new DiscordClient(savage_config);
            Log("Server", "Critical", "Configs loaded.");
            #endregion Client Config setup

            #region Command Configs and Prefix
            Log("Server", "Critical", "Setting up Commands config and prefix.");
            var CommandConfig = new CommandsNextConfiguration
            {
                StringPrefix = "#",

                EnableMentionPrefix = true,

                EnableDms = false
            };
            this.Commands = this.Client.UseCommandsNext(CommandConfig);
            Log("Server", "Critical", "Commands config loaded.");
            #endregion Command Configs and Prefix

            #region Command Loader
            Log("Server", "Critical", "Loading command classes.");
            this.Commands.RegisterCommands<Commands_Base>();
            Log("Server", "Critical", "Command classes loaded.");
            #endregion Command Loader

            #region Bot Connection and Task Looping
            Log("Server", "Critical", "Starting bot connection.");
            var discord_conn = new DiscordConnection();
            if (Is_Win_7 == true)
            {
                this.Client.SetWebSocketClient<WebSocket4NetClient>();
            }
            await this.Client.ConnectAsync();
            Log("Server", "Critical", "Bot connected.");
            Log("Server", "Info", "Enjoy your stay.");
            await Task.Delay(-1);
            #endregion Bot Connection and Task Looping
        }
        #endregion Running System
    }
}