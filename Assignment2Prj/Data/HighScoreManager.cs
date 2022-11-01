using System;
using System.Collections.Generic;
using System.IO;

namespace Breakout.Data
{
    public class HighScoreManager
    {
        private readonly string _scoreFilePath;

        public HighScoreManager()
        {
            _scoreFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "breakout.scores");
        }

        public List<HighScore> LoadScores()
        {
            var scores = new List<HighScore>();

            if (File.Exists(_scoreFilePath))
            {
                using (Stream fileStream = File.OpenRead(_scoreFilePath))
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    while (fileStream.Position < fileStream.Length)
                    {
                        string name = reader.ReadString();
                        int score = reader.ReadInt32();
                        scores.Add(new HighScore(name, score));
                    }
                }
            }

            return scores;
        }

        public void AddScore(HighScore score)
        {
            // TODO
        }

    }
}
