using System;
using System.Collections.Generic;
using System.Threading;

namespace Clap
{
	public delegate void InputEvent(ConsoleKeyInfo keyInfo);
	
	public class InputManager
	{
		public static event InputEvent OnKeyPressed;
		
		public List<Shortcut> shortcuts = new List<Shortcut>()
		{
			// new Shortcut
			// (
			// 	() =>
			// 	{
			// 		Command.Execute("write;");
			// 	},
			// 	ConsoleKey.O, true
			// ),
			// new Shortcut
			// (
			// 	() =>
			// 	{
			// 		clap.instance.commandPanel.mode = CommandPanelMode.COMMAND;
			// 		clap.instance.SwitchFocus(clap.instance.commandPanel);
			// 	},
			// 	ConsoleKey.E, true
			// )
		};
		
		private Clap clap;
		private Thread listenerThread;
		public bool CancelKeySpread = false;
		public InputManager (Clap clap)
		{
			this.clap = clap;
		}
		
		public void StartListener()
		{
			while (true) // TODO: Exit listener
			{
				ConsoleKeyInfo keyInfo = Console.ReadKey();
				
				if (OnKeyPressed != null)
					OnKeyPressed(keyInfo);
				
				
				foreach (Shortcut shortcut in shortcuts)
				{
					if (shortcut.Match(keyInfo))
					{
						shortcut.Fire();
						continue;
					}
				}
				
				if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && keyInfo.Key == ConsoleKey.C)
				{
					Console.ResetColor();
					Console.Clear();
					Console.CursorVisible = true;
					clap.Exiting = true;
					GUI.StopGUIEventListener ();
					Thread.Sleep(10);
					Console.WriteLine("CTRL+C Pressed... Exiting.");
					Environment.Exit(0);
				}
				else
				{
					if (clap.FocusedComponent != null && clap.FocusedComponent is InputListener)
						((InputListener)clap.FocusedComponent).KeyPressed (keyInfo);
				}
			}
		}
	}
}

