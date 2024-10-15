# 6. Send a packet

Let's send a packet when we activate the extension in G-Earth by clicking the green play button. Our
goal is to make our avatar wave when we click this button.

First, let's find out what packet is sent when we wave. Open the "Packet Logger" extension in
G-Earth, and make sure "View outgoing" is enabled:

![Packet Logger](~/images/tutorial/6-1.png)

Then, make your avatar wave. You should see a packet like the following appear in the log:

```txt
[AvatarExpression]
Outgoing[1336] -> [0][0][0][6][5]8[0][0][0][1]
{out:AvatarExpression}{i:1}
```

Here is a basic explanation of this packet log:

- `[AvatarExpression]` is the name of the message.
- `Outgoing[1336]` means an outgoing header with the value `1336`. This value represents the message
`AvatarExpression`. The following text after `->` represents the bytes of the packet as text.
- `{out:AvatarExpression}{i:1}` represents the packet as an expression. It defines the direction
and message name `{out:AvatarExpression}` followed by the data. In this case, we have a single
integer (represented by `i`) with the value `1`.

See the [structure of a packet](~/docs/in-depth/packet-structure.md) for an in-depth explanation of
the packet log format, packet expressions, and the various packet data types.

This means that if we create a packet with the outgoing header value `1336`, write the integer `1`
and send it, our avatar should wave.

Add the following using statements to the top of your program:

```csharp
using Xabbo;
using Xabbo.Messages;
```

Then add the following code to the @Xabbo.GEarth.GEarthExtension.Activated event handler:

```csharp
ext.Activated += () => {
    // Create a new outgoing header with the value 1336.
    var header = new Header(Direction.Out, 1336);
    // Create a new packet using the header, specifying our client type.
    var packet = new Packet(header, ClientType.Flash);
    // Write an integer with the value 1 to the packet.
    packet.Write(1);
    // Send the packet to the server.
    ext.Send(packet);
};
```

> [!CAUTION]
> The header value may be different depending on the client version. Make sure you follow the steps
> and adjust the value accordingly.

Then when you activate the extension in G-Earth, your avatar should wave:

![Waving Avatar](~/images/tutorial/6-2.png)

However, there is a problem with this method. We would need to hard-code these header values into
our program, but the header values *can change depending on the client version*. Our extension
would break and we would need to update our extension every time the client updates.

We will fix this in the next step.