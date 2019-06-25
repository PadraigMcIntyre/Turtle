using System;

namespace Turtle
{
	public class Location : IEquatable<Location>
	{
		// Readonly auto-implemented properties.
		public int X { get; set; }
		public int Y { get; set; }

		// Set the properties in the constructor.
		public Location(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Location);
		}

		public bool Equals(Location other)
		{
			// If parameter is null, return false.
			if (ReferenceEquals(other, null))
			{
				return false;
			}

			// Optimization for a common success case.
			if (ReferenceEquals(this, other))
			{
				return true;
			}

			// If run-time types are not exactly the same, return false.
			if (this.GetType() != other.GetType())
			{
				return false;
			}

			// Return true if the fields match.
			// Note that the base class is not invoked because it is
			// System.Object, which defines Equals as reference equality.
			return (X == other.X) && (Y == other.Y);
		}

		public override int GetHashCode()
		{
			return X * 0x00010000 + Y;
		}

		public static bool operator ==(Location lhs, Location rhs)
		{
			// Check for null on left side.
			return lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
			// null == null = true.

			// Only the left side is null.
			// Equals handles case of null on right side.
		}

		public static bool operator !=(Location lhs, Location rhs)
		{
			return !(lhs == rhs);
		}
	}
}
