# 9. Blocking packets

Intercepting packets allows us to block outgoing packets from being sent to the server, and block incoming packets from reaching the client. To block a packet, call `Block()` on the intercept event arguments:

```csharp
ext.Intercept(Out.MoveAvatar, e => {
    // Block the walk packet.
    e.Block();
});
```

The above code will prevent you from walking, but let's add some additional behaviour. We will make our avatar face in the direction of the tile we click. Notice that when you click on another user in the room, you will face their direction. Take a look at which packets are sent when you click on your own avatar. You should see the following `LookTo` packet:

```txt
[LookTo]
Outgoing[2403] -> [0][0][0][10][9]c[0][0][0][8][0][0][0][8]
{out:LookTo}{i:8}{i:8}
```

Just like the walk packet, it contains the X and Y coordinates of a tile, in this case the coordinates of a tile to look towards.
Now, let's send this packet using the coordinates from our walk packet:

```csharp
ext.Intercept(Out.MoveAvatar, e => {
    // Block the walk packet.
    e.Block();
    // Read the tile coordinates.
    var (x, y) = e.Packet.Read<int, int>();
    // Send the LookTo packet with the tile coordinates.
    ext.Send(Out.LookTo, x, y);
});
```

This will make your avatar look towards the clicked tile:

![Click to turn](~/images/tutorial/9-1.gif)

Notice that the `MoveAvatar` and `LookTo` packets have the exact same structure.
They both accept the X and Y coordinates of a tile as integers.
In this specific case, since the packet structure is the same,
instead of blocking the walk packet and then sending a look-to packet,
we can simply replace the header for `MoveAvatar` with the header for `LookTo`, and the result is the same:

```csharp
ext.Intercept(Out.MoveAvatar, e => {
    // Replace the packet's header with the resolved header for the LookTo identifier.
    e.Packet.Header = ext.Messages.Resolve(Out.LookTo);
});
```
