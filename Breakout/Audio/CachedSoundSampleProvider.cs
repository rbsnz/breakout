using System;

using NAudio.Wave;

// Source: https://markheath.net/post/fire-and-forget-audio-playback-with

namespace Breakout.Audio
{
    /// <summary>
    /// Provides audio samples of a cached sound.
    /// </summary>
    public class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound _sound;
        private long _position;

        /// <summary>
        /// Gets the wave format of the sound.
        /// </summary>
        public WaveFormat WaveFormat => _sound.WaveFormat;

        /// <summary>
        /// Constructs a new <see cref="CachedSoundSampleProvider"/>
        /// the provides samples of the specified <see cref="CachedSound"/>.
        /// </summary>
        public CachedSoundSampleProvider(CachedSound cachedSound)
        {
            _sound = cachedSound;
        }

        /// <summary>
        /// Reads audio samples from the current position into the specified buffer and returns the number of samples read.
        /// </summary>
        public int Read(float[] buffer, int offset, int count)
        {
            long availableSamples = _sound.AudioData.Length - _position;
            long samplesToCopy = Math.Min(availableSamples, count);
            Array.Copy(_sound.AudioData, _position, buffer, offset, samplesToCopy);
            _position += samplesToCopy;
            return (int)samplesToCopy;
        }
    }
}
