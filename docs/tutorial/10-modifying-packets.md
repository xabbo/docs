# 10. Modifying packets

Being able to intercept packets also allows us to modify the contents of those packets.

Let's replace the contents of all incoming chat messages. First, take another look at an incoming chat message:

```txt
[Chat]
Incoming[1064] -> [0][0][0][29][4]([0][0][0][8][0][5]hello[0][0][0][0][0][0][0][0][0][0][0][0][0][0][0][0]
{in:Chat}{i:8}{s:"hello"}{i:0}{i:0}{i:0}{i:0}
```

We have an integer, followed by a string with our message contents, and 4 more integers. The first integer represents the avatar index, which tells our game client which avatar in the room sent the chat message. We won't need to worry about the following integers - we can modify what we want and leave everything else unchanged.

Add the following intercept to your code:

```csharp
ext.Intercept(In.Chat, e => {
    // Read the avatar index.
    e.Packet.Read<int>();
    // Replace the message.
    e.Packet.Replace("hello from xabbo");
});
```

First, we read the avatar index. This allows us to move the packet's position past the integer to the beginning of the string we want to modify. We then pass a string into `Replace`, which replaces a string from the current position in the packet with the specified string.

> [!TIP]
> If we know that integers on Flash are 4 bytes in length, we could omit reading the avatar index and use `e.Packet.ReplaceAt(4, "hello from xabbo")` instead. This replaces a string starting after the 4th byte in the packet. However, the length of a certain data type may be different on other clients. On Shockwave, integers are written as @Xabbo.Messages.VL64 which have a variable byte length. Using `Read<int>` allows us to skip over the integer without worrying about the length on different clients.

When we run the extension we should see all incoming chat messages replaced with `"hello from xabbo"`:

![Modified chat messages](~/images/tutorial/10-1.png)

This only modifies Chat messages, but if we wanted to modify Shout and Whisper messages as well, we can intercept all three by passing multiple identifiers inside a collection expression `[...]`.

```csharp
// Intercept incoming Chat, Shout and Whisper packets.
ext.Intercept([In.Chat, In.Shout, In.Whisper], e => {
    // Read the avatar index.
    e.Packet.Read<int>();
    // Replace the message.
    e.Packet.Replace("hello from xabbo");
});
```

As all three packets share the same structure, we can apply the same replacement logic.

What if we wanted to read the string and apply a transformation, then replace it? For example, to change chat messages to uppercase. We could save the position of the packet, read the string, transform it, revert the packet's position back to the start of the string and then replace it. Which would look like this:

```csharp
ext.Intercept([In.Chat, In.Shout, In.Whisper], e => {
    // Read the avatar index.
    e.Packet.Read<int>();
    // Save the current packet position.
    int start = e.Packet.Position;
    // Read the message string.
    string message = e.Packet.Read<string>();
    // Restore the packet position.
    e.Packet.Position = start;
    // Replace a string with the uppercase message.
    e.Packet.Replace(message.ToUpper());
});
```

However, xabbo provides a method that does this. By using `Modify`, we can pass in a modifier function to read, transform and replace a value in-place. Change your intercept to the following:

```csharp
ext.Intercept([In.Chat, In.Shout, In.Whisper], e => {
    // Read the avatar index.
    e.Packet.Read<int>();
    // Change the message to uppercase.
    e.Packet.Modify((string s) => s.ToUpper());
});
```

Note that when you `Modify` a value, the packet's position is advanced past the value the same as if we `Read` the value.

Now when you run your extension, all chat messages should be transformed to uppercase:

![Chat messages modified to uppercase](~/images/tutorial/10-2.png)