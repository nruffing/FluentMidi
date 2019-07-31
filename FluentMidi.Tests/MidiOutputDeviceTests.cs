using NUnit.Framework;
using System;

namespace FluentMidi.Tests
{
    [TestFixture]
    public class MidiOutputDeviceTests
    {
        [Test]
        public void ComposeControlChangeTest()
        {
            IMidiOutputDevice device = MidiDeviceLocator.SelectForOutput(0);
            Assume.That(device != null);

            Assert.IsNotNull(device.ComposeControlChange());
        }

        [Test]
        public void ComposePatchChangeTest()
        {
            IMidiOutputDevice device = MidiDeviceLocator.SelectForOutput(0);
            Assume.That(device != null);

            Assert.IsNotNull(device.ComposePatchChange());
        }

        [Test]
        [TestCase(0)]
        [TestCase(17)]
        public void SetDefaultChannel_InvalidTest(byte channel)
        {
            IMidiOutputDevice device = MidiDeviceLocator.SelectForOutput(0);
            Assume.That(device != null);

            Assert.Throws<ArgumentException>(() => device.SetDefaultChannel(channel));
        }

        [Test]
        public void SetDefaultChannelTest()
        {
            IMidiOutputDevice device = MidiDeviceLocator.SelectForOutput(0);
            Assume.That(device != null);

            for (byte i = 1; i <= 16; i++)
            {
                Assert.AreSame(device, device.SetDefaultChannel(i));
                Assert.AreEqual(i, device.DefaultChannel);
            }
        }
    }
}