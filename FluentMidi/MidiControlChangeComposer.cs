using NAudio.Midi;
using System;

namespace FluentMidi
{
    /// <summary>
    /// Implementation for a MIDI event composer that sends MIDI control change events.
    /// </summary>
    public sealed class MidiControlChangeComposer : IMidiControlChangeComposer
    {
        private byte _channel;
        private byte? _controlNumber = null;
        private byte? _value = null;
        private IMidiOutputDevice _device;

        /// <summary>
        /// Initializes an instance of <see cref="MidiControlChangeComposer"/>.
        /// </summary>
        /// <param name="device">The <see cref="IMidiOutputDevice"/> to send the composed event.</param>
        internal MidiControlChangeComposer(IMidiOutputDevice device)
        {
            this._device = device;
            this._channel = device.DefaultChannel;
        }

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
        public IMidiControlChangeComposer WithChannel(byte channel)
        {
            channel.ValidateAsMidiChannel();

            this._channel = channel;
            return this;
        }

        /// <summary>
        /// Sets the control number for the current control change being composed. A 
        /// control number must be set before sending the event.
        /// </summary>
        /// <param name="controlNumber">The control number to use for the current change
        /// event being composed.</param>
        /// <returns>The current <see cref="IMidiControlChangeComposer"/>.</returns>
        public IMidiControlChangeComposer WithControlNumber(byte controlNumber)
        {
            this._controlNumber = controlNumber;
            return this;
        }

        /// <summary>
        /// Sets the parameter value for the current control change being composed. A
        /// value must be set before sending the event.
        /// </summary>
        /// <param name="value">The parameter value for the current change event being
        /// composed</param>
        /// <returns>The current <see cref="IMidiControlChangeComposer"/>.</returns>
        public IMidiControlChangeComposer WithValue(byte value)
        {
            this._value = value;
            return this;
        }

        /// <summary>
        /// Sends the control change currently being composed. A control number
        /// and value must be set before sending the event.
        /// </summary>
        /// <returns>The currently selected <see cref="IMidiOutputDevice"/>.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the control 
        /// number or value have not been set yet.</exception>
        public IMidiOutputDevice Send()
        {
            if (this._controlNumber == null || this._value == null)
            {
                throw new InvalidOperationException("Both a control number and a value are required.");
            }

            using (MidiOut midi = new MidiOut(this._device.DeviceId))
            {
                midi.Send((this._channel - 1) + (int)MidiCommandCode.ControlChange + (this._controlNumber.Value << 8) + (this._value.Value << 16));
            }

            return this._device;
        }
    }
}