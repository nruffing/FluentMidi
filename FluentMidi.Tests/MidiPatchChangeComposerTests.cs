using NUnit.Framework;
using System;

namespace FluentMidi.Tests
{
    [TestFixture]
    public class MidiPatchChangeComposer
    {
        [Test]
        [TestCase(0)]
        [TestCase(17)]
        public void WithChannel_InvalidTest(byte channel)
        {
            IMidiPatchChangeComposer composer = GetComposer();
            Assert.Throws<ArgumentException>(() => composer.WithChannel(channel));
        }

        [Test]
        public void WithChannelTest()
        {
            IMidiPatchChangeComposer composer = GetComposer();

            for (byte i = 1; i <= 16; i++)
            {
                Assert.AreSame(composer, composer.WithChannel(i));
            }
        }

        [Test]
        public void WithPatchNumberTest()
        {
            IMidiPatchChangeComposer composer = GetComposer();
            Assert.AreSame(composer, composer.WithPatchNumber(1));
        }

        [Test]
        public void Send_InvalidTest()
            => Assert.Throws<InvalidOperationException>(() => GetComposer().Send());
        

        [Test]
        public void Send()
            => Assert.NotNull(GetComposer()
                .WithPatchNumber(1)
                .Send());

        private IMidiPatchChangeComposer GetComposer()
        {
            IMidiOutputDevice device = MidiDeviceLocator.SelectForOutput(0);
            Assume.That(device != null);

            IMidiPatchChangeComposer composer = device.ComposePatchChange();
            Assume.That(composer != null);
            return composer;
        }
    }
}
