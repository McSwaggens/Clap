using System;
using static System.Console;

using SDL2;
using static SDL2.SDL_mixer;

namespace Clap
{
	public class Audio
	{
		IntPtr song;
		uint startTime;
		
		public uint CurrentTime
		{
			get
			{
				return (SDL_GetTicks() - startTime) / 1000;
			}
		}
		
		public Audio ()
		{
			if (SDL_mixer.Mix_Init (SDL_mixer.MIX_InitFlags.MIX_INIT_MP3) != 0)
			{
				WriteLine ("(ERROR) " + Error);
				return;
			}
			
			if (SDL_mixer.Mix_OpenAudio (44100, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048) != 0)
			{
				WriteLine ("(ERROR) " + Error);
				return;
			}
		}
		
		public void Play (string file)
		{
			//@"/home/daniel/Music/Billy Joel - Piano Man.mp3"
			song = Mix_LoadMUS (file);
			
			if (Mix_PlayMusic(song, 1) == 0)
			{
				startTime = SDL_GetTicks();

				WriteLine ("Playing Music->" + Mix_PlayingMusic());

				while (Mix_PlayingMusic() == 1)
				{
					SDL.SDL_Delay (1000);
					WriteLine ($"Time: CurrentTime");
				}
			}
			else
			{
				WriteLine ("(ERROR) " + Error);
			}
			
			Mix_FreeMusic (song);
		}
		
		public void SetVolume (int volume)
		{
			Mix_VolumeMusic (100);
		}
		
		public void Dispose ()
		{
			Mix_CloseAudio ();
		}
	}
}

