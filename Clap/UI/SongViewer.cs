using System;

namespace Clap
{
	public class SongViewer : GraphicalInterface, InputListener
	{
		Clap clap;
		public SongViewer (Clap clap) : base (clap)
		{
			this.clap = clap;
			this.name = "Song Viewer";
		}
		
		public override void Update ()
		{
			GUI.FillRectangle (new Location (0, 0), new Location (size.Width, size.Height), ConsoleColor.Red);
		}

        public void KeyPressed(ConsoleKeyInfo keyInfo)
        {
        }
    }
}

