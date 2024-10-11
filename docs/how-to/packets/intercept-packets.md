# How to: Intercept packets

## Intercept a single packet

# [Minimal](#tab/minimal)

To intercept a packet, call `Intercept` on your extension and pass in an intercept handler callback:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=intercept-single-identifier)]

# [Derived](#tab/derived)

To intercept a packet, create an intercept handler method that returns `void` and accepts a single
@Xabbo.Intercept argument. Then add an `InterceptIn` or `InterceptOut` attribute to your method:

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=intercept-single-identifier)]

To get auto-completion you could also use the `nameof` keyword instead of a string literal:
```csharp
[InterceptOut(nameof(Out.MoveAvatar))]
```

---

The `e` parameter is an @Xabbo.Intercept that provides information about the intercepted packet.

## Intercept multiple packets

# [Minimal](#tab/minimal)

To intercept multiple packets, place multiple identifiers inside a collection expression:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=intercept-multiple-identifiers)]

# [Derived](#tab/derived)

To intercept multiple packets, pass multiple identifier names to the attribute constructor:

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=intercept-multiple-identifiers)]

You can also specify both an `InterceptIn` and `InterceptOut` attribute on the same method.

---

## Intercept on certain clients

It is possible to specify that a packet should only be intercepted on certain clients. For example,
if you specify that an intercept should target the Flash client, then the intercept handler will
have no effect when on a Shockwave or Unity session.

This allows you to handle functionality that only exists on a certain client when developing
cross-client extensions.

# [Minimal](#tab/minimal)

To intercept packets on a certain client, supply the optional `clients` argument:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=intercept-client)]

# [Derived](#tab/derived)

To intercept packets on a certain client, add an `Intercept` attribute that specifies the target
clients:

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=intercept-client)]

---

In the above example, since the `GetSelectedBadges` identifier does not exist on Shockwave,
attempting to intercept it would normally throw an @Xabbo.Messages.UnresolvedIdentifiersException
because the identifier could not be resolved to a header for that client. However, by specifying
that the intercept targets @Xabbo.ClientType.Modern clients, the intercept handler will only be
applied when on Flash or Unity.

## Intercept on multiple clients

Intercepting on multiple clients is automatically handled by the @Xabbo.Messages.MessageManager.
For example, intercepting the `MoveAvatar` message will also intercept the `MOVE` message when on a
Shockwave session. This mapping is defined in the
[message map file](https://github.com/xabbo/messages/blob/main/messages.ini). Read about xabbo
messages in the [introduction](~/docs/introduction.md#xabbomessages). However, you must handle
A lot of the parsers, composers and messages provided by @Xabbo.Core are designed to be
compatible cross-client.