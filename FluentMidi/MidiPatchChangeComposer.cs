using NAudio.Midi;
using System;

namespace FluentMidi
{
    /// <summary>
    /// Implementation for a MIDI event composer that sends MIDI patch change events.
    /// </summary>
    class MidiPatchChangeComposer : IMidiPatchChangeComposer
    {
        private byte _channel;
        private byte? _patchNumber;
        private IMidiOutputDevice _device;

        /// <summary>
        /// Initializes an instance of <see cref="MidiPatchChangeComposer"/>.
        /// </summary>
        /// <param name="device">The <see cref="IMidiOutputDevice"/> to send the composed event.</param>
        internal MidiPatchChangeComposer(IMidiOutputDevice device)
        {
            this._device = device;
            this._channel = device.DefaultChannel;
        }

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
        public IMidiPatchChangeComposer WithChannel(byte channel)
        {
            channel.ValidateAsMidiChannel();

            this._channel = channel;
            return this;
        }

        /// <summary>
        /// Sets the patch number for the current patch change being composed. A
        /// patch number must be set before sending the event.
        /// </summary>
        /// <param name="patchNumber">The patch number to use for the current patch
        /// change event being composed.</param>
        /// <returns>The current <see cref="IMidiPatchChangeComposer"/>.</returns>
        public IMidiPatchChangeComposer WithPatchNumber(byte patchNumber)
        {
            this._patchNumber = patchNumber;
            return this;
        }

        /// <summary>
        /// Sends the patch change currently being composed. A patch number must be set
        /// before the sending the event.
        /// </summary>
        /// <returns>The currently selected <see cref="IMidiOutputDevice"/>.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the control 
        /// number or value have not been set yet.</exception>
        public IMidiOutputDevice Send()
        {
            if (this._patchNumber == null)
            {
                throw new InvalidOperationException("A patch number is required");
            }

            using (MidiOut midi = new MidiOut(this._device.DeviceId))
            {
                midi.Send(new PatchChangeEvent(0, this._channel, this._patchNumber.Value).GetAsShortMessage());
            }

            return this._device;
        }
    }
}