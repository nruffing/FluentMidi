using NAudio.Midi;
using System;
using System.Collections.Generic;

namespace FluentMidi
{
    /// <summary>
    /// A collection of static methods for querying and selecting MIDI devices
    /// connected to the current system.
    /// </summary>
    public static class MidiDeviceLocator
    {
        /// <summary>
        /// Retrieves a <see cref="IMidiOutputDevice"/> for the device with the
        /// specified identifier.
        /// </summary>
        /// <param name="deviceId">The identifier for requested MIDI output device.</param>
        /// <returns>A <see cref="IMidiOutputDevice"/> for the device with the
        /// specified identifier.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the specified device 
        /// identifier is invalid or a device with that identifier does not exist.</exception>
        public static IMidiOutputDevice SelectForOutput(int deviceId)
        {
            int numberOfDevices = MidiOut.NumberOfDevices;
            if (deviceId < 0 || deviceId >= numberOfDevices)
            {
                throw new ArgumentException("Device id is invalid or does not exist.");
            }

            return GetOutputDevice(deviceId);
        }

        /// <summary>
        /// Retrieves a collection of all MIDI output devices on the current system.
        /// </summary>
        /// <returns>A collection of all MIDI output devices on the current system.</returns>
        public static IEnumerable<IMidiOutputDevice> GetAllOutputDevices()
        {
            int numberOfDevices = MidiOut.NumberOfDevices;
            IMidiOutputDevice[] devices = new IMidiOutputDevice[numberOfDevices];

            for (int i = 0; i < numberOfDevices; i++)
            {
                devices[i] = GetOutputDevice(i);
            }

            return devices;
        }

        private static IMidiOutputDevice GetOutputDevice(int deviceId)
        {
            MidiOutCapabilities capabilities = MidiOut.DeviceInfo(deviceId);
            return new MidiOutputDevice(deviceId, capabilities.ProductName);
        }
    }
}