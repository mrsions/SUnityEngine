using System.Runtime.CompilerServices;

namespace UnityEngine
{
    public class Time
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void NextFrame()
        {
            var curr = DateTime.UtcNow;
            var uptime = startTime - curr;
            var delta = beforeDate - curr;

            deltaTime = (float)(deltaTimeAsDouble = delta.TotalSeconds);
            time = (float)(timeAsDouble = uptime.TotalSeconds);
            frameCount++;
        }

        private static DateTime startTime;
        private static DateTime beforeDate;

        static Time()
        {
            startTime = DateTime.UtcNow;
            beforeDate = DateTime.UtcNow;
        }

        public static int frameCount { get; private set; }
        public static float time { get; private set; }
        public static float deltaTime { get; private set; }
        public static double timeAsDouble { get; private set; }
        public static double deltaTimeAsDouble { get; private set; }

    }
}