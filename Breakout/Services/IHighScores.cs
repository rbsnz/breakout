
using System.Collections.Generic;

using Breakout.Data;

namespace Breakout.Services
{
    /// <summary>
    /// Represents a high score manager.
    /// </summary>
    public interface IHighScores : IReadOnlyList<HighScore>
    {
        /// <summary>
        /// The maximum number of high scores stored by this high score manager.
        /// </summary>
        int MaxScores { get; }

        /// <summary>
        /// Gets if the specified score qualifies as a high score.
        /// </summary>
        bool IsHighScore(int score);

        /// <summary>
        /// Attempts the add the specified high score and returns whether it was successful.
        /// </summary>
        /// <returns><see langword="true"/> if the score was added.</returns>
        bool Add(HighScore highScore);

        /// <summary>
        /// Clears all high scores.
        /// </summary>
        void Clear();
    }
}
