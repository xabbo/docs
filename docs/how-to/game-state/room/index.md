# Room Manager

The @Xabbo.Core.Game.RoomManager tracks the state of the current room, allowing you to access
information about the room, its name, description, owner, the user's permissions in the room,
its avatars and furni, etc. It also provides many events that you can subscribe to, such as when
the user enters or leaves the room, the room data is updated, an avatar or furni is added, updated
or removed from the room, and many more.

To use the room manager, create an instance of it and pass your extension to its constructor. The
room manager will then intercept and parse packets to keep track of the room state and fire events
when they occur. The room manager works on both Flash and Shockwave clients, although certain events
will not fire on Shockwave if that functionality does not exist.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/room-manager/minimal/Program.cs?name=use-room-manager)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/room-manager/derived/MyExtension.cs?name=use-room-manager)]

---
