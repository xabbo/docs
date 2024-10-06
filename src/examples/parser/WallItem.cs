using Xabbo;
using Xabbo.Messages;

namespace Examples.Parser;

#if FALSE
public class WallItem
#endif

public class WallItem : IParser<WallItem>
{
    public string Id { get; set; } = "";
    public int Kind { get; set; }
    public string Location { get; set; } = "";
    public string Data { get; set; } = "";
    public int SecondsUntilExpiration { get; set; }
    public int UsagePolicy { get; set; }
    public Id OwnerId { get; set; }
    public string OwnerName { get; set; } = "";

    // Define properties here...

    public static WallItem Parse(in PacketReader p)
    {
        // Create a new WallItem.
        WallItem item = new();

        // Read the fields from the PacketReader and set them on the new WallItem.
        item.Id = p.ReadString();
        item.Kind = p.ReadInt();
        item.Location = p.ReadString();
        item.Data = p.ReadString();
        item.SecondsUntilExpiration = p.ReadInt();
        item.UsagePolicy = p.ReadInt();
        item.OwnerId = p.ReadId();
        item.OwnerName = p.ReadString();

        // Return the parsed item.
        return item;
    }
}