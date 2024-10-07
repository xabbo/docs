using Xabbo;
using Xabbo.Messages;

namespace Examples.ParserComposer;

/*
 * Be careful if editing this file as all of the
 * parser/composer articles depend on the line numbers.
*/

#if FALSE
// For code snippets.
public class WallItem
public class WallItem : IParser<WallItem>
public class WallItem : IComposer
#endif

public class WallItem : IParserComposer<WallItem>
{
    // Define properties here...
    public Id Id { get; set; }
    public int Kind { get; set; }
    public string Location { get; set; } = "";
    public string Data { get; set; } = "";
    public int SecondsUntilExpiration { get; set; }
    public int UsagePolicy { get; set; }
    public Id OwnerId { get; set; }
    public string OwnerName { get; set; } = "";

    public static WallItem Parse(in PacketReader p)
    {
        // Implement parser here...

        // Create a new WallItem.
        WallItem item = new();

        // We read a string and cast it to an Id so it is in the correct type for our model.
        item.Id = (Id)p.ReadString();

        // Read the rest of the fields from the PacketReader and set them on the new WallItem.
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
        // Implement composer here...

        // Write each property to the packet.
        // We convert the Id to a string here, so it is in the correct type for the packet.
        p.WriteString(Id.ToString());
        p.WriteInt(Kind);
        p.WriteString(Location);
        p.WriteString(Data);
        p.WriteInt(SecondsUntilExpiration);
        p.WriteInt(UsagePolicy);
        p.WriteId(OwnerId);
        p.WriteString(OwnerName);
    }
}