# How to: Use request messages

Request messages are outgoing messages that are associated with an incoming response message and
response data type. They implement the @Xabbo.Messages.IRequestMessage`3 interface.

## Send a request

To send a request, use the `RequestAsync` method from an asynchronous method, passing in an instance
of a request message.

The @Xabbo.Core.Messages.Outgoing.GetUserDataMsg used in the previous example from
[receive a message](~/docs/how-to/messages/receive-messages.md) is also a request message, and can
be used in `RequestAsync`:

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=request)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/messages/derived/MyExtension.cs?name=request)]

---

In the above example, a @Xabbo.Core.Messages.Outgoing.GetUserDataMsg is sent to the server to
request the user's data, which responds with a @Xabbo.Core.Messages.Incoming.UserDataMsg, after
which the @Xabbo.Core.UserData is extracted from the message and returned as the result.

> [!TIP]
> All request messages from `Xabbo.Core` indicate which response message they are a request for, so
> check the @Xabbo.Core.Messages.Outgoing documentation page.