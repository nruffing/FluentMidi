namespace FluentMidi
{
    /// <summary>
    /// Implementation for a MIDI output device.
    /// </summary>
    public sealed class MidiOutputDevice : IMidiOutputDevice
    {
        private const byte DefaultDefaultChannel = 1;

        /// <summary>
        /// Initializes an instance of <see cref="MidiOutputDevice"/>.
        /// </summary>
        /// <param name="deviceId">The identifier of the MIDI output device.</param>
        /// <param name="name">The name of the MIDI output device.</param>
        internal MidiOutputDevice(int deviceId, string name)
        {
            this.DeviceId = deviceId;
            this.Name = name;
        }

        /// <summary>
        /// Gets the identifier of the MIDI output device.
        /// </summary>
        public int DeviceId { get; }

        /// <summary>
        /// Gets the name of the MIDI output device.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the default MIDI channel for the MIDI output device.
        /// </summary>
        public byte DefaultChannel { get; private set; } = DefaultDefaultChannel;

        /// <summary>
        /// Sets the default MIDI channel of the MIDI output device. A valid MIDI
        /// channel is an integer in the inclusive range of 1 to 16.
        /// </summary>
        /// <param name="channel">The MIDI channel to set as the MIDI output device's default.</param>
        /// <returns>The <see cref="IMidiOutputDevice"/>.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the specified channel is not a valid
        /// MIDI channel.</exception>
        public IMidiOutputDevice SetDefaultChannel(byte channel)
        {
            channel.ValidateAsMidiChannel();
            this.DefaultChannel = channel;
            return this;
        }

        /// <summary>
        /// Creates and returns a <see cref="IMidiControlChangeComposer"/> that can be used to 
        /// compose and send a control change event.
        /// </summary>
        /// <returns>A new <see cref="IMidiControlChangeComposer"/>.</returns>
        public IMidiControlChangeComposer ComposeControlChange()
            => new MidiControlChangeComposer(this);

        /// <summary>
        /// Creates and returns a <see cref="IMidiPatchChangeComposer"/> that can be used to 
        /// compose and send a patch change event.
        /// </summary>
        /// <returns>A new <see cref="IMidiPatchChangeComposer"/>.</returns>
        public IMidiPatchChangeComposer ComposePatchChange()
            => new MidiPatchChangeComposer(this);

    }
}