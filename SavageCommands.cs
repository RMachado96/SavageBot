using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;


namespace DspBot
{
    public class Commands_Base
    {
        string victim;

        [Command("ping")] // let's define this method as a command
        [Description("Example ping command")] // this will be displayed to tell users what this command does when they invoke help
        [Aliases("pong")] // alternative names for the command
        public async Task Ping(CommandContext ctx) // this command takes no arguments
        {
            // let's trigger a typing indicator to let
            // users know we're working
            await ctx.TriggerTypingAsync();

            // let's make the message a bit more colourful
            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");

            // respond with ping
            await ctx.RespondAsync($"{emoji} Pong! Ping: {ctx.Client.Ping}ms");
        }



        [Command("savage")]
        [Description("Random insults against someone")]
        public async Task FuckPedro(CommandContext ctx, [Description("Mention user to destroy")] DiscordMember memb)
        {
            victim = memb.DisplayName;
            
            await ctx.TriggerTypingAsync();
            var emoji = DiscordEmoji.FromName(ctx.Client, ":Triggered:");

            Random rnd = new Random();
            int inslt = rnd.Next(1, 4);

            switch (inslt)
            {
                case 1:
                    await ctx.RespondAsync(emoji + " " + victim + ", if you were anymore inbred you would be a sandwich. " + emoji);
                    break;
                case 2:
                    await ctx.RespondAsync(emoji + " Shut up " + victim + ", you'll never be the man your mother is. " + emoji);
                    break;
                case 3:
                    await ctx.RespondAsync(emoji + " " + victim + ", you're a dumbass. " + emoji);
                    break;

                    
            }                       
        }
    }

    //[Group("Admin")] // let's mark this class as a command group
    //[Description("Administrative commands.")] // give it a description for help purposes
    //[Hidden] // let's hide this from the eyes of curious users
    public class Commands_Admin
    {
        [Command("sudo"), Description("Executes a command as another user."), Hidden, /*RequireOwner*/]
        public async Task Sudo(CommandContext ctx, [Description("Member to execute as.")] DiscordMember member, [RemainingText, Description("Command text to execute.")] string command)
        {
            // note the [RemainingText] attribute on the argument.
            // it will capture all the text passed to the command

            // let's trigger a typing indicator to let
            // users know we're working
            await ctx.TriggerTypingAsync();

            // get the command service, we need this for
            // sudo purposes
            var cmds = ctx.Client.GetCommandsNext();

            // and perform the sudo
            await cmds.SudoAsync(member, ctx.Channel, command);
        }
    }
}
