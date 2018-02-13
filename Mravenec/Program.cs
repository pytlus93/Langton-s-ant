using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mravenec
{
    class Program
    {
        enum Direction
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        static void Main(string[] args)
        {
            const int maxX = 128;
            int x = maxX / 2, y = maxX / 2;
            int fiboA = 1; int fiboB = 1;
            var image = new Bitmap(maxX, maxX);
            using (Graphics graph = Graphics.FromImage(image))
            {
                Rectangle ImageSize = new Rectangle(0, 0, maxX, maxX);
                graph.FillRectangle(Brushes.White, ImageSize);
            }
            var white = Color.White;
            var black = Color.Black;

            Direction direction = Direction.North;

            var dirName = $"{maxX}x{maxX}";
            System.IO.Directory.CreateDirectory(dirName);
            for (int i = 0; i < 1000000000; i++)
            {
                if (i == fiboB)
                {
                    var p = fiboA + fiboB;
                    fiboA = fiboB;
                    fiboB = p;

                    image.Save(dirName + "/Step_" + i + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
                    Console.WriteLine(i);
                }

                var actCol = image.GetPixel(x, y);
                image.SetPixel(x, y, actCol.ToArgb() == white.ToArgb() ? black : white);
                switch (direction)
                {
                    case Direction.North:
                        if (actCol.ToArgb() == white.ToArgb())
                        {
                            x--;
                            direction = Direction.West;
                        }
                        else
                        {
                            x++;
                            direction = Direction.East;
                        }
                        break;
                    case Direction.South:
                        if (actCol.ToArgb() == black.ToArgb())
                        {
                            x--;
                            direction = Direction.West;
                        }
                        else
                        {
                            x++;
                            direction = Direction.East;
                        }
                        break;
                    case Direction.West:
                        if (actCol.ToArgb() == white.ToArgb())
                        {
                            y--;
                            direction = Direction.South;
                        }
                        else
                        {
                            y++;
                            direction = Direction.North;
                        }
                        break;
                    case Direction.East:
                        if (actCol.ToArgb() == black.ToArgb())
                        {
                            y--;
                            direction = Direction.South;
                        }
                        else
                        {
                            y++;
                            direction = Direction.North;
                        }
                        break;
                }

                if (x >= maxX)
                    x = 0;
                else if (x < 0)
                    x = maxX - 1;

                if (y >= maxX)
                    y = 0;
                else if (y < 0)
                    y = maxX - 1;
            }
            image.Save(dirName + "/Step_finish.gif", System.Drawing.Imaging.ImageFormat.Gif);
        }
    }
}
