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
#endregion Imports

namespace Savage_Bot
{
    class Commands_Base
    {
        #region Command Logs
        public static void Log(CommandContext ctx)
        {
            string log_time = "[" + DateTime.Now + "][Client] Command <" + ctx.Command.Name.ToString() + "> from group [Dev] issued by " + ctx.Member.DisplayName.ToString() + " in guild " + ctx.Guild.Name.ToString() + ".";
            Console.WriteLine(log_time);
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
            member.Title = " - Commands";
            member.Description = "Shows all the available commands";
            member.Thumbnail = new DiscordEmbedThumbnail { Url = "http://i2.kym-cdn.com/entries/icons/original/000/020/309/nice.jpg" };
            member.Author = new DiscordEmbedAuthor { Name = "Available Commands: ", IconUrl = "http://i2.kym-cdn.com/entries/icons/original/000/020/309/nice.jpg" };
            member.Footer = new DiscordEmbedFooter { Text = "Made by Savage Bot.", IconUrl = "https://cdn.discordapp.com/embed/avatars/0.png" };            
            await ctx.RespondAsync("", false, member);
            C_Log("Output", "Info", "Available Commands: ");
            C_Log("Output", "Info", " - Commands");
            C_Log("Output", "Info", " Shows all the available commands");
            C_Log("Output", "Info", "Made by Savage Bot.");

        }
        #endregion Show Commands

        [Command("Credits")]
        [Aliases("credits")]
        [Hidden]
        [Description("Shows info about source code and developers.")]
        public async Task Base_Credits(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbed member = new DiscordEmbed();
            member.Color = 11564709;
            member.Title = "Developed by: ";
            member.Description = "<@!212231937039794176>";
            member.Thumbnail = new DiscordEmbedThumbnail { Url = "https://cdn.discordapp.com/avatars/212231937039794176/fd5962d4dbba1a1f6b15349575e0ddda.png?size=1024" };
            member.Fields.Add(new DiscordEmbedField { Name = "Source Code: ", Value = "[GitHub](https://github.com/RikMachado/SavageBot)" });
            member.Fields.Add(new DiscordEmbedField { Name = "Library used: ", Value = "[DSharpPlus](https://github.com/NaamloosDT/DSharpPlus/)" });
            member.Author = new DiscordEmbedAuthor { Name = "Credits" };
            member.Footer = new DiscordEmbedFooter { Text = "Made by Savage Bot", IconUrl = "https://cdn.discordapp.com/embed/avatars/0.png" };
            await ctx.RespondAsync("", false, member);
        }

        [Command("GetInfo")]
        [Aliases("getinfo")]
        [Hidden]
        [Description("Debug test command. Change for whatever needed.")]
        public async Task Base_Test(CommandContext ctx, DiscordMember memb)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("```Display name: " + memb.DisplayName + ",\n" +
                                   "User name: " + memb.Username + ",\n" +
                                   "4 digit ID: " + memb.Discriminator + ",\n" +
                                   "Full ID: " + memb.Id.ToString() + ",\n" +
                                   "Mention Code: " + memb.Mention + ",\n" +
                                   "Avatar URL: " + memb.AvatarUrl + ".```");
        }
    }
}
