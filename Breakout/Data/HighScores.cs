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
    public class HighScores : IReadOnlyList<HighScore>
    {
        private readonly List<HighScore> _scores = new List<HighScore>();

        /// <summary>
        /// Gets the path of the high scores file.
        /// </summary>
        public string FilePath { get; }

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
        /// Constructs a new high score manager with the specified file path and max scores.
        /// </summary>
        public HighScores(string filePath = @"%LOCALAPPDATA%\breakout.scores", int maxScores = 5)
        {
            FilePath = Path.GetFullPath(Environment.ExpandEnvironmentVariables(filePath));
            MaxScores = maxScores;

            Load();
        }

        private void SortScores()
        {
            _scores.Sort();
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

            if (File.Exists(FilePath))
            {
                using (Stream fileStream = File.OpenRead(FilePath))
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    if (fileStream.Position < fileStream.Length)
                    {
                        int count = reader.ReadByte();
                        for (int i = 0; i < count; i++)
                        {
                            DateTime date = DateTime.FromBinary(reader.ReadInt64());
                            int score = reader.ReadInt32();
                            string name = reader.ReadString();
                            
                            _scores.Add(new HighScore(date, score, name));
                        }
                    }
                }
            }

            SortScores();
        }

        /// <summary>
        /// Saves the top high scores from the specified list.
        /// </summary>
        private void Save()
        {
            SortScores();

            using (Stream fileStream = File.OpenWrite(FilePath))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write((byte)_scores.Count);
                foreach (var highScore in _scores)
                {
                    writer.Write(highScore.Date.ToBinary());
                    writer.Write(highScore.Score);
                    writer.Write(highScore.Name);
                }
            }
        }

        /// <summary>
        /// Adds the specified high score
        /// </summary>
        public bool Add(HighScore highScore)
        {
            if (!IsHighScore(highScore.Score))
                return false;

            _scores.Add(highScore);
            SortScores();
            Save();
            return true;
        }

        /// <summary>
        /// Resets all scores.
        /// </summary>
        public void Clear()
        {
            _scores.Clear();

            try { File.Delete(FilePath); } catch { }
        }

        public IEnumerator<HighScore> GetEnumerator() => _scores.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
