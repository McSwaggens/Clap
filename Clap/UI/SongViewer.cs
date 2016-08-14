using System;

namespace Clap
{
	public class SongViewer : GraphicalInterface, InputListener
	{
		Clap clap;
		
		int highLighted;
		
		public SongViewer (Clap clap) : base (clap)
		{
			this.clap = clap;
			this.name = "Song Viewer";
		}
		
		public override void Update ()
		{
			GUI.FillRectangle (new Location (0, 0), new Location (size.Width, size.Height), ConsoleColor.Black);
			
			for (int i = 0; i < clap.songs.Count; i++)
			{
				Song song = clap.songs[i];
				
				ConsoleColor fillColor = highLighted == i ? ConsoleColor.Red : ConsoleColor.Black;
				ConsoleColor textColor = highLighted == i ? ConsoleColor.Black : ConsoleColor.Red;
				
				GUI.FillRectangle (new Location (0, i), new Location (size.Width, i+1), fillColor);
				GUI.DrawString ($"{i}", new Location (0, i), textColor, fillColor);
				GUI.DrawString ($"{song.name}", new Location (5, i), textColor, fillColor);
			}
		}

        public void KeyPressed(ConsoleKeyInfo keyInfo)
        {
			if (keyInfo.Key == ConsoleKey.UpArrow)
			{
				if (highLighted != 0)
				{
					highLighted--;
					SelfUpdate ();
				}
			}
			else
			if (keyInfo.Key == ConsoleKey.DownArrow)
			{
				if (highLighted != clap.songs.Count-1)
				{
					highLighted++;
					SelfUpdate ();
				}
			}
        }
    }
}

