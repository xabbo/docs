using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

// Instantiate a new GEarthExtension.
var ext = new GEarthExtension(new GEarthOptions {
    Name = "Minimal",
    Description = "a minimal example",
    Author = "xabbo",
    Version = "1.0"
});

// Intercept a walk message and shout out the coordinates.
ext.Intercept(Out.MoveAvatar, e => {
    var (x, y) = e.Packet.Read<int, int>();
    ext.Send(Out.Shout, $"I am walking to {x}, {y}", 0);
});

// Run the extension.
ext.Run();