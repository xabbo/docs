using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

namespace Examples.ExtensionTypes.Derived;

[Extension(
    Name = "Derived",
    Description = "a derived example",
    Author = "xabbo",
    Version = "1.0"
)]
partial class MyExtension : GEarthExtension
{
    // Intercept a walk message and shout out the coordinates.
    [InterceptOut(nameof(Out.MoveAvatar))]
    void OnWalk(Intercept e)
    {
        var (x, y) = e.Packet.Read<int, int>();
        Send(Out.Shout, $"I am walking to {x}, {y}", 0);
    }
}