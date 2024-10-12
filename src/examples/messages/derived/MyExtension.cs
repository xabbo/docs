using Xabbo;
using Xabbo.Core;
using Xabbo.Core.Messages.Incoming;
using Xabbo.Core.Messages.Outgoing;
using Xabbo.GEarth;
using Xabbo.Messages;
using Xabbo.Messages.Flash;
#if SHOCKWAVE
using Xabbo.Messages.Shockwave;
#endif

namespace Examples.Messages.Derived;

[Extension]
partial class MyExtension : GEarthExtension
{
    // <intercept-in>
    // Log floor items added to the room.
    [Intercept]
    void InterceptFloorItemAdded(FloorItemAddedMsg msg)
        => Console.WriteLine($"{msg.Item.OwnerName} placed floor item #{msg.Item.Id} at {msg.Item.X}, {msg.Item.Y}");

    // Log floor items removed from the room.
    [Intercept]
    void InterceptFloorItemRemoved(FloorItemRemovedMsg msg)
        => Console.WriteLine($"Floor item #{msg.Id} was removed");

    // Log avatars added to the room.
    [Intercept]
    void InterceptAvatarsAdded(AvatarsAddedMsg avatars)
    {
        foreach (var avatar in avatars)
            Console.WriteLine($"Avatar '{avatar.Name}' added to the room.");
    }
    // </intercept-in>

    // <intercept-out>
    // Intercept outgoing walk messages, logging the coordinates of tiles when they are clicked.
    [Intercept]
    void InterceptWalk(WalkMsg walk) => Console.WriteLine($"You clicked the tile at {walk.Point}.");

    // Intercept and log outgoing Talk, Shout and Whisper messages.
    [Intercept]
    void InterceptChat(ChatMsg chat) => Console.WriteLine($"You said: {chat.Message}");

    // Intercept and log outgoing move floor item messages.
    [Intercept]
    void InterceptMoveFloorItem(MoveFloorItemMsg move)
        => Console.WriteLine($"You are moving floor item #{move.Id} to {move.Location}");

    // Intercept and log outgoing pickup item messages.
    [Intercept]
    void InterceptPickupFurni(PickupFurniMsg pickup)
        => Console.WriteLine($"You are picking up {pickup.Type} item #{pickup.Id}");
    // </intercept-out>

    // <block-generic-intercept>
    // Block walk messages and look towards the tile clicked.
    [Intercept]
    void BlockWalk(Intercept<WalkMsg> e)
    {
        e.Block();
        Send(new LookToMsg(e.Msg.Point));
    }
    // </block-generic-intercept>

    // <block-non-generic-intercept>
    // Block incoming chat messages if they contain the word 'block'.
    [Intercept]
    void BlockChat(Intercept e, AvatarChatMsg chat)
    {
        if (chat.Message.Contains("block"))
            e.Block();
    }
    // </block-non-generic-intercept>

    // <modify>
    // Modify the figure and gender of all avatars added to the room.
    [Intercept]
    IMessage? ModifyAvatars(AvatarsAddedMsg avatars)
    {
        // Loop over each user.
        foreach (var avatar in avatars.OfType<User>()) {
            // Update the user's figure and gender.
            avatar.Figure = "hr-3090-42.hd-180-1.ch-3110-64-1408.lg-275-64.ha-1003-64";
            avatar.Gender = Gender.Male;
        }
        // Return the modified AvatarsAddedMsg.
        return avatars;
    }
    // </modify>

    // <modify-block>
    [Intercept]
    IMessage? ModifyOrBlockChat(AvatarChatMsg chat)
    {
        if (chat.Message.Contains("block"))
        {
            // Block messages that contain the word "block".
            return IMessage.Block;
        }
        else
        {
            // Otherwise modify the chat message to uppercase.
            return chat with { Message = chat.Message.ToUpper() };
        }
    }
    // </modify-block>

    async Task ReceiveMessageAsync()
    {
    // <receive>
        try
        {
            Console.WriteLine("Requesting user data...");
            // Send a GetUserDataMsg.
            Send(new GetUserDataMsg());
            // Receive the UserDataMsg response.
            var userDataMsg = await ReceiveAsync<UserDataMsg>(block: true);
            // Print the user's data to the console.
            Console.WriteLine($"Your ID is: {userDataMsg.UserData.Id}");
            Console.WriteLine($"Your name is: {userDataMsg.UserData.Name}");
        }
        catch (TimeoutException)
        {
            Console.WriteLine("Request timed out.");
        }
    // </receive>
    }

    // <select-tile-async>
    async Task<Point> PromptForTileAsync()
    {
        // Show a prompt in-game.
        Send(new AvatarWhisperMsg("Click a tile to select its position...", BubbleStyle: 34));
        // Wait for the user to click a tile.
        var walk = await ReceiveAsync<WalkMsg>(timeout: -1, block: true);
        // Return the coordinates of the tile that was clicked.
        return walk.Point;
    }
    // </select-tile-async>

    async Task PromptForTileAsyncCaller()
    {
        // <select-tile-async-caller>
        // Wait for the user to select a tile.
        Console.WriteLine("Prompting user to select a tile...");
        var selectedTile = await PromptForTileAsync();
        // Print the selected tile coordinates.
        Console.WriteLine($"User selected: {selectedTile}");
        // </select-tile-async-caller>
    }

    // <confirm-async>
    async Task<bool> ConfirmAsync(string prompt)
    {
        // Show the prompt in-game.
        Send(new AvatarTalkMsg($"{prompt} /y or /n", BubbleStyle: 34));
        // Wait for the user to send '/y' or '/n'.
        var chat = await ReceiveAsync<ChatMsg>(
            // Don't time out.
            timeout: -1,
            // Block the captured packet.
            block: true,
            // Only capture the packet if the user said '/y' or '/n'.
            shouldCapture: chat => chat.Message is "/y" or "/n"
        );
        // Read the message and return true if it is '/y'.
        return chat.Message == "/y";
    }
    // </confirm-async>

    async Task ConfirmAsyncCaller()
    {
        // <confirm-async-caller>
        Console.WriteLine("Awaiting user confirmation...");
        bool result = await ConfirmAsync("Are you sure you want to ...?");
        if (result) {
            Console.WriteLine("User selected yes.");
        } else {
            Console.WriteLine("User selected no.");
        }
        // </confirm-async-caller>
    }

    async Task RequestMessageAsync()
    {
        // <request>
        try
        {
            Console.WriteLine("Requesting user data...");
            // Sends a GetUserDataMsg request, receives the UserDataMsg response,
            // then extracts the resulting UserData and returns it.
            var userData = await RequestAsync(new GetUserDataMsg());
            // Now we have direct access to the UserData object,
            // we can print the user's data to the console.
            Console.WriteLine($"Your ID is: {userData.Id}");
            Console.WriteLine($"Your name is: {userData.Name}");
        }
        catch (TimeoutException)
        {
            Console.WriteLine("Request timed out.");
        }
        // </request>
    }
}