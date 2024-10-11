# How to: Block packets

To block a packet from reaching the client or server, call `Block` on the @Xabbo.Intercept event
arguments passed to the intercept handler:

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=block-packets)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=block-packets)]

---

Once a packet has been flagged as blocked, it cannot be reset.
