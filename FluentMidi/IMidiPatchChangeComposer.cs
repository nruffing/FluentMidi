namespace FluentMidi
{
    /// <summary>
    /// Interface for a MIDI event composer that sends MIDI patch change events.
    /// </summary>
    public interface IMidiPatchChangeComposer
    {
        /// <summary>
        /// Overrides the default channel of the current device for the patch
        /// change event being composed. A valid MIDI channel is an integer in the 
        /// inclusive range of 1 to 16.
        /// </summary>
        /// <param name="channel">The MIDI channel to use for the current patch change 
        /// event being composed.</param>
        /// <returns>The current <see cref="IMidiPatchChangeComposer"/>.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the specified channel is not a 
        /// valid MIDI channel.</exception>
        IMidiPatchChangeComposer WithChannel(byte channel);

        /// <summary>
        /// Sets the patch number for the current patch change being composed. A
        /// patch number must be set before sending the event.
        /// </summary>
        /// <param name="patchNumber">The patch number to use for the current patch
        /// change event being composed.</param>
        /// <returns>The current <see cref="IMidiPatchChangeComposer"/>.</returns>
        IMidiPatchChangeComposer WithPatchNumber(byte patchNumber);

        /// <summary>
        /// Sends the patch change currently being composed. A patch number must be set
        /// before the sending the event.
        /// </summary>
        /// <returns>The currently selected <see cref="IMidiOutputDevice"/>.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the control 
        /// number or value have not been set yet.</exception>
        IMidiOutputDevice Send();
    }
}