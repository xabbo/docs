using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

using Examples.ParserComposer;

var ext = new GEarthExtension(new GEarthOptions {
    Name = "How to",
    Description = "create a parser and composer",
    Author = "xabbo"
});

// <composer>
ext.Activated += () => {
    // Create a new WallItem.
    var item = new WallItem
    {
        Id = 1,
        Kind = 4001,
        Data = "501",
        Location = ":w=10,10 l=0,0 r",
        OwnerId = 1,
        OwnerName = "xabbo"
    };
    // Send the WallItem to the client.
    ext.Send(In.ItemAdd, item);
};
// </composer>

// <parser>
ext.Intercept(In.ItemAdd, e => {
    var item = e.Packet.Read<WallItem>();
    Console.WriteLine($"{item.OwnerName} placed a wall item with ID {item.Id} of kind {item.Kind} at {item.Location}");
});
// </parser>

// <parser-composer>
ext.Intercept(In.ItemAdd, e => {
    // Modify a WallItem
    e.Packet.Modify<WallItem>(item => {
        // Change the item kind to 4001 (poster)
        item.Kind = 4001;
        // Change the item data to "501" (Jolly Roger)
        item.Data = "501";
        // Return the modified item.
        return item;
    });
});
// </parser-composer>

ext.Run();