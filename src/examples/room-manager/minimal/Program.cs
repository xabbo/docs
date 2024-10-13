using Xabbo.Core;
using Xabbo.Core.Game;
using Xabbo.GEarth;

// <use-room-manager>
var ext = new GEarthExtension(new GEarthOptions {
    Name = "example",
    Description = "room manager example",
    Author = "xabbo"
});

var roomManager = new RoomManager(ext);

roomManager.Entered += (e) => {
    Console.WriteLine($"You entered the room '{e.Room.Data?.Name ?? "?"}' (id: {e.Room.Id})");
};

roomManager.Left += () => {
    Console.WriteLine("You left the room");
};

ext.Run();
// </use-room-manager>

// <event-entered>
roomManager.Entered += (e) => {
    Console.WriteLine($"You entered the room '{e.Room.Data?.Name ?? "?"}' (id: {e.Room.Id})");
    Console.WriteLine("The room currently has:");
    Console.WriteLine($"* {e.Room.Avatars.Count()} avatars");
    Console.WriteLine($"* {e.Room.Furni.Count()} total furni");
    Console.WriteLine($"* {e.Room.Furni.Count(x => x.Type is ItemType.Floor)} floor items");
    Console.WriteLine($"* {e.Room.Furni.Count(x => x.Type is ItemType.Wall)} wall items");
};
// </event-entered>

// <event-left>
roomManager.Left += () => Console.WriteLine("You left the room");
// </event-left>

// <event-avatar-added>
roomManager.AvatarAdded += (e) => Console.WriteLine($"{e.Avatar.Name} entered the room.");
// </event-avatar-added>

// <event-avatar-removed>
roomManager.AvatarRemoved += (e) => Console.WriteLine($"{e.Avatar.Name} left the room.");
// </event-avatar-removed>

// <event-avatar-chat>
roomManager.AvatarChat += (e) => Console.WriteLine($"{e.Avatar.Name}: {e.Message}");
// </event-avatar-chat>

// <event-floor-item-added>
roomManager.FloorItemAdded += (e)
    => Console.WriteLine($"{e.Item.OwnerName} placed floor item #{e.Item.Id} at {e.Item.Location}");
// </event-floor-item-added>

// <event-floor-item-updated>
roomManager.FloorItemUpdated += (e) => {
    if (e.PreviousItem.Location != e.Item.Location)
    {
        // The item's location changed
        Console.WriteLine($"Floor item #{e.Item.Id} was moved from {e.PreviousItem.Location} -> {e.Item.Location}");
    }
    else if (e.PreviousItem.Direction != e.Item.Direction)
    {
        // The item's rotation changed
        Console.WriteLine($"Floor item #{e.Item.Id} was rotated from {e.PreviousItem.Direction} -> {e.Item.Direction}");
    }
};
// </event-floor-item-updated>

// <event-floor-item-removed>
roomManager.FloorItemRemoved += (e) => Console.WriteLine($"Floor item #{e.Item.Id} was removed");
// </event-floor-item-removed>