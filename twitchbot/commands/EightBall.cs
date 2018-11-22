using System;
using System.Linq;
using twitchbot.infrastructure;

namespace twitchbot.commands
{
    public class EightBall : ITwitchCommand
    {
        private readonly string[] _answers = new[]
        {
            "It is certain.",
            "Ask again later.",
            "Outlook not so good",
            "Signs point to yes",
            "Most likely",
            "My reply is no.",
            "Reply hazy, try again.",
            "As I see it, yes.",
            "Without a doubt"
        };
        
        public string Execute(params string[] args)
        {
            if (!args.Any())
            {
                return "I'm not a mind reader, ask your question";
            }
            
            var answer = new Random().Next() % _answers.Length;

            return _answers[answer];
        }
    }
}