# How to: Send messages

## Send a message

To send a message, simply provide an instance of @Xabbo.Messages.IMessage`1 to the `Send` method:

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=send)]

Because the `Xabbo.Core` messages are designed to work cross-client where possible, these will work
on both the Flash and Shockwave clients.

## Send a message to the client

Since messages are associated with identifiers, they are inherently directional. Just provide an
incoming message to the `Send` method:

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=send-to-client)]

The `Xabbo.Core` messages are separated into the @Xabbo.Core.Messages.Incoming and
@Xabbo.Core.Messages.Outgoing namespaces.

> [!WARNING]
> If a message does not support a specific client, it will throw an exception if you try to send it.
> All messages in `Xabbo.Core` are documented with their supported clients and the identifiers they
> use.