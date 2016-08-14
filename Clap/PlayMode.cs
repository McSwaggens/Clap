using System;

namespace Clap
{
	public enum PlayMode
	{
		NONE, 	// Do nothing after finishing a song
		REPEAT, // Play the song again when done
		LOOP, 	// Loop through the current playlist
		RANDOM	// Play a random song
	}
}

