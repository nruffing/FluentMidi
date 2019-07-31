using System;

namespace FluentMidi
{
    /// <summary>
    /// A collection extension methods that can performed on bytes.
    /// </summary>
    internal static class ByteExtensions
    {
        /// <summary>
        /// The minimum valid MIDI channel.
        /// </summary>
        private const byte MinimumMidiChannel = 1;
        
        /// <summary>
        /// The maximum valid MIDI channel.
        /// </summary>
        private const byte MaximumMidiChannel = 16;

        /// <summary>
        /// Validates that the channel represents a valid MIDI channel (i.e. [1-16]).
        /// An exception is thrown if the channel does not represent a valid MIDI channel.
        /// </summary>
        /// <param name="channel">The channel number to validate.</param>
        /// <exception cref="System.ArgumentException">Thrown if the specified channel number does not represent a valid MIDI channel.</exception>
        internal static void ValidateAsMidiChannel(this byte channel)
        {
            if (channel < MinimumMidiChannel || channel > MaximumMidiChannel)
            {
                throw new ArgumentException("Channel must be in the inclusive range of 1 through 16.");
            }
        }
    }
}