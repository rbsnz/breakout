using System.IO;

using Breakout.Audio;

namespace Breakout.Services
{
    public interface ISoundManager
    {
        /// <summary>
        /// Loads a sound from the specified stream.
        /// </summary>
        ISound Load(Stream stream);

        /// <summary>
        /// Loads a sound from the specified file.
        /// </summary>
        ISound Load(string fileName);

        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        void Play(ISound sound);
    }
}
