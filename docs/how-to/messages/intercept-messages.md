# How to: Intercept messages

## Intercept incoming messages

# [Minimal](#tab/minimal)

To intercept a message, specify the message type as a generic type argument to `Intercept`.

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=intercept-in)]

# [Derived](#tab/derived)

To intercept a message, add an `Intercept` attribute to a method that accepts an instance of an
@Xabbo.Messages.IMessage`1. The message type specified in the method argument is what will be
intercepted.

[!code-csharp[](~/src/examples/messages/derived/MyExtension.cs?name=intercept-in)]

---

## Intercept outgoing messages

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=intercept-out)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/messages/derived/MyExtension.cs?name=intercept-out)]

---

> [!NOTE]
> If a message does not support a certain client and an intercept is registered for that client,
> it will simply be ignored, and will have no effect. Therefore, it is safe to intercept these
> messages when developing cross-client extensions. All messages in `Xabbo.Core` are documented with
> their supported clients and the identifiers they use.