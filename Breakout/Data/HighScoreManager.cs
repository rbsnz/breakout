using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Breakout.Data
{
    /// <summary>
    /// Manages the high score database.
    /// </summary>
    public class HighScoreManager : IReadOnlyList<HighScore>
    {
        private readonly string _scoreFilePath;

        private readonly List<HighScore> _scores = new List<HighScore>();

        /// <summary>
        /// The maximum number of high scores to store.
        /// </summary>
        public int MaxScores { get; }

        /// <summary>
        /// Gets the number of high scores.
        /// </summary>
        public int Count => _scores.Count;

        /// <summary>
        /// Gets the high score at the specified index.
        /// </summary>
        public HighScore this[int index] => _scores[index];

        /// <summary>
        /// Constructs a new high score manager.
        /// </summary>
        public HighScoreManager(int maxScores = 10)
        {
            MaxScores = maxScores;

            _scoreFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "breakout.scores");

            Load();
        }

        private void SortScores()
        {
            _scores.Sort((a, b) => b.Score - a.Score);
            if (_scores.Count > MaxScores)
                _scores.RemoveRange(MaxScores, _scores.Count - MaxScores);
        }

        /// <summary>
        /// Gets if the specified score qualifies as a high score.
        /// </summary>
        public bool IsHighScore(int value) => (_scores.Count < MaxScores) || (value > _scores.Min(x => x.Score));

        /// <summary>
        /// Loads the high scores.
        /// </summary>
        private void Load()
        {
            _scores.Clear();

            if (File.Exists(_scoreFilePath))
            {
                using (Stream fileStream = File.OpenRead(_scoreFilePath))
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    int count = reader.ReadByte();
                    for (int i = 0; i < count; i++)
                    {
                        int score = reader.ReadInt32();
                        string name = reader.ReadString();
                        _scores.Add(new HighScore(name, score));
                    }
                }
            }

            SortScores();
        }

        /// <summary>
        /// Saves the top 10 high scores from the specified list.
        /// </summary>
        private void Save()
        {
            SortScores();

            using (Stream fileStream = File.OpenWrite(_scoreFilePath))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write((byte)_scores.Count);
                foreach (var highScore in _scores)
                {
                    writer.Write(highScore.Score);
                    writer.Write(highScore.Name);
                }
            }
        }

        /// <summary>
        /// Adds the specified high score.
        /// </summary>
        public void Add(HighScore highScore)
        {
            _scores.Add(highScore);
            SortScores();
            Save();
        }

        /// <summary>
        /// Resets all scores.
        /// </summary>
        public void Reset()
        {
            _scores.Clear();

            try { File.Delete(_scoreFilePath); } catch { }
        }

        public IEnumerator<HighScore> GetEnumerator() => _scores.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
