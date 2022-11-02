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
            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate, 300, "A")));
            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate, 200, "B")));
            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate, 100, "C")));
        }

        [TestMethod]
        public void TestIsHighScore()
        {
            Assert.IsFalse(HighScores.IsHighScore(0), "0 should not qualify as a high score, even if the score list is not full.");

            PopulateScores();

            Assert.IsFalse(HighScores.IsHighScore(50), "A score smaller than the lowest score should not qualify as a high score.");
            Assert.IsFalse(HighScores.IsHighScore(100), "A score the same as the lowest score should not qualify as a high score.");
            Assert.IsTrue(HighScores.IsHighScore(101), "Any number larger than the lowest score should qualify as a high score.");
            Assert.IsTrue(HighScores.IsHighScore(300), "Any number larger than the lowest score should qualify as a high score.");
            Assert.IsTrue(HighScores.IsHighScore(200), "Any number larger than the lowest score should qualify as a high score.");
            Assert.IsTrue(HighScores.IsHighScore(400), "Any number larger than the lowest score should qualify as a high score.");
        }

        [TestMethod]
        public void TestAddHighScore()
        {
            PopulateScores();

            Assert.AreEqual(HighScores.MaxScores, HighScores.Count, "The high score list should be full.");
            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate, 500, "d")), "The score should be added as it qualifies as a high score.");
            Assert.AreEqual(HighScores.MaxScores, HighScores.Count, "The the size of the high score list should remain the same.");
            Assert.IsFalse(HighScores.Any(x => x.Value == 100), "The lowest score should be removed.");
        }

        /// <summary>
        /// Asserts that a score is not added when the high score list is full
        /// and the score is smaller than the lowest score in the list.
        /// </summary>
        [TestMethod]
        public void TestAddLowScore()
        {
            PopulateScores();

            Assert.IsFalse(HighScores.Add(new HighScore(50, "d")), "A score smaller than the lowest should not be added as a high score.");
        }

        /// <summary>
        /// Asserts that a score is not added when the high score list is full
        /// and the score is equal to the lowest score in the list.
        /// </summary>
        [TestMethod]
        public void TestAddSameScore_Low()
        {
            PopulateScores();

            Assert.IsFalse(HighScores.Add(new HighScore(100, "d")), "A score equal to the lowest should not be added as a high score.");

            Assert.AreEqual(HighScores.MaxScores, HighScores.Count, "The list should not be changed.");
            Assert.AreEqual("A", HighScores[0].Name, "The list should not be changed.");
            Assert.AreEqual("B", HighScores[1].Name, "The list should not be changed.");
            Assert.AreEqual("C", HighScores[2].Name, "The list should not be changed.");
        }

        [TestMethod]
        public void TestAddSameScore_Mid()
        {
            PopulateScores();

            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate.AddMinutes(5), 200, "D")), "Adding a score with the same value as B should succeed.");
            Assert.AreEqual(HighScores.MaxScores, HighScores.Count, "The list length should not be changed.");
            Assert.AreEqual("A", HighScores[0].Name, "A should remain the highest score in the list.");
            Assert.AreEqual("B", HighScores[1].Name, "B should not change position since it was added at an earlier date.");
            Assert.AreEqual("D", HighScores[2].Name, "D should be added to the last position in the list, replacing C.");
            Assert.IsFalse(HighScores.Any(x => x.Name.Equals("C")), "C should no longer exist in the high score list.");
        }

        [TestMethod]
        public void TestAddSameScore_High()
        {
            PopulateScores();

            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate.AddMinutes(5), 300, "D")), "Adding a score with the same value as A should succeed.");
            Assert.AreEqual(HighScores.MaxScores, HighScores.Count, "The list length should not be changed.");
            Assert.AreEqual("A", HighScores[0].Name, "A should remain the highest score in the list since it was added at an earlier date.");
            Assert.AreEqual("D", HighScores[1].Name, "D should be inserted after A.");
            Assert.AreEqual("B", HighScores[2].Name, "B should be moved to the last position, pushing C off the list.");
            Assert.IsFalse(HighScores.Any(x => x.Name.Equals("C")), "C should no longer exist in the high score list.");
        }

        [TestMethod]
        public void TestAddSameScore_Highest()
        {
            PopulateScores();

            Assert.IsTrue(HighScores.Add(new HighScore(OriginDate.AddMinutes(5), 400, "D")), "Adding a new highest score should succeed.");
            Assert.AreEqual(HighScores.MaxScores, HighScores.Count, "The list length should not be changed.");
            Assert.AreEqual("D", HighScores[0].Name, "D should be the new highest score in the list.");
            Assert.AreEqual("A", HighScores[1].Name, "A should be pushed down to position 2.");
            Assert.AreEqual("B", HighScores[2].Name, "B should be pushed down to the last position.");
            Assert.IsFalse(HighScores.Any(x => x.Name.Equals("C")), "C should no longer exist in the high score list.");
        }

        [TestMethod]
        public void TestComparison_DifferentScore()
        {
            DateTime dt = DateTime.Now;

            HighScore
                low = new HighScore(dt, 50, "x"),
                high = new HighScore(dt, 1000, "x");

            Assert.AreEqual(-1, high.CompareTo(low), "High scores should take precedence over low scores.");
        }

        [TestMethod]
        public void TestComparison_DifferentDate()
        {
            HighScore
                past = new HighScore(DateTime.Now.AddMinutes(-10), 100, "x"),
                future = new HighScore(DateTime.Now.AddMinutes(10), 100, "x");

            Assert.AreEqual(-1, past.CompareTo(future), "Past dates should take precedence over future dates.");
        }

        [TestMethod]
        public void TestComparison_DifferentTimezone()
        {
            DateTime dt = DateTime.Now;
            DateTime dtUtc = dt.ToUniversalTime();

            HighScore
                now = new HighScore(dt, 100, "x"),
                utcNow = new HighScore(dtUtc, 100, "x");

            Assert.AreEqual(0, now.CompareTo(utcNow), "DateTime should be converted to universal time when compared, so these scores should be equal.");
        }
    }
}
