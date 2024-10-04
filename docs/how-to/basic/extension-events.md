# How to: Handle extension events

## Initialized

Occurs when your extension is initialized by G-Earth.

Accepts an @Xabbo.InitializedEventArgs.

Packet information is **not** yet available, so attempting to send a packet (by identifier) here will fail.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/extension-events/minimal/Program.cs?range=5-8)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/extension-events/derived/MyExtension.cs?range=6-8,9-15,45)]

---

## Connected

Occurs when the game is connected.

Accepts a @Xabbo.ConnectedEventArgs which provides information about the game host and port,
whether the connection was already established before the extension was initialized,
and the @Xabbo.Session containing the current @Xabbo.Hotel and @Xabbo.Client.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/extension-events/minimal/Program.cs#L10-L13)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/extension-events/derived/MyExtension.cs?range=6-8,17-23,45)]

---

## Activated

Occurs when the user clicks the green play button next to your extension in G-Earth.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/extension-events/minimal/Program.cs#L15-L17)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/extension-events/derived/MyExtension.cs?range=6-8,25-30,45)]

---

## Intercepted

Occurs when a packet is intercepted.

Accepts an @Xabbo.Intercept.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/extension-events/minimal/Program.cs#L19-L21)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/extension-events/derived/MyExtension.cs?range=6-8,32-37,45)]

---

## Disconnected

Occurs when the game disconnects.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/extension-events/minimal/Program.cs#L23-L25)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/extension-events/derived/MyExtension.cs?range=6-8,39-44,45)]

---