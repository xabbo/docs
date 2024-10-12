# How to: Receive messages

# Receive a message

Receiving a message is similar to [receiving a packet](~/docs/how-to/packets/receive-packets.md),
but you specify the message type you want to receive as a generic type argument to `ReceiveAsync`:

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=receive)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/messages/derived/MyExtension.cs?name=receive)]

---

## Example usage

The examples below are based on the examples shown in
[receiving packets](~/docs/how-to/packets/receive-packets.md), using messages to achieve the same
result.

### Wait for the user to select a tile

The following example defines a method that prompts the user to select a tile,
waits for the user to select the tile and then returns its coordinates.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=select-tile-async)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/messages/derived/MyExtension.cs?name=select-tile-async)]

---

The method can be reused whenever you want the user to select a tile:

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=select-tile-async-caller)]

### Wait for the user to respond to a prompt

The following example defines a method that will display a confirmation message to the user,
and return whether the user confirmed by typing `/y` or `/n`.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=confirm-async)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/messages/derived/MyExtension.cs?name=confirm-async)]

---

You can reuse the method with different prompts:

[!code-csharp[](~/src/examples/messages/minimal/Program.cs?name=confirm-async-caller)]
