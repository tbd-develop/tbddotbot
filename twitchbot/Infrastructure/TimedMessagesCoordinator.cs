using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using twitchstreambot;

namespace twitchbot.Infrastructure
{
    public class TimedMessagesCoordinator
    {
        private readonly TwitchStreamBot _bot;
        private readonly IEnumerable<TimerData> _timerData;

        public TimedMessagesCoordinator(TwitchStreamBot bot)
        {
            _bot = bot;

            _timerData = from t in Assembly.GetExecutingAssembly().GetTypes()
                         where t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(x => x == typeof(ITwitchTimerAction))
                         let timerData = (TwitchTimerAttribute)t.GetCustomAttributes(typeof(TwitchTimerAttribute)).SingleOrDefault()
                         let action = (ITwitchTimerAction)Activator.CreateInstance(t)
                         select new TimerData
                         {
                             Action = action,
                             Period = TimeSpan.FromSeconds(timerData.Seconds),
                             Timer = new Timer((s) => action.OnTimer(_bot), null, TimeSpan.FromMilliseconds(-1),
                                 TimeSpan.FromSeconds(timerData.Seconds))
                         };
        }

        public void Start()
        {
            foreach (var data in _timerData)
            {
                data.Timer.Change(data.Period, data.Period);
            }
        }

        public void Stop()
        {
            foreach (var data in _timerData)
            {
                data.Timer.Dispose();
            }
        }
    }

    public class TimerData
    {
        public ITwitchTimerAction Action { get; set; }
        public TimeSpan Period { get; set; }
        public Timer Timer { get; set; }
    }
}