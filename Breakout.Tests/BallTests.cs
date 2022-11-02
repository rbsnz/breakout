using Breakout.Game;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Numerics;

namespace Breakout.Tests
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void TestMove()
        {
            Ball ball = new Ball(Vector2.Zero);

            ball.Move(new Vector2(5, 10));
            Assert.AreEqual(new Vector2(5, 10), ball.Position, "The ball should move by the specified amount.");

            Assert.AreEqual(new Vector2(10, -5), ball.Position, "The ball should move relative to its current position.");
        }
    }
}
