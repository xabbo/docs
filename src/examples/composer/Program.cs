using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

using Examples.Composer;

var ext = new GEarthExtension(new GEarthOptions {
    Name = "How to",
    Description = "create a composer",
    Author = "xabbo"
});

// <snippet>
ext.Activated += () => {
    // Create a new WallItem.
    var item = new WallItem
    {
        Id = "1",
        Kind = 4001,
        Data = "501",
        Location = ":w=10,10 l=0,0 r",
        OwnerId = 1,
        OwnerName = "xabbo"
    };
    // Send the WallItem to the client.
    ext.Send(In.ItemAdd, item);
};
// </snippet>

ext.Run();