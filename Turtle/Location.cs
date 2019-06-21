using System;

namespace Turtle
{
	internal class Location : IEquatable<Location>
	{
		// Readonly auto-implemented properties.
		public int X { get; private set; }
		public int Y { get; private set; }

		// Set the properties in the constructor.
		public Location(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as Location);
		}

		public bool Equals(Location other)
		{
			// If parameter is null, return false.
			if (object.ReferenceEquals(other, null))
			{
				return false;
			}

			// Optimization for a common success case.
			if (object.ReferenceEquals(this, other))
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
			if (Object.ReferenceEquals(lhs, null))
			{
				if (Object.ReferenceEquals(rhs, null))
				{
					// null == null = true.
					return true;
				}

				// Only the left side is null.
				return false;
			}
			// Equals handles case of null on right side.
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Location lhs, Location rhs)
		{
			return !(lhs == rhs);
		}
	}
}
