using System;
using System.Threading;

namespace Clap
{
	public class GUI
	{
		private static Clap clap;
		private static readonly bool ENABLED = true; //TURN TO FALSE FOR LOGGER.cs OUTPUT

		public static void Initialize(Clap clap)
		{
			GUI.clap = clap;
		}
		
		private static int LastScreenWidth = Console.BufferWidth, LastScreenHeight = Console.BufferHeight-1;

		public static int ScreenWidth
		{
			get
			{
				return Console.BufferWidth;
			}
		}

		private static Thread guiListenerThread;

		public static void StopGUIEventListener() {
			if (guiListenerThread != null && guiListenerThread.IsAlive)
				guiListenerThread.Abort ();
		}

		public static void StartGUIEventListener() {
			if (guiListenerThread == null || !guiListenerThread.IsAlive) {
				guiListenerThread = new Thread (() => {
					while (true) {
						if (HasSizeChanged ())
							clap.SizeChanged ();
						Thread.Sleep (20);
					}
				});
				guiListenerThread.Start ();
			}
		}

		public static bool HasSizeChanged() {
			if (LastScreenWidth != Console.BufferWidth || LastScreenHeight != Console.BufferHeight-1) {
				LastScreenWidth = Console.BufferWidth;
				LastScreenHeight = Console.BufferHeight-1;
				return true;
			}
			return false;
		}

		public static int ScreenHeight
		{
			get
			{
				return Console.BufferHeight-1;
			}
		}

		public static void DrawString(string str, Location location)
		{
			if (!ENABLED)
				return;
			location.X += clap.CurrentlyUpdating.location.X;
			location.Y += clap.CurrentlyUpdating.location.Y;

			Console.SetCursorPosition (location.X, location.Y);
			Console.Write (str);
		}

		public static void ClearLine(int line)
		{
			line += clap.CurrentlyUpdating.location.Y;
			string wiper = Util.StringSpacer (clap.CurrentlyUpdating.size.Width);
			DrawString(wiper, new Location(0, line));
		}

		public static void DrawString(string str, Location location, ConsoleColor ForeColor, ConsoleColor BackColor)
		{
			location.X += clap.CurrentlyUpdating.location.X;
			location.Y += clap.CurrentlyUpdating.location.Y;

			Console.ForegroundColor = ForeColor;
			Console.BackgroundColor = BackColor;

			Console.SetCursorPosition (location.X, location.Y);
			Console.Write (str);
			Console.ResetColor();
		}
		
		public static void DrawString(string str, Location location, ConsoleColor ForeColor)
		{
			location.X += clap.CurrentlyUpdating.location.X;
			location.Y += clap.CurrentlyUpdating.location.Y;

			Console.ForegroundColor = ForeColor;

			Console.SetCursorPosition (location.X, location.Y);
			Console.Write (str);
			Console.ResetColor();
		}

		public static void SetCursorPos(Location loc)
		{
			//LAZY CODE
			//TODO FIX LAZY CODE

			Location location = new Location (loc.X, loc.Y);

			location.X += clap.CurrentlyUpdating.location.X;
			location.Y += clap.CurrentlyUpdating.location.Y;
			Console.SetCursorPosition (location.X, location.Y);
		}

		public static void FillRectangle(Location start, Location end, ConsoleColor color)
		{
			if (!ENABLED)
				return;
			start.X += clap.CurrentlyUpdating.location.X;
			start.Y += clap.CurrentlyUpdating.location.Y;

			end.X += clap.CurrentlyUpdating.location.X;
			end.Y += clap.CurrentlyUpdating.location.Y;

			Console.BackgroundColor = color;

			int x = start.X;
			string Spacer = Util.StringSpacer(end.X);

			for (int y = start.Y; y < end.Y; y++)
			{
				Console.SetCursorPosition (x, y);
				Console.Write (Spacer);
			}

			Console.ResetColor ();
		}
	}
}

