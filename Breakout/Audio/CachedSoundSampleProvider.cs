using System;

using NAudio.Wave;

// Source: https://markheath.net/post/fire-and-forget-audio-playback-with

namespace Breakout.Audio
{
    public class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound _sound;
        private long _position;

        public WaveFormat WaveFormat => _sound.WaveFormat;

        public CachedSoundSampleProvider(CachedSound cachedSound)
        {
            _sound = cachedSound;
        }

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
