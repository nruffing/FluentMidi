namespace FluentMidi
{
    /// <summary>
    /// Interface for a MIDI event composer that sends MIDI control change events.
    /// </summary>
    public interface IMidiControlChangeComposer
    {
        /// <summary>
        /// Overrides the default channel of the current device for the current control
        /// change event being composed. A valid MIDI channel is an integer in the 
        /// inclusive range of 1 to 16.
        /// </summary>
        /// <param name="channel">The MIDI channel to use for the current control change 
        /// event being composed.</param>
        /// <returns>The current <see cref="IMidiControlChangeComposer"/>.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the specified channel is not a 
        /// valid MIDI channel.</exception>
        IMidiControlChangeComposer WithChannel(byte channel);

        /// <summary>
        /// Sets the control number for the current control change being composed. A 
        /// control number must be set before sending the event.
        /// </summary>
        /// <param name="controlNumber">The control number to use for the current change
        /// event being composed.</param>
        /// <returns>The current <see cref="IMidiControlChangeComposer"/>.</returns>
        IMidiControlChangeComposer WithControlNumber(byte controlNumber);

        /// <summary>
        /// Sets the parameter value for the current control change being composed. A
        /// value must be set before sending the event.
        /// </summary>
        /// <param name="value">The parameter value for the current change event being
        /// composed</param>
        /// <returns>The current <see cref="IMidiControlChangeComposer"/>.</returns>
        IMidiControlChangeComposer WithValue(byte value);

        /// <summary>
        /// Sends the control change currently being composed. A control number
        /// and value must be set before sending the event.
        /// </summary>
        /// <returns>The currently selected <see cref="IMidiOutputDevice"/>.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the control 
        /// number or value have not been set yet.</exception>
        IMidiOutputDevice Send();
    }
}