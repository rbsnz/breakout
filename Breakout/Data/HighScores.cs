using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Breakout.Services;

namespace Breakout.Data
{
    /// <inheritdoc cref="IHighScores"/>
    public class HighScores : IHighScores
    {
        private readonly List<HighScore> _scores = new List<HighScore>();

        /// <summary>
        /// Gets the path of the high scores file.
        /// </summary>
        public string FilePath { get; }

        /// <inheritdoc/>
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

        // Return the enumerator of the backing list.
        public IEnumerator<HighScore> GetEnumerator() => _scores.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Sorts the scores and removes the lowest scores if there are more than <see cref="MaxScores"/> in the list.
        /// </summary>
        private void SortScores()
        {
            _scores.Sort();
            if (_scores.Count > MaxScores)
                _scores.RemoveRange(MaxScores, _scores.Count - MaxScores);
        }

        /// <inheritdoc/>
        public bool IsHighScore(int value) => (_scores.Count < MaxScores) || (value > _scores.Min(x => x.Value));

        /// <summary>
        /// Loads the high scores.
        /// </summary>
        private void Load()
        {
            _scores.Clear();

            // Load each score from the specified file path.
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

            // Sort scores after loading.
            SortScores();
        }

        /// <summary>
        /// Saves the top high scores from the specified list.
        /// </summary>
        private void Save()
        {
            // Sort scores before saving.
            SortScores();

            // Save each score to the specified file path.
            using (Stream fileStream = File.OpenWrite(FilePath))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write((byte)_scores.Count);
                foreach (var highScore in _scores)
                {
                    writer.Write(highScore.Date.ToBinary());
                    writer.Write(highScore.Value);
                    writer.Write(highScore.Name);
                }
            }
        }

        /// <inheritdoc/>
        public bool Add(HighScore highScore)
        {
            if (!IsHighScore(highScore.Value))
                return false;

            _scores.Add(highScore);
            SortScores();
            Save();
            return true;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _scores.Clear();

            try { File.Delete(FilePath); } catch { }
        }
    }
}
