using NUnit.Framework;
using System;

namespace FluentMidi.Tests
{
    [TestFixture]
    public class MidiControlChangeComposerTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(17)]
        public void WithChannel_InvalidTest(byte channel)
        {
            IMidiControlChangeComposer composer = GetComposer();
            Assert.Throws<ArgumentException>(() => composer.WithChannel(channel));
        }

        [Test]
        public void WithChannelTest()
        {
            IMidiControlChangeComposer composer = GetComposer();

            for (byte i = 1; i <= 16; i++)
            {
                Assert.AreSame(composer, composer.WithChannel(i));
            }
        }

        [Test]
        public void WithControlNumberTest()
        {
            IMidiControlChangeComposer composer = GetComposer();
            Assert.AreSame(composer, composer.WithControlNumber(1));
        }

        [Test]
        public void WithValueTest()
        {
            IMidiControlChangeComposer composer = GetComposer();
            Assert.AreSame(composer, composer.WithValue(1));
        }

        [Test]
        public void Send_InvalidTest()
        {
            Assert.Throws<InvalidOperationException>(() => GetComposer().Send());
            Assert.Throws<InvalidOperationException>(() => GetComposer().WithControlNumber(1).Send());
            Assert.Throws<InvalidOperationException>(() => GetComposer().WithValue(1).Send());
        }

        [Test]
        public void Send()
            => Assert.NotNull(GetComposer()
                .WithControlNumber(1)
                .WithValue(1)
                .Send());

        private IMidiControlChangeComposer GetComposer()
        {
            IMidiOutputDevice device = MidiDeviceLocator.SelectForOutput(0);
            Assume.That(device != null);

            IMidiControlChangeComposer composer = device.ComposeControlChange();
            Assume.That(composer != null);
            return composer; 
        }
    }
}