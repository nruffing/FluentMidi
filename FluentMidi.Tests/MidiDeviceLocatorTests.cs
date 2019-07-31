using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMidi.Tests
{
    [TestFixture]
    public class MidiDeviceLocatorTests
    {
        [Test]
        public void GetAllOutputDevicesTest()
        {
            IEnumerable<IMidiOutputDevice> devices = MidiDeviceLocator.GetAllOutputDevices();

            Assume.That(devices.Any()); // whether or not there are actually any midi output devices is going to be dependant on the machine the test is running on

            HashSet<int> uniqueDeviceIds = new HashSet<int>();
            foreach (IMidiOutputDevice device in devices)
            {
                Assert.IsNotNull(device);
                Assert.True(device.DeviceId > -1);
                Assert.False(string.IsNullOrWhiteSpace(device.Name));

                Assert.True(uniqueDeviceIds.Add(device.DeviceId), "Not all MIDI output devices have a unique device ID");
            }
        }

        [Test]
        [TestCase(-1)]
        [TestCase(9999)]
        public void SelectForOutput_InvalidDeviceNumber(int deviceId)
        {
            IEnumerable<IMidiOutputDevice> devices = MidiDeviceLocator.GetAllOutputDevices();
            Assume.That(devices.Any()); // whether or not there are actually any midi output devices is going to be dependant on the machine the test is running on
            Assume.That(devices.Count() < 9999);

            Assert.Throws<ArgumentException>(() => MidiDeviceLocator.SelectForOutput(deviceId));
        }

        [Test]
        public void SelectForOutputTest()
        {
            IEnumerable<IMidiOutputDevice> devices = MidiDeviceLocator.GetAllOutputDevices();
            Assume.That(devices.Any()); // whether or not there are actually any midi output devices is going to be dependant on the machine the test is running on

            IMidiOutputDevice device = MidiDeviceLocator.SelectForOutput(0);
            Assert.IsNotNull(device);
            Assert.AreEqual(0, device.DeviceId);
            Assert.False(string.IsNullOrWhiteSpace(device.Name));
        }
    }
}