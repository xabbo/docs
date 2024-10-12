# How to: Modify messages

# [Minimal](#tab/minimal)

To modify a message, return an instance of the modified message from your message intercept handler:

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=modify)]

# [Derived](#tab/derived)

To modify a message, change the return type of your intercept method to `IMessage?`, and return an
instance of the modified message from your message intercept handler:

[!code-csharp[](~/src/examples/messages/derived/MyExtension.cs?name=modify)]

---

It is possible to block a message from a modifier by returning the special `IMessage.Block` message:

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=modify-block)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/messages/derived/MyExtension.cs?name=modify-block)]

---

You can also return `null` from a modifier if you do not need to modify the message.