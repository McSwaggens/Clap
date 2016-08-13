using System;
using System.IO;

namespace Clap
{
	public class Song
	{
		public string filePath;
		public string name;
		public SongFormat format;
		
		public Song (string filePath)
		{
			this.filePath = filePath;
			format = GetFileFormat();
		}
		
		private SongFormat GetFileFormat ()
		{
			foreach (SongFormat format in Enum.GetValues(typeof(SongFormat)))
			{
				if (format.ToString().ToLower() == Path.GetExtension(filePath).Split('.')[1])
				{
					return format;
				}
			}
			return SongFormat.NONE;
		}
	}
}

