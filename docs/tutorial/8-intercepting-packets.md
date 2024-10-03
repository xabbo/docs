# 8. Intercepting packets

Now, let's look into how we can intercept packets. Intercepting packets allows us to perform an action whenever a packet is sent or received, modify the contents of packets, or block packets from being sent to the client or server.

We will make our avatar shout out the coordinates of the tile we are walking to.\
First, make your avatar walk to a tile and check the packet logger output.\
You should see a packet like the following:

```txt
[MoveAvatar]
Outgoing[3741] -> [0][0][0][10][14][157][0][0][0][9][0][0][0][10]
{out:MoveAvatar}{i:9}{i:10}
```

Here we can see two integers, `9` and `10`. Try clicking on different tiles to see how these values change. They represent the `X` and `Y` coordinates of the tile we want to walk to.

To intercept this packet, call `Intercept` on the extension and pass in the message identifier `Out.MoveAvatar`, followed by an intercept handler callback.
The handler you provide will be executed each time a packet matching the specified identifier is intercepted.

```csharp
// Intercept outgoing MoveAvatar packets.
ext.Intercept(Out.MoveAvatar, e => Console.WriteLine("Intercepted MoveAvatar"));
```

With the above, you should see "Intercepted MoveAvatar" output to the console each time you click a tile to walk. The `e` parameter is an @Xabbo.Intercept which provides data about the intercepted packet.

Next, to read the `X` and `Y` coordinate integers from the packet, call `Read<int>()` on the packet:

```csharp
ext.Intercept(Out.MoveAvatar, e => {
    // Read the X and Y coordinates.
    int x = e.Packet.Read<int>();
    int y = e.Packet.Read<int>();
    // Print the coordinates to the console.
    Console.WriteLine($"Walking to {x}, {y}");
});
```

<details class="TIP alert alert-info">
<summary><h5 class="d-inline">Packet reading basics</h5></summary>

The packet keeps track of a read position, so when we read the first integer, the position is advanced to the next byte after that integer. Looking at the data of our packet in bytes:

```txt
First integer
v----------v
[0][0][0][9][0][0][0][10]
 ^          ^-----------^
 |          Second integer
Position (0)
```

Each integer takes up 4 bytes, so reading an integer advances the position by 4. After we read the first integer, our position will point at the first byte of the second integer:

```txt
            Second integer
            v-----------v
[0][0][0][9][0][0][0][10]
             ^
             |
            Position (4)
```

Note that the position points at the 5th byte because the index is zero-based (the first byte is at 0).
After reading the second integer, our position will then point after the last byte:

```txt
            Second integer
            v-----------v
[0][0][0][9][0][0][0][10]
^----------^              ^
First integer             |
                         Position (8)
```

Since there is no more data available, attempting to read any more values will throw an error.
</details>

You should see the coordinates printed to the console each time you click a tile to walk.

We can also read multiple values by passing multiple types into `Read<...>()`:

```csharp
ext.Intercept(Out.MoveAvatar, e => {
    // Read the X and Y coordinates.
    var (x, y) = e.Packet.Read<int, int>();
    // Print the coordinates to the console.
    Console.WriteLine($"Walking to {x}, {y}");
});
```

Now, let's make our avatar shout out these coordinate when we walk.\
As with previous packets, capture the shout packet in the logger.

Note that if you also have "View incoming" enabled in the packet logger, you will see both outgoing and incoming Shout packets like below:

```txt
[Shout]
Outgoing[322] -> [0][0][0][13][1]B[0][5]hello[0][0][0][0]
{out:Shout}{s:"hello"}{i:0}
----------------
[Shout]
Incoming[20] -> [0][0][0][29][0][20][0][0][0][0][0][5]hello[0][0][0][0][0][0][0][0][0][0][0][0]ÿÿÿÿ
{in:Shout}{i:0}{s:"hello"}{i:0}{i:0}{i:0}{i:-1}
```

The outgoing packet tells the server that you want to shout a message,
and the incoming packet tells your game client which avatar in the room is shouting, the contents of the message, and some extra information.

Let's focus on the outgoing packet for now.

```txt
[Shout]
Outgoing[322] -> [0][0][0][13][1]B[0][5]hello[0][0][0][0]
{out:Shout}{s:"hello"}{i:0}
```

We see a new packet expression type `s:` which defines a string of characters - in this case `"hello"`, and an integer `0`. You might not know what the integer `0` means yet, but it is important to include it so that the packet is structured correctly.
Change your `Console.WriteLine` to call `ext.Send` with the `Out.Shout` identifier, followed by the message string and the integer `0`:

```csharp
ext.Intercept(Out.MoveAvatar, e => {
    // Read the X and Y coordinates.
    var (x, y) = e.Packet.Read<int, int>();
    // Send a shout packet with the coordinates.
    ext.Send(Out.Shout, $"Walking to {x}, {y}", 0);
});
```

Now, whenever we walk, we will also shout the coordinates we are walking to.

> [!TIP]
> Try changing the integer to `3` and see what happens.

