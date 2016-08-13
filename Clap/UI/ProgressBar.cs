using System;
using System.Threading;

namespace Clap
{
	public class ProgressBar : GraphicalInterface, InputListener
	{
		Clap clap;
		
		public int progress = 0;
		
		public ProgressBar (Clap clap) : base (clap)
		{
			this.clap = clap;
			this.name = "Song Viewer";
			
			new Thread (() => 
			{
				for (int i = 0; i <= 100; i++)
				{
					Thread.Sleep (200);
					progress = i;
					SelfUpdate();
				}
			}).Start ();
		}
		
		public override void Update ()
		{
			GUI.FillRectangle (new Location (0, 0), new Location (size.Width, size.Height), ConsoleColor.White);
			
			if (progress != 0)
			{
				GUI.FillRectangle (new Location (0, 0), new Location ((progress * size.Width) / 100, size.Height), ConsoleColor.Blue);
			}
		}

        public void KeyPressed(ConsoleKeyInfo keyInfo)
        {
        }
    }
}

