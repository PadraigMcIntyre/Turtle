using System;
using System.Collections.Generic;
using System.IO;

namespace Turtle
{
	public class Settings
	{
		public string Filename { get; set; }
		public Position StartPosition { get; set; }
		public Location ExitLocation { get; set; }
		public List<Location> MineLocations { get; set; }

		public Settings(string filename)
		{
			Filename = filename;
			StartPosition = new Position(-1, -1, Direction.North);
			ExitLocation = new Location(-1, -1);
			MineLocations = new List<Location>();
		}

		public void ParseFile()
		{
			if (!File.Exists(Filename))
				throw new Exception($"Game settings file ({Filename}) not found");

			// Read a text file line by line.
			var lines = File.ReadAllLines(Filename);

			foreach (var line in lines)
			{
				var parts = line.Split(new char[] { ' ', ',', '(', ')' });
				switch (parts[0])
				{
					case "Size:":
						StartPosition.MaxX = int.Parse(parts[3]);
						StartPosition.MaxY = int.Parse(parts[7]);
						break;
					case "Starting":
						StartPosition.X = int.Parse(parts[4]);
						StartPosition.Y = int.Parse(parts[8]);
						StartPosition.Direction = StartPosition.GetDirectionFromText(parts[12]);
						break;
					case "Exit":
					{
						var x = int.Parse(parts[4]);
						var y = int.Parse(parts[8]);

						ExitLocation = new Location(x, y);
						break;
					}
					case "Mines:":
					{
						var xLocation = 2;
						var yLocation = 4;
						while (yLocation < parts.Length)
						{
							var x = int.Parse(parts[xLocation]);
							var y = int.Parse(parts[yLocation]);

							MineLocations.Add(new Location(x, y));
							xLocation += 6;
							yLocation += 6;
						}

						break;
					}
					default:
						throw new Exception("Invalid input arguments.");
				}
			}

		}
	}
}
