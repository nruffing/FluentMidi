namespace FluentMidi
{
    /// <summary>
    /// Interface for a MIDI output device.
    /// </summary>
    public interface IMidiOutputDevice
    {
        /// <summary>
        /// Gets the identifier of the MIDI output device.
        /// </summary>
        int DeviceId { get; }

        /// <summary>
        /// Gets the name of the MIDI output device.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the default MIDI channel for the MIDI output device.
        /// </summary>
        byte DefaultChannel { get; }

        /// <summary>
        /// Sets the default MIDI channel of the MIDI output device. A valid MIDI
        /// channel is an integer in the inclusive range of 1 to 16.
        /// </summary>
        /// <param name="channel">The MIDI channel to set as the MIDI output device's default.</param>
        /// <returns>The <see cref="IMidiOutputDevice"/>.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the specified channel is not a valid
        /// MIDI channel.</exception>
        IMidiOutputDevice SetDefaultChannel(byte channel);

        /// <summary>
        /// Creates and returns a <see cref="IMidiControlChangeComposer"/> that can be used to 
        /// compose and send a control change event.
        /// </summary>
        /// <returns>A new <see cref="IMidiControlChangeComposer"/>.</returns>
        IMidiControlChangeComposer ComposeControlChange();

        /// <summary>
        /// Creates and returns a <see cref="IMidiPatchChangeComposer"/> that can be used to 
        /// compose and send a patch change event.
        /// </summary>
        /// <returns>A new <see cref="IMidiPatchChangeComposer"/>.</returns>
        IMidiPatchChangeComposer ComposePatchChange();
    }
}