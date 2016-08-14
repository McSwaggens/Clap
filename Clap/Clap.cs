using System;
using System.Collections.Generic;
using System.Threading;
using static Clap.Logger;
using System.IO;

namespace Clap
{
	public class Clap
	{
		
		public static Clap instance;
		public GraphicalInterface FocusedComponent;
		public InputManager inputManager;
		public GraphicalInterface CurrentlyUpdating = null;
		public List<GraphicalInterface> Components = new List<GraphicalInterface>();
		public bool Exiting = false;
		
		public List<Song> songs = new List<Song>();
		
		public string musicDirectory = $"/home/{Environment.UserName}/Music/";
		
		public SongViewer songViewer;
		
		public StatusBar statusBar;
		
		public PlayMode playMode = PlayMode.NONE;
		
		public ProgressBar songProgressBar;
		
		public Song currentSong = null;
		
		/*
		* Song name at top 
		* Controls second line from the bottom
		* Song progress bar at the bottom
		*/
		
		public Clap ()
		{
			
			Log ($"Launching Clap...");
			
			//Check if the music directory exists
			if (!Directory.Exists(musicDirectory))
			{
				//Music directory does not exist...
				LogWarning ("No music directory was found...\nAttempting to create new music directory...");
				try
				{
					// Try and create the music directory
					Directory.CreateDirectory (musicDirectory);
				}
				catch (IOException e)
				{
					//Failed to create new music directory
					LogError ($"Failed to create folder at \"{musicDirectory}\"!");
					return; // Exit the application
				}
			}
			
			instance = this;
			
			//Initialize the GUI
			//Console.Clear ();
			GUI.Initialize (this);
			
			//Load all of the songs in the music directory into a list
			FindSongs ();
			
			inputManager = new InputManager (this);
			
			//Make the cursor invisible
			Console.CursorVisible = false;
			
			Console.TreatControlCAsInput = true;
			
			songViewer = new SongViewer (this);
			songViewer.Format (new Location (0, 1), new Size (GUI.ScreenWidth, GUI.ScreenHeight-2));
			AddComponent (songViewer);
			
			songProgressBar = new ProgressBar (this);
			songProgressBar.Format (new Location (0, GUI.ScreenHeight), new Size (GUI.ScreenWidth, 1));
			songProgressBar.progress = 50;
			AddComponent (songProgressBar);
			
			statusBar = new StatusBar (this);
			statusBar.Format (new Location (0, 0), new Size (GUI.ScreenWidth, 1));
			AddComponent (statusBar);
			
			//Create a new thread to check if the terminal has been resized
			GUI.StartGUIEventListener();
			
			songViewer.Focus ();
			
			//Draw everything to the screen for the first time
			Refresh ();
			
			//Start listening for keyboard input
			inputManager.StartListener ();
		}
		
		public void FindSongs ()
		{
			songs.Clear();
			foreach (string file in Directory.GetFiles(musicDirectory))
			{
				Song song = new Song (file);
				if (song.format != SongFormat.NONE)
				{
					songs.Add (song);
				}
			}
		}
		
		public void Quit()
		{
			Console.ResetColor();
			Console.Clear();
			Console.CursorVisible = true;
			Exiting = true;
			GUI.StopGUIEventListener ();
			Thread.Sleep(10);
			Environment.Exit(0);
		}
		
		public void Quit(string message)
		{
			Console.ResetColor();
			Console.Clear();
			Console.CursorVisible = true;
			Exiting = true;
			GUI.StopGUIEventListener ();
			Thread.Sleep(10);
			Console.WriteLine(message);
			Environment.Exit(0);
		}
		
		public void PlaySong (Song song)
		{
			currentSong = song;
			Refresh ();
		}

		public void Refresh()
		{
			Console.ResetColor ();
			Console.Clear ();
			foreach (GraphicalInterface component in Components)
			{
				CurrentlyUpdating = component;
				component.Update();
			}
			CurrentlyUpdating = null;
		}

		public void RefreshComponent(GraphicalInterface component)
		{
			CurrentlyUpdating = component;
			component.Update ();
			CurrentlyUpdating = null;
		}

		public void AddComponent(GraphicalInterface component)
		{
			this.Components.Add (component);
			//Redraw the screen after the new component is added
			Refresh ();
		}

		public void RemoveComponent(GraphicalInterface component)
		{
			this.Components.Remove (component);
			//Redraw the screen after the component is removed
			Refresh ();
		}

		public void RemoveFocus(GraphicalInterface component)
		{
			FocusedComponent = Components [Components.Count-1];
			FocusedComponent.Focus ();
		}
		
		public void SwitchFocus(GraphicalInterface component)
		{
			FocusedComponent.focused = false;
			FocusedComponent.OnUnfocused();
			FocusedComponent = component;
			FocusedComponent.focused = true;
			FocusedComponent.OnFocused();
		}
		
		public void SizeChanged() {
			// editor.size.Width = GUI.ScreenWidth;
			// editor.size.Height = GUI.ScreenHeight-1;
			// commandPanel.size.Width = GUI.ScreenWidth;
			// commandPanel.Move(new Location (0, GUI.ScreenHeight));
			Refresh ();
		}
	}
}

