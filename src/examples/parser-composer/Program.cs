using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

using Examples.ParserComposer;

var ext = new GEarthExtension(new GEarthOptions {
    Name = "How to",
    Description = "combine parser and composer",
    Author = "xabbo"
});

// <snippet>
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
// </snippet>

ext.Run();