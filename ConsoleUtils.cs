using System;

namespace ZuxiTags
{
    internal class ConsoleUtils
    {

        static int CurrentColor = 1;

        public static ConsoleColor GetNewConsoleColor()
        {
            CurrentColor++;

            if (CurrentColor == 15)
            {
                CurrentColor = 1;
            }

            if (CurrentColor == 12)
            {
                CurrentColor = 13;
            }

            return (ConsoleColor)CurrentColor;
        }
    }
}
