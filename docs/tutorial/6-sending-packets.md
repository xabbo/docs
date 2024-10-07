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

A packet has 3 parts - the length, header, and data. The length declares the number of bytes that
make up the header and data. The header value defines what type of message the packet represents,
for example, whether a user is talking, shouting, or whispering. The data contains the information
of the message, for example, the actual contents of a chat message.

In the packet logger output, the first line has the message name, `AvatarExpression`.

The second line has the direction `Outgoing` and header value `1336`, followed by a text
representation of the entire packet, including its length, header and data. We can see the 3 parts
of the packet:

- `[0][0][0][6]` decodes to the integer `6`, representing the length of the header and data, and it
is always 4 bytes in length. This means that 6 more bytes follow.
- `[5]8` is the header value, which decodes to `1336`. It is always 2 bytes in length.
- `[0][0][0][1]` is the packet data. We have 4 bytes here, which is the length of an integer.

The third line represents the packet as a packet expression. G-Earth will attempt to guess the
structure of the packet for us and output a packet expression such as this one. In this case,
`{out:AvatarExpression}` represents the message direction and name, and the `{i:1}` means an integer
value of `1` (this specifies which action to perform). This means that if we create a packet with
the outgoing header value `1336`, write the integer `1` and send it, our avatar should wave.

> [!NOTE]
> In xabbo, a @Xabbo.Messages.Header contains a @Xabbo.Messages.Header.Direction and
> @Xabbo.Messages.Header.Value. The @Xabbo.Messages.Packet.Length of a packet defines the length of
> the data only, excluding the header.
>
> On Shockwave the length field is not present, so you will only see the header and data in the
> packet logger.

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