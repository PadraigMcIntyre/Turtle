using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Turtle
{
	class Program
	{
		static void Main(string[] args)
		{
			var location1 = new Location(4, 3);
			var location2 = new Location(4, 5);

			var locations = new List<Location>
			{
				new Location(3, 3),
				new Location(3, 4),
				new Location(4, 3)
			};

			if (locations.Contains(location1))
			{
				Console.WriteLine("Location1 in list");
			}

			if (locations.Contains(location2))
			{
				Console.WriteLine("Location2 in list");
			}

			var startPosition = new Position(0, 0, Direction.East);

			var exitLocation = new Location(-1, -1);
			var bombLocations = new List<Location>();

			// Read from game-settings file
			const string settingsFile = @"..\..\game-settings.txt";
			
			if (!File.Exists(settingsFile))
				throw new Exception("Game settings file (game-settings.txt) not found");

			// Read a text file line by line.
			var lines = File.ReadAllLines(settingsFile);

			foreach (var line in lines)
			{
				//Console.WriteLine(line);
				var parts = line.Split(new char[] { ' ', ',', '(', ')' });
				switch (parts[0])
				{
					case "Size:":
						startPosition.MaxX = int.Parse(parts[3]);
						startPosition.MaxY = int.Parse(parts[7]);
						break;
					case "Starting":
						startPosition.X = int.Parse(parts[4]);
						startPosition.Y = int.Parse(parts[8]);
						startPosition.Direction = startPosition.GetDirectionFromText(parts[12]);
						break;
					case "Exit":
					{
						var x = int.Parse(parts[4]);
						var y = int.Parse(parts[8]);

						exitLocation = new Location(x, y);
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

							bombLocations.Add(new Location(x, y));
							xLocation += 6;
							yLocation += 6;
						}

						break;
					}
					default:
						throw new Exception("Invalid input arguments.");
				}
			}

			// Read from moves file
			const string movesFile = @"..\..\moves.txt";

			if (!File.Exists(movesFile))
				throw new Exception("Moves file (moves.txt) not found");

			var finished = false;
			//var startPosition = new Position(position.X, position.Y, position.Direction);

			// Read a text file line by line.
			var moveLines = File.ReadAllLines(movesFile);

			foreach (var moveLine in moveLines)
			{
				var parts = moveLine.Split(new char[] {' '});
				var position = new Position(startPosition);

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
						if (position.ToLocation() == exitLocation)
						{
							Console.WriteLine($"Sequence {parts[0]} Success!");
							finished = true;
							break;
						}

						// Check if we have hit a bomb.
						if (!bombLocations.Contains(position.ToLocation()))
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

	public enum Direction
	{
		North,
		East,
		South,
		West
	}
}
