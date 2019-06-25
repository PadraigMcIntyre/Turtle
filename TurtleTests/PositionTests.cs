using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turtle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turtle.Tests
{
	[TestClass()]
	public class PositionTests
	{
		[TestMethod()]
		public void MoveTest_When_FacingNorth()
		{
			var initialPosition = new Position(1, 1, Direction.North) { MaxX = 5, MaxY = 10 };
			var expectedPosition = new Position(1, 0, Direction.North) { MaxX = 5, MaxY = 10 };

			initialPosition.Move();

			Assert.AreEqual(expectedPosition, initialPosition);
		}

		[TestMethod()]
		public void MoveTest_When_FacingEast()
		{
			var initialPosition = new Position(1, 1, Direction.East) { MaxX = 5, MaxY = 10 };
			var expectedPosition = new Position(2, 1, Direction.East) { MaxX = 5, MaxY = 10 };

			initialPosition.Move();

			Assert.AreEqual(expectedPosition, initialPosition);
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidOperationException))]
		public void MoveTest_When_MovingNorthPastTheEdge()
		{
			var initialPosition = new Position(0, 0, Direction.North) { MaxX = 5, MaxY = 10 };
			var expectedPosition = new Position(0, -1, Direction.North) { MaxX = 5, MaxY = 10 };

			initialPosition.Move();
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidOperationException))]
		public void MoveTest_When_MovingWestPastTheEdge()
		{
			var initialPosition = new Position(0, 0, Direction.West) { MaxX = 5, MaxY = 10 };
			var expectedPosition = new Position(0, -1, Direction.West) { MaxX = 5, MaxY = 10 };

			initialPosition.Move();
		}

		[TestMethod()]
		public void TurnTest_When_FacingEast()
		{
			var initialPosition = new Position(1, 1, Direction.East) { MaxX = 5, MaxY = 10 };
			var expectedPosition = new Position(1, 1, Direction.South) { MaxX = 5, MaxY = 10 };

			initialPosition.Turn();

			Assert.AreEqual(expectedPosition.Direction, initialPosition.Direction);
		}

		[TestMethod()]
		public void TurnTest_When_FacingSouth()
		{
			var initialPosition = new Position(1, 1, Direction.South) { MaxX = 5, MaxY = 10 };
			var expectedPosition = new Position(1, 1, Direction.West) { MaxX = 5, MaxY = 10 };

			initialPosition.Turn();

			Assert.AreEqual(expectedPosition.Direction, initialPosition.Direction);
		}
	}
}