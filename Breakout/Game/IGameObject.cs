namespace Breakout.Game
{
    /// <summary>
    /// Represents a game object.
    /// </summary>
    public interface IGameObject : IDrawable
    {
        /// <summary>
        /// Updates the object and returns whether the object is still alive.
        /// </summary>
        /// <returns>Whether the object is still alive.</returns>
        bool Update();
    }
}
