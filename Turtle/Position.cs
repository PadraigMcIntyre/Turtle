using System;

namespace Turtle
{
	public class Position : Location
	{
		public Direction Direction { get; set; }
		public int MaxX { get; set; }
		public int MaxY { get; set; }

		public Position(int x, int y, Direction direction) : base(x, y)
		{
			Direction = direction;
		}

		public Position(Position position) : base(position.X, position.Y)
		{
			Direction = position.Direction;
			MaxX = position.MaxX;
			MaxY = position.MaxY;
		}

		public void Move()
		{
			switch (Direction)
			{
				case Direction.North:
					Y--;
					break;
				case Direction.East:
					X++;
					break;
				case Direction.South:
					Y++;
					break;
				case Direction.West:
					X--;
					break;
				default:
					throw new Exception($"Invalid Direction {Direction}");
			}

			ConfirmPositionIsValid();
		}

		private void ConfirmPositionIsValid()
		{
			if (X < 0 || Y < 0 || X >= MaxX || Y >= MaxY)
				throw new InvalidOperationException($"Location ({X}, {Y}) is not valid.");
		}

		public void Turn()
		{
			switch (Direction)
			{
				case Direction.North:
					Direction=Direction.East;
					break;
				case Direction.East:
					Direction = Direction.South;
					break;
				case Direction.South:
					Direction = Direction.West;
					break;
				case Direction.West:
					Direction = Direction.North;
					break;
				default:
					throw new Exception($"Invalid Direction {Direction}");
			}
		}

		internal Direction GetDirectionFromText(string facing)
		{
			switch (facing)
			{
				case "North":
					return Direction.North;
				case "East":
					return Direction.East;
				case "South":
					return Direction.South;
				case "West":
					return Direction.West;
				default:
					throw new Exception("Invalid Facing Direction");
			}
		}

		internal Location ToLocation()
		{
			return new Location(X, Y);
		}
	}
}
