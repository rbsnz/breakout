using System.Collections.Generic;
using System.Linq;

using NAudio.Wave;

// Source: https://markheath.net/post/fire-and-forget-audio-playback-with

namespace Breakout.Audio
{
    /// <summary>
    /// Represents a sound that is played from cached audio data.
    /// </summary>
    public class CachedSound : ISound
    {
        /// <summary>
        /// Gets the wave format of this sound.
        /// </summary>
        public WaveFormat WaveFormat { get; private set; }

        /// <summary>
        /// Gets the audio data of this sound.
        /// </summary>
        public float[] AudioData { get; private set; }

        /// <summary>
        /// Constructs a new cached sound, loading from the specified audio file.
        /// </summary>
        public CachedSound(string audioFileName)
        {
            using (var audioFileReader = new AudioFileReader(audioFileName))
            {
                WaveFormat = audioFileReader.WaveFormat;
                var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
                var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
                int samplesRead;
                while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
                {
                    wholeFile.AddRange(readBuffer.Take(samplesRead));
                }
                AudioData = wholeFile.ToArray();
            }
        }
    }
}
