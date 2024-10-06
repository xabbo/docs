using Xabbo;
using Xabbo.Messages;

namespace Examples.ParserComposer;

public class WallItem : IParserComposer<WallItem>
{
    // Define your properties here
    public string Id { get; set; } = "";
    public int Kind { get; set; }
    public string Location { get; set; } = "";
    public string Data { get; set; } = "";
    public int SecondsUntilExpiration { get; set; }
    public int UsagePolicy { get; set; }
    public Id OwnerId { get; set; }
    public string OwnerName { get; set; } = "";

    // ...

    public static WallItem Parse(in PacketReader p)
    {
        // Implement parser here

        // Create a new WallItem.
        WallItem item = new();

        // Read the fields from the PacketReader and set them on a new WallItem.
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

    public void Compose(in PacketWriter p)
    {
        // Implement composer here
        p.WriteString(Id);
        p.WriteInt(Kind);
        p.WriteString(Location);
        p.WriteString(Data);
        p.WriteInt(SecondsUntilExpiration);
        p.WriteInt(UsagePolicy);
        p.WriteId(OwnerId);
        p.WriteString(OwnerName);
    }
}