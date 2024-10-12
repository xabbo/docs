# How to: Block messages

# [Minimal](#tab/minimal)

To block a message, you must accept 2 arguments in the intercept handler. The first argument is an
@Xabbo.Intercept that allows you to call `Block`, and the second is the message instance that
provides the parsed data.

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=block)]

# [Derived](#tab/derived)

To block a message, you can either accept a generic @Xabbo.Intercept`1 specifying the message type
to intercept, where the intercepted message can be accessed via the `Msg` property:

[!code-csharp[](~/src/examples/messages/derived/MyExtension.cs?name=block-generic-intercept)]

Or you can accept an @Xabbo.Intercept as the first argument, and an @Xabbo.Messages.IMessage`1 as
the second argument:

[!code-csharp[](~/src/examples/messages/derived/MyExtension.cs?name=block-non-generic-intercept)]

---