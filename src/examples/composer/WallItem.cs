using Xabbo;
using Xabbo.Messages;

namespace Examples.Composer;

public class WallItem : IComposer
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

    public void Compose(in PacketWriter p)
    {
        // Write each property to the packet.
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