using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Turtle
{
	public class Moves
	{
		public string Filename { get; set; }
		public Settings Settings { get; set; }

		public Moves(string filename, Settings settings)
		{
			Filename = filename;
			Settings = settings;
		}

		public void ParseFile()
		{
			if (!File.Exists(Filename))
				throw new Exception($"Moves file ({Filename}) not found");

			var finished = false;

			// Read a text file line by line.
			var moveLines = File.ReadAllLines(Filename);

			foreach (var moveLine in moveLines)
			{
				var parts = moveLine.Split(new char[] { ' ' });
				var position = new Position(Settings.StartPosition);

				foreach (var c in parts[1].ToLower())
				{
					try
					{
						switch (c)
						{
							case 'm':
								position.Move();
								break;
							case 'r':
								position.Turn();
								break;
							default:
								throw new Exception($"Unknown move {c}.");
						}

						// Check if we have reached the exit.
						if (position.ToLocation() == Settings.ExitLocation)
						{
							Console.WriteLine($"Sequence {parts[0]} Success!");
							finished = true;
							break;
						}

						// Check if we have hit a mine.
						if (!Settings.MineLocations.Contains(position.ToLocation()))
							continue;

						Console.WriteLine($"Sequence {parts[0]} Mine hit!");
						finished = true;
						break;
					}
					catch (Exception e)
					{
						Console.WriteLine($"Sequence {parts[0]} {e.Message}");
						finished = true;
						break;
					}
				}

				if (finished)
				{
					finished = false;
					continue;
				}

				Console.WriteLine($"Sequence {parts[0]} Still in danger!");
			}
		}
	}
}
