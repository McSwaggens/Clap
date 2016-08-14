using System;

namespace Clap
{
	public class StatusBar : GraphicalInterface
	{
		public StatusBar (Clap clap) : base (clap)
		{
			this.clap = clap;
			this.name = "Status Bar";
		}
		
		public override void Update ()
		{
			GUI.FillRectangle (new Location (0, 0), new Location (size.Width, 0), ConsoleColor.Black);
			string title = clap.currentSong == null ? "-No song playing-" : $"-{clap.currentSong.name}-";
			GUI.DrawString (title, new Location ((size.Width / 2) - ((title.Length / 2)), 0), ConsoleColor.Red, ConsoleColor.Black);
		}
    }
}

