using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuxiTags
{
    internal class ConsoleExt
    {
       internal static ConsoleColor GetNearestConsoleColor(string hex)
        {
            // Parse the hex color code
            int r = Convert.ToInt32(hex.Substring(1, 2), 16);
            int g = Convert.ToInt32(hex.Substring(3, 2), 16);
            int b = Convert.ToInt32(hex.Substring(5, 2), 16);

            // Create a mapping of ConsoleColors to their approximate RGB values
            ConsoleColor[] consoleColors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
            ConsoleColor nearestColor = consoleColors[0];
            double nearestDistance = double.MaxValue;

            foreach (var color in consoleColors)
            {
                // Get the RGB value of the console color
                var consoleRgb = GetRgbFromConsoleColor(color);
                double distance = GetColorDistance(r, g, b, consoleRgb.r, consoleRgb.g, consoleRgb.b);

                // Find the nearest color
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestColor = color;
                }
            }

            return nearestColor;
        }

        static (int r, int g, int b) GetRgbFromConsoleColor(ConsoleColor color)
        {
            // Approximate RGB values for each ConsoleColor
            return color switch
            {
                ConsoleColor.Black => (0, 0, 0),
                ConsoleColor.DarkBlue => (0, 0, 128),
                ConsoleColor.DarkGreen => (0, 128, 0),
                ConsoleColor.DarkCyan => (0, 128, 128),
                ConsoleColor.DarkRed => (128, 0, 0),
                ConsoleColor.DarkMagenta => (128, 0, 128),
                ConsoleColor.DarkYellow => (128, 128, 0),
                ConsoleColor.Gray => (192, 192, 192),
                ConsoleColor.DarkGray => (128, 128, 128),
                ConsoleColor.Blue => (0, 0, 255),
                ConsoleColor.Green => (0, 255, 0),
                ConsoleColor.Cyan => (0, 255, 255),
                ConsoleColor.Red => (255, 0, 0),
                ConsoleColor.Magenta => (255, 0, 255),
                ConsoleColor.Yellow => (255, 255, 0),
                ConsoleColor.White => (255, 255, 255),
                _ => (255, 255, 255) // Default to white if not found
            };
        }

        static double GetColorDistance(int r1, int g1, int b1, int r2, int g2, int b2)
        {
            return Math.Sqrt(Math.Pow(r1 - r2, 2) + Math.Pow(g1 - g2, 2) + Math.Pow(b1 - b2, 2));
        }
    }
}
