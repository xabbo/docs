using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

using Examples.Parser;

var ext = new GEarthExtension(new GEarthOptions {
    Name = "How to",
    Description = "create a parser",
    Author = "xabbo"
});

// <snippet>
ext.Intercept(In.ItemAdd, e => {
    var item = e.Packet.Read<WallItem>();
    Console.WriteLine($"{item.OwnerName} placed a wall item with ID {item.Id} of kind {item.Kind} at {item.Location}");
});
// </snippet>

ext.Run();