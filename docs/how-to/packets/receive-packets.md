# How to: Receive packets

The @Xabbo.InterceptorExtensions.ReceiveAsync(Xabbo.Interceptor.IInterceptor,System.ReadOnlySpan{Xabbo.Messages.Identifier},System.Nullable{System.Int32},System.Boolean,System.Func{Xabbo.Messages.IPacket,System.Boolean},System.Threading.CancellationToken)?text=ReceiveAsync
method allows you to receive packets inline in an asynchronous method. For example, we could send a
packet to request the user's data, wait to receive the response packet, and then do something with
that packet.

## Receiving a packet

`ReceiveAsync` should be called from an async method. First, define an `async` method returning a
`Task`:

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=receive-packet-task)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=receive-packet-task)]

---

Then, when you want to call that method, use `Task.Run` to run it in the background.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=receive-packet-run-task)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=receive-packet-run-task)]

---

## Configuring the task

You can configure the receiver task by optionally providing a timeout in milliseconds,
whether the received packet should also be blocked, a predicate that returns whether
the packet should be captured, and/or a cancellation token that can be used to cancel the task.

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=configure-receive-async)]

## Capturing outgoing packets

It is also possible to use `ReceiveAsync` to *receive* outgoing packets. Simply specify an outgoing
identifier instead of an incoming one.

## Example usage

### Wait for the user to select a tile

The following example defines a method that prompts the user to select a tile,
waits for the user to select the tile and then returns its coordinates.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=select-tile-async)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=select-tile-async)]

---

The method can be reused whenever you want the user to select a tile:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=select-tile-async-caller)]

### Wait for the user to respond to a prompt

The following example defines a method that will display a confirmation message to the user,
and return whether the user confirmed by typing `/y` or `/n`.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=confirm-async)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=confirm-async)]

---

You can reuse the method with different prompts:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=confirm-async-caller)]

> [!CAUTION]
> If you try to get the received packet using methods such as `ReceiveAsync(...).Result` or
> `ReceiveAsync(...).GetAwaiter().GetResult()` inside an intercept or extension event handler, you
> will block the extension processing loop. The receiver will never capture the response packet, and
> it will always time out because the extension cannot process any more packets until the receive
> method returns.
