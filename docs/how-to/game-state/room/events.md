# Room Manager: Events

The following are just some examples of the events provided by the room manager. For a full list of
events, check the @Xabbo.Core.Game.RoomManager#events documentation page.

## Entered room

Upon entering a room, the event arguments provide the @Xabbo.Core.Game.IRoom instance, where you
have access to the room's @Xabbo.Core.RoomData?text=Data, @Xabbo.Core.Avatar?text=Avatars,
@Xabbo.Core.Furni, and other information.

The room data may be `null`, but it is usually loaded by the client before entering the room.

[!code-csharp[](~/src/examples/room-manager/minimal/Program.cs?name=event-entered)]

## Left room

Occurs when the user leaves the room.

[!code-csharp[](~/src/examples/room-manager/minimal/Program.cs?name=event-left)]

## Avatar added

Occurs when avatars are loaded or an avatar enters the room.

[!code-csharp[](~/src/examples/room-manager/minimal/Program.cs?name=event-avatar-added)]

## Avatar removed

Occurs when an avatar is removed from the room.

[!code-csharp[](~/src/examples/room-manager/minimal/Program.cs?name=event-avatar-removed)]

## Avatar chat

Occurs when an avatar sends a chat message.

[!code-csharp[](~/src/examples/room-manager/minimal/Program.cs?name=event-avatar-chat)]

## Floor item added

Occurs when a floor item is added to the room.

[!code-csharp[](~/src/examples/room-manager/minimal/Program.cs?name=event-floor-item-added)]

## Floor item updated

Occurs when a floor item is moved or rotated.

[!code-csharp[](~/src/examples/room-manager/minimal/Program.cs?name=event-floor-item-updated)]

## Floor item removed

Occurs when a floor item is removed from the room.

[!code-csharp[](~/src/examples/room-manager/minimal/Program.cs?name=event-floor-item-removed)]
