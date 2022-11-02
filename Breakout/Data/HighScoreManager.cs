using System;
using System.Collections.Generic;
using System.IO;

namespace Breakout.Data
{
    /// <summary>
    /// Manages the high score database.
    /// </summary>
    public class HighScoreManager
    {
        private readonly string _scoreFilePath;

        /// <summary>
        /// Constructs a new high score manager.
        /// </summary>
        public HighScoreManager()
        {
            _scoreFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "breakout.scores");
        }

        /// <summary>
        /// Loads the high scores.
        /// </summary>
        public List<HighScore> Load()
        {
            var scores = new List<HighScore>();

            if (File.Exists(_scoreFilePath))
            {
                using (Stream fileStream = File.OpenRead(_scoreFilePath))
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    int count = reader.ReadByte();

                    while (fileStream.Position < fileStream.Length)
                    {
                        string name = reader.ReadString();
                        int score = reader.ReadInt32();
                        scores.Add(new HighScore(name, score));
                    }
                }
            }

            scores.Sort((a, b) => b.Score - a.Score);
            return scores;
        }

        /// <summary>
        /// Saves the top 10 high scores from the specified list.
        /// </summary>
        public void Save(List<HighScore> highScores)
        {
            highScores.Sort((a, b) => b.Score - a.Score);

            if (highScores.Count > 10)
            {
                highScores.RemoveRange(10, highScores.Count - 10);
            }

            using (Stream fileStream = File.OpenWrite(_scoreFilePath))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write((ushort)highScores.Count);

                foreach (var highScore in highScores)
                {
                    writer.Write(highScore.Score);
                    writer.Write(highScore.Name);
                }
            }
        }
    }
}
