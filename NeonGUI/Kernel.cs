using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;
using Canvas = Cosmos.System.Graphics.Canvas;
using Cosmos.System.Graphics.Fonts;
using Cosmos.System.Graphics.Extensions;

namespace NeonGUI
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Console.WriteLine("Neon GUI");
        }

        protected override void Run()
        {
            Canvas canvas;
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            PCScreenFont screenFont = PCScreenFont.Default;

            Pen pen = new Pen(Color.White);

            canvas.Clear(Color.Blue);
            canvas.DrawString("\nHello and welcome to Neon GUI Demo. Press any key to exit", screenFont, pen, 1, 1);
            canvas.DrawString("\nTODO: Make some graphics", screenFont, pen, 1, 50);

            Console.ReadKey();
            canvas.Disable();
            Sys.Power.Reboot();
        }
    }
}
