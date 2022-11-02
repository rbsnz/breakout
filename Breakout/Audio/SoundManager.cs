using System;
using System.IO;

using NAudio.Wave;
using NAudio.Wave.SampleProviders;

using Breakout.Audio;
using Breakout.Services;

// Source: https://markheath.net/post/fire-and-forget-audio-playback-with

namespace Breakout
{
    /// <inheritdoc cref="ISoundManager"/>
    public class SoundManager : ISoundManager
    {
        private readonly IWavePlayer _output;
        private readonly MixingSampleProvider _mixer;

        public SoundManager()
        {
            _output = new WaveOutEvent { DesiredLatency = 100 };
            _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44_100, 2)) { ReadFully = true };

            _output.Volume = 0.8f;
            _output.Init(_mixer);
            _output.Play();
        }

        private ISampleProvider ConvertChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == _mixer.WaveFormat.Channels)
            {
                return input;
            }

            if (input.WaveFormat.Channels == 1 && _mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }

            throw new NotImplementedException("Conversion for this channel count is not yet implemented.");
        }

        private void AddMixerInput(ISampleProvider input)
        {
            _mixer.AddMixerInput(ConvertChannelCount(input));
        }

        public void PlaySound(CachedSound sound)
        {
            AddMixerInput(new CachedSoundSampleProvider(sound));
        }

        public ISound Load(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ISound Load(string fileName)
        {
            return new CachedSound(fileName);
        }

        public void Play(ISound sound)
        {
            if (sound is null)
                throw new ArgumentNullException(nameof(sound));

            if (!(sound is CachedSound cachedSound))
                throw new NotSupportedException($"Cannot play the specified sound type: {sound.GetType()}.");

            AddMixerInput(new CachedSoundSampleProvider(cachedSound));
        }
    }
}
