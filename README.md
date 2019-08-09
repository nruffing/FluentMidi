# FluentMidi
A fluent library for sending MIDI control and patch change events.

This library uses [NAudio](https://github.com/naudio/NAudio) for MIDI functionality and wraps a fluent interface around its functionality to find MIDI output devices on the current system and send patch and control change events.

```c#
/*
  Find all MIDI output devices on the current system.
*/
IEnumerable<IMidiOutputDevice> devices = MidiDeviceLocator.GetAllOutputDevices();

/*
  Send a control change event.
*/
MidiDeviceLocator.SelectForOutput(1) // DeviceId for the MIDI output device
  .SetDefaultChannel(1) 
  .ComposeControlChange()
    .WithControlNumber(63)
    .WithValue(127)
    .Send();

/*
  Send a patch change event.
*/
MidiDeviceLocator.SelectForOutput(1) // DeviceId for the MIDI output device
  .SetDefaultChannel(1) 
  .ComposePatchChange()
    .WithPatchNumber(12)
    .Send();
```
