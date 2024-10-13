using Xabbo;
using Xabbo.Core.Events;
using Xabbo.Core.Game;
using Xabbo.GEarth;

namespace Examples.RoomMgr.Derived;

// <use-room-manager>
[Extension(
    Name = "example",
    Description = "room manager example",
    Author = "xabbo"
)]
partial class MyExtension : GEarthExtension
{
    private readonly RoomManager _roomManager;

    public MyExtension()
    {
        _roomManager = new RoomManager(this);
        _roomManager.Entered += OnEnteredRoom;
        _roomManager.Left += OnLeftRoom;
    }

    private void OnEnteredRoom(RoomEventArgs e)
    {
        Console.WriteLine($"You entered the room '{e.Room.Data?.Name ?? "?"}' (id: {e.Room.Id})");
    }

    private void OnLeftRoom()
    {
        Console.WriteLine("You left the room");
    }
}
// </use-room-manager>