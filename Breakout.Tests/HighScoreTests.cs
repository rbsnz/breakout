using System;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Breakout.Data;

namespace Breakout.Tests
{
    [TestClass]
    public class HighScoreTests
    {
        private readonly DateTime OriginDate;

        public HighScores HighScores { get; set; }

        public HighScoreTests()
        {
            OriginDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a high score list with a maximum count of 3,
        /// and high score values of 100, 200, and 300.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            HighScores = new HighScores(Path.GetTempFileName(), 3);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            File.Delete(HighScores.FilePath);
        }

        private void PopulateScores()
        {
            // Adding the first 3 high scores should succeed.
            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate, 300, "a")));
            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate, 200, "b")));
            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate, 100, "c")));
        }

        [TestMethod]
        public void TestIsHighScore()
        {
            PopulateScores();

            Assert.IsTrue(HighScores.IsHighScore(400));
            Assert.IsTrue(HighScores.IsHighScore(300));
            Assert.IsTrue(HighScores.IsHighScore(200));
            Assert.IsFalse(HighScores.IsHighScore(100));
            Assert.IsFalse(HighScores.IsHighScore(50));
        }

        [TestMethod]
        public void TestAddHighScore()
        {
            PopulateScores();

            // The high score list should be full.
            Assert.AreEqual(HighScores.MaxScores, HighScores.Count);
            // The score should be added as it qualifies as a high score.
            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate, 500, "d")));
            // The the size of the high score list should remain the same.
            Assert.AreEqual(HighScores.MaxScores, HighScores.Count);
            // The lowest score should be removed.
            Assert.IsFalse(HighScores.Any(x => x.Score == 100));
        }

        /// <summary>
        /// Asserts that a score is not added when the high score list is full
        /// and the score is smaller than the lowest score in the list.
        /// </summary>
        [TestMethod]
        public void TestAddLowScore()
        {
            PopulateScores();

            // Attempting to add a score less than the lowest score
            // when the high score list is full should not succeed.
            Assert.IsFalse(HighScores.Add(new HighScore(50, "d")));
        }

        /// <summary>
        /// Asserts that a score is not added when the high score list is full
        /// and the score is equal to the lowest score in the list.
        /// </summary>
        [TestMethod]
        public void TestAddSameScore_Low()
        {
            PopulateScores();

            // Attempting to add a score equal to the lowest score
            // when the high score list is full should not succeed.
            Assert.IsFalse(HighScores.Add(new HighScore(100, "d")));

            // The list should not be changed.
            Assert.AreEqual(HighScores.MaxScores, HighScores.Count);
            Assert.AreEqual("a", HighScores[0].Name);
            Assert.AreEqual("b", HighScores[1].Name);
            Assert.AreEqual("c", HighScores[2].Name);
        }

        [TestMethod]
        public void TestAddSameScore_Mid()
        {
            PopulateScores();

            // Adding a score with the same value as B should succeed.
            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate.AddMinutes(5), 200, "d")));
            // The list length should not be changed.
            Assert.AreEqual(HighScores.MaxScores, HighScores.Count);
            // A should remain the highest score in the list.
            Assert.AreEqual("a", HighScores[0].Name);
            // B should not change position as it was added at an earlier date.
            Assert.AreEqual("b", HighScores[1].Name);
            // D should be added to the last position in the list, replacing C.
            Assert.AreEqual("d", HighScores[2].Name);
        }

        [TestMethod]
        public void TestAddSameScore_High()
        {
            PopulateScores();

            // Adding a score with the same value as A should succeed.
            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate.AddMinutes(5), 300, "d")));
            // The list length should not be changed.
            Assert.AreEqual(HighScores.MaxScores, HighScores.Count);
            // A should remain the highest score in the list.
            Assert.AreEqual("a", HighScores[0].Name);
            // D should be added after A.
            Assert.AreEqual("d", HighScores[1].Name);
            // B should be moved to the last position, pushing C off the list.
            Assert.AreEqual("b", HighScores[2].Name);
        }

        [TestMethod]
        public void TestComparison_DifferentScore()
        {
            DateTime dt = DateTime.Now;

            HighScore
                low = new HighScore(dt, 50, "x"),
                high = new HighScore(dt, 1000, "x");

            // High scores should take precedence over low scores.
            Assert.AreEqual(-1, high.CompareTo(low));
        }

        [TestMethod]
        public void TestComparison_DifferentDate()
        {
            HighScore
                past = new HighScore(DateTime.Now.AddMinutes(-10), 100, "x"),
                future = new HighScore(DateTime.Now.AddMinutes(10), 100, "x");

            // Past dates should take precedence over future dates.
            Assert.AreEqual(-1, past.CompareTo(future));
        }

        [TestMethod]
        public void TestComparison_DifferentTimezone()
        {
            DateTime dt = DateTime.Now;
            DateTime dtUtc = dt.ToUniversalTime();

            HighScore
                now = new HighScore(dt, 100, "x"),
                utcNow = new HighScore(dtUtc, 100, "x");

            // DateTime should be converted to universal time when compared,
            // so these scores should be equal.
            Assert.AreEqual(0, now.CompareTo(utcNow));
        }
    }
}
