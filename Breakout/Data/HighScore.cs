using System;

namespace Breakout.Data
{
    /// <summary>
    /// Represents a high score with a date and name.
    /// </summary>
    public class HighScore : IComparable<HighScore>
    {
        /// <summary>
        /// Gets the date of this high score.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Gets the value of this high score.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Gets the name of this high score.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Constructs a new high score with the specified date, value and name.
        /// </summary>
        public HighScore(DateTime date, int value, string name)
        {
            Date = date;
            Value = value;
            Name = name;
        }

        /// <summary>
        /// Constructs a new high score with the current date/time, specified value and name.
        /// </summary>
        public HighScore(int value, string name)
            : this(DateTime.UtcNow, value, name)
        { }

        /// <summary>
        /// Compares this high score another and returns the precedence of this high score.
        /// </summary>
        public int CompareTo(HighScore other)
        {
            if (Value == other.Value)
            {
                return Date.ToUniversalTime().CompareTo(other.Date.ToUniversalTime());
            }
            else
            {
                return other.Value.CompareTo(Value);
            }
        }
    }
}
