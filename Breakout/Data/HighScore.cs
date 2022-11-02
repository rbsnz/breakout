using System;

namespace Breakout.Data
{
    public class HighScore : IComparable<HighScore>
    {
        public DateTime Date { get; }
        public int Score { get; }
        public string Name { get; }

        public HighScore(DateTime date, int score, string name)
        {
            Date = date;
            Score = score;
            Name = name;
        }

        public HighScore(int score, string name)
            : this(DateTime.UtcNow, score, name)
        { }

        public int CompareTo(HighScore other)
        {
            if (Score == other.Score)
            {
                return Date.ToUniversalTime().CompareTo(other.Date.ToUniversalTime());
            }
            else
            {
                return other.Score.CompareTo(Score);
            }
        }
    }
}
