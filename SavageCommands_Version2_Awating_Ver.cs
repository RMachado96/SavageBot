#region Imports
using System;
using System.Net.NetworkInformation;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Text;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;
using System.Management;
using Newtonsoft.Json;
#endregion Imports

namespace Savage_Bot
{
    class Commands_Base
    {
        public string Dictionary_Path = "D:\\C#\\Savage Bot\\Alpha_0_1\\SavageCommands\\SavageCommands\\Configs\\Command_Dictionary.json";

        #region Command Logs
        public static void Log(CommandContext ctx)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string log_time = "[" + DateTime.Now + "][Client] Command <" + ctx.Command.Name.ToString() + "> from group [Dev] issued by " + ctx.Member.DisplayName.ToString() + " in guild " + ctx.Guild.Name.ToString() + ".";
            Console.WriteLine(log_time);
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        #endregion Command Logs

        #region Program Logs
        public void C_Log(string source, string level, string log)
        {
            string log_time = "[" + DateTime.Now + "]";
            string log_source = "[" + source + "] ";
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

        #region Show Commands
        [Command("Commands")]
        [Aliases("commands")]
        [Description("Show currently available commands.")]
        public async Task Base_Help(CommandContext ctx)
        {
            Log(ctx);
            await ctx.TriggerTypingAsync();
            DiscordEmbed member = new DiscordEmbed();
            member.Color = 11564709;
            member.Thumbnail = new DiscordEmbedThumbnail { Url = "http://www.dndjunkie.com/civilopedia/images/large/RESOURCE_BANANA.png" };
            member.Author = new DiscordEmbedAuthor { Name = "Available Commands: ", IconUrl = "http://www.dndjunkie.com/civilopedia/images/large/RESOURCE_BANANA.png" };
            member.Footer = new DiscordEmbedFooter { Text = "Made by Savage Bot.", IconUrl = "https://cdn.discordapp.com/embed/avatars/0.png" };
            Dictionary<string, string> Command_Dict = new Dictionary<string, string>();
            Command_Dict = ReadFromJsonFile<Dictionary<string, string>>(Dictionary_Path);
            C_Log("Output", "Info", "Available Commands: ");
            foreach (var item in Command_Dict)
            {
                member.Fields.Add(new DiscordEmbedField { Name = item.Key, Value = item.Value });
                C_Log("Output", "Info", " -" + item.Key);
                C_Log("Output", "Info", " " + item.Value);
            }
            await ctx.RespondAsync("", false, member);
        }
        #endregion Show Commands

        #region Credits
        [Command("Credits")]
        [Aliases("credits")]
        [Hidden]
        [Description("Shows info about source code and developers.")]
        public async Task Base_Credits(CommandContext ctx)
        {
            Log(ctx);
            await ctx.TriggerTypingAsync();
            DiscordEmbed member = new DiscordEmbed();
            member.Color = 11564709;
            member.Title = "Developed by: ";
            member.Description = "<@!212231937039794176> \n<@!110141872948645888>";
            member.Thumbnail = new DiscordEmbedThumbnail { Url = "https://cdn.discordapp.com/avatars/212231937039794176/fd5962d4dbba1a1f6b15349575e0ddda.png?size=1024" };
            member.Fields.Add(new DiscordEmbedField { Name = "Source Code: ", Value = "[GitHub](https://github.com/RikMachado/SavageBot)" });
            member.Fields.Add(new DiscordEmbedField { Name = "Library used: ", Value = "[DSharpPlus](https://github.com/NaamloosDT/DSharpPlus/)" });
            member.Author = new DiscordEmbedAuthor { Name = "Credits" };
            member.Footer = new DiscordEmbedFooter { Text = "Made by Savage Bot", IconUrl = "https://cdn.discordapp.com/embed/avatars/0.png" };
            await ctx.RespondAsync("", false, member);
            C_Log("Output", "Info", "Showing Credits card.");
        }
        #endregion Credits       

        #region Get info tool for bot development
        [Command("GetInfo")]
        [Aliases("getinfo")]
        [Hidden]
        [Description("Debug test command. Change for whatever needed.")]
        public async Task Base_Test(CommandContext ctx, DiscordMember memb)
        {
            Log(ctx);
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("```Display name: " + memb.DisplayName + ",\n" +
                                   "User name: " + memb.Username + ",\n" +
                                   "4 digit ID: " + memb.Discriminator + ",\n" +
                                   "Full ID: " + memb.Id.ToString() + ",\n" +
                                   "Mention Code: " + memb.Mention + ",\n" +
                                   "Avatar URL: " + memb.AvatarUrl + ".```");

            C_Log("Output", "Info", "Display name: " + memb.DisplayName + ".");
            C_Log("Output", "Info", "User name: " + memb.Username + ".");
            C_Log("Output", "Info", "4 digit ID: " + memb.Discriminator + ".");
            C_Log("Output", "Info", "Full ID: " + memb.Id.ToString() + ".");
            C_Log("Output", "Info", "Mention Code: " + memb.Mention + ".");
            C_Log("Output", "Info", "Avatar URL: " + memb.AvatarUrl + ".");
        }
        #endregion Get info tool for bot development

        //Commands can be added through here and the file itself. This command will eventually be deleted.
        //Every command can be fully edited on the .json file.
        #region Add command to dict
        [Command("new_c")]
        [Description("Create a new command")]
        public async Task test(CommandContext ctx, [Description("Command name")] string command_name, [Description("Command result")] [RemainingText] string command_func)
        {
            Log(ctx);
            await ctx.TriggerTypingAsync();
            Dictionary<string, string> Command_Dict = new Dictionary<string, string>();
            Command_Dict = ReadFromJsonFile<Dictionary<string, string>>(Dictionary_Path);
            Command_Dict.Add(command_name, command_func);
            await ctx.RespondAsync("Command <" + command_name + "> with the description: \"" + command_func + "\" was added.");
            C_Log("Output", "Info", "Command <" + command_name + "> with the description: \"" + command_func + "\" was added.");
            #region Save to Json
            var json_obj_list = JsonConvert.SerializeObject(Command_Dict);
            TextWriter writer_obj = new StreamWriter("D:\\C#\\Savage Bot\\Alpha_0_1\\SavageCommands\\SavageCommands\\Configs\\Command_Dictionary.json", false);
            writer_obj.Write(json_obj_list);
            writer_obj.Close();
            #endregion Save to Json

        }
        #endregion Add command to dict
    }
}
