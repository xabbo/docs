# 7. Using identifiers

Instead of using header values, we can use a message @Xabbo.Messages.Identifier. An identifier
contains a client type, direction, and name. G-Earth provides extensions with message information,
allowing us to refer to message headers by name, thanks to the [Sulek API](https://sulek.dev/about).

Notice the message name `AvatarExpression` in the packet log:

```txt
[AvatarExpression]
Outgoing[1336] -> [0][0][0][6][5]8[0][0][0][1]
{out:AvatarExpression}{i:1}
```

We can use this name instead of the header value `1336`. Change your code to the following:

```csharp
ext.Activated += () => {
    // Create an identifier.
    var identifier = new Identifier(ClientType.Flash, Direction.Out, "AvatarExpression");
    // Use the extension's message manager to resolve the identifier to a header.
    var header = ext.Messages.Resolve(identifier);
    // Create a new packet using the header, specifying our client type.
    var packet = new Packet(header, ClientType.Flash);
    // Write an integer with the value 1 to the packet.
    packet.Write(1);
    // Send the packet to the server.
    ext.Send(packet);
};
```

Here, we create an identifier with the @Xabbo.ClientType.Flash client type, @Xabbo.Direction.Out
direction and `"AvatarExpression"` name. We then use the extension's @Xabbo.Messages.IMessageManager
to resolve this identifier to a header.

We can confirm that our avatar waves when activating the extension, meaning that the identifier
successfully resolved to the correct header. G-Earth will supply our extension with updated header
values for future client versions via the Sulek API, solving our  issue from the previous step.

Another issue we still have is that we need to manually create an identifier for each message we
want to send. The `Xabbo.Messages` package contains the @Xabbo.Messages.Flash.In?text=In and
@Xabbo.Messages.Flash.Out?text=Out classes that allow us to easily reference message identifiers and
also gives us autocompletion in our IDE. To add the package:

# [.NET CLI](#tab/cli)

```sh
dotnet add package Xabbo.Messages
```

# [Visual Studio](#tab/vs)

TODO

---

Once the package reference has been added, include the following using statement at the top of your
program:

```csharp
using Xabbo.Messages.Flash;
```

If we wanted to use Shockwave message names, we could import the @Xabbo.Messages.Shockwave
namespace.

Next, update your code to the following:

```csharp
ext.Activated += () => {
    // Use the extension's message manager to resolve the AvatarExpression identifier to a header.
    var header = ext.Messages.Resolve(Out.AvatarExpression);
    // Create a new packet using the header, specifying our client type.
    var packet = new Packet(header, ClientType.Flash);
    // Write an integer with the value 1 to the packet.
    packet.Write(1);
    // Send the packet to the server.
    ext.Send(packet);
};
```

The extension should work the same as before.

Finally, instead of creating a packet, writing to it, and then sending it, we can use the `Send`
method to do the same thing in one line. Update your code to the following:

```csharp
ext.Activated += () => {
    // Send AvatarExpression with the Wave (1) action.
    ext.Send(Out.AvatarExpression, 1);
};
```

This does the same as before but with a much more concise syntax - it resolves the identifier to a
header, creates a packet with that header, writes the integer `1` to the packet and sends it. With
this syntax, we can provide any number of values to be written to the packet.
