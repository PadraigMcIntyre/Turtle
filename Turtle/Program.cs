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
			try
			{
				// Read from game-settings file
				var settings = new Settings(@"..\..\game-settings.txt");
				settings.ParseFile();

				// Read from moves file
				var moves = new Moves(@"..\..\moves.txt", settings);
				moves.ParseFile();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				//throw;
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
