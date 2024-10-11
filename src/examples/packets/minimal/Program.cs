using Xabbo;
using Xabbo.Core;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

#if FALSE
// <using-messages>
using Xabbo.Messages.Flash;
// - or -
using Xabbo.Messages.Shockwave;
// </using-messages>
#endif

#pragma warning disable CS8321 // Local function is declared but never used

// ** NOTE **
// This is just used as a source for the code snippets to ensure that they compile successfully.
// It won't work properly if it is run as is.

var ext = new GEarthExtension();

// ...

// ** Sending packets by identifier. **

// <send-by-identifier-implicit>
ext.Send((ClientType.Flash, Direction.Out, "MoveAvatar"), 3, 4);
// </send-by-identifier-implicit>

// <send-by-identifier>
ext.Send(Out.MoveAvatar, 3, 4);
// </send-by-identifier>

// <send-multiple-values>
ext.Send(Out.GetMarketplaceOffers, 100, 2500, "duck", 1);
// </send-multiple-values>

// <send-collection>
// Assume we have a list of valid item IDs in our inventory:
var itemIds = new Id[] { 1, 2, 3 };
// We can offer all items by sending the collection:
ext.Send(Out.AddItemsToTrade, itemIds);
// </send-collection>

// <send-to-client>
// Sends an incoming Shout message to the client:
ext.Send(In.Shout, -1, "Hello, world", 0, 34, 0, 0);

// Sends an outgoing Shout message to the server:
ext.Send(Out.Shout, "Hello, world", 0);
// </send-to-client>

// <send-composer>
// Inject a Rubber Duck furni into the room client-side.
ext.Send(In.ObjectAdd,
    new FloorItem
    {
        Id = 1000,
        Kind = 179, // Rubber duck
        Location = (6, 6, 0),
        Direction = 2
    },
    "xabbo" // Owner name
);
// </send-composer>

// <send-header>
ext.Send((Direction.Out, 123), "packet data");
// </send-header>

// <intercept-single-identifier>
ext.Intercept(Out.MoveAvatar, e => {
    Console.WriteLine("Intercepted MoveAvatar");
});
// </intercept-single-identifier>

// <intercept-multiple-identifiers>
ext.Intercept([Out.Chat, Out.Shout, Out.Whisper], e => {
    Console.WriteLine("Intercepted Chat, Shout or Whisper");
    // To check which one was intercepted:
    if (e.Is(Out.Whisper))
    {
        Console.WriteLine("Intercepted Whisper");
    }
});
// </intercept-multiple-identifiers>

// ** Reading from packets. **

// <read-single-value>
ext.Intercept([Out.Chat, Out.Shout, Out.Whisper], e => {
    string message = e.Packet.Read<string>();
    Console.WriteLine($"You said: {message}.");
});
// </read-single-value>

// <read-multiple-values>
ext.Intercept(Out.MoveAvatar, e => {
    var (x, y) = e.Packet.Read<int, int>();
    Console.WriteLine($"You are walking to: {x}, {y}.");
});
// </read-multiple-values>

// <read-array>
ext.Intercept(Out.AddItemsToTrade, e => {
    var itemIds = e.Packet.Read<Id[]>();
    Console.WriteLine($"You are offering {itemIds.Length} items in trade.");
    Console.WriteLine($"Item IDs are: {string.Join(", ", itemIds)}");
});
// </read-array>

// <read-array-manual>
ext.Intercept(Out.AddItemsToTrade, e => {
    Length itemCount = e.Packet.Read<Length>();
    var itemIds = new Id[itemCount];
    for (int i = 0; i < itemCount; i++)
    {
        itemIds[i] = e.Packet.Read<Id>();
    }
});
// </read-array-manual>

// <read-parser>
ext.Intercept(In.Users, e => {
    var avatars = e.Packet.Read<Avatar[]>();
    foreach (var avatar in avatars)
        Console.WriteLine($"{avatar.Name} entered the room.");
});
// </read-parser>

// <block-packets>
ext.Intercept(Out.MoveAvatar, e => {
    // Block the packet.
    e.Block();
});
// </block-packets>

// <replace-single-value>
ext.Intercept([In.Chat, In.Shout, In.Whisper], e => {
    // Skip the avatar index.
    e.Packet.Read<int>();
    // Replace the chat message.
    e.Packet.Replace("Hello, from xabbo");
});
// </replace-single-value>

// <replace-multiple-values>
ext.Intercept([In.Chat, In.Shout, In.Whisper], e => {
    // Skip the avatar index.
    e.Packet.Read<int>();
    // Replace multiple values (of different types).
    e.Packet.Replace("Hello, from xabbo", 1, 13);
});
// </replace-multiple-values>

// <modify-single-value>
ext.Intercept([In.Chat, In.Shout, In.Whisper], e => {
    // Skip the avatar index.
    e.Packet.Read<int>();
    // Modify the chat message to all uppercase.
    e.Packet.Modify((string s) => s.ToUpper());
});
// </modify-single-value>

// <modify-multiple-values>
ext.Intercept(Out.MoveAvatar, e => {
    e.Packet.Modify(
        (int x) => x + 1,
        (int y) => y - 1
    );
});
// </modify-multiple-values>

// <receive-packet-run-task>
ext.Intercept([Out.Chat, Out.Shout], e => {
    // Read the message sent.
    string message = e.Packet.Read<string>();
    // If we send the message '/whoami':
    if (message == "/whoami")
    {
        // Block the chat message.
        e.Block();
        // Run the GetUserDataAsync task in the background.
        Task.Run(GetUserDataAsync);
    }
});
// </receive-packet-run-task>

// <receive-packet-task>
async Task GetUserDataAsync()
{
    // Request the user's data.
    Console.WriteLine("Requesting user data...");
    ext.Send(Out.InfoRetrieve);
    try
    {
        // Asynchronously receive the first UserObject packet.
        var packet = await ext.ReceiveAsync(In.UserObject);
        Console.WriteLine("Received user data.");
        // Read the user's ID and name from the packet.
        var (id, name) = packet.Read<Id, string>();
        Console.WriteLine($"Your user ID is: {id}.");
        Console.WriteLine($"Your user name is: {name}.");
    }
    catch (TimeoutException)
    {
        // If we timed out, a TimeoutException will be thrown.
        Console.WriteLine("Request timed out.");
    }
}
// </receive-packet-task>

// <configure-receive-async>
await ext.ReceiveAsync(
    // The identifier(s) to receive.
    Out.MoveAvatar,
    // The timeout in milliseconds, null will use the global default timeout. Use -1 for no timeout.
    timeout: 30000,
    // Whether to block the received packet.
    block: true,
    // A function that accepts a received packet to further inspect the packet
    // and return whether it should be captured or not.
    shouldCapture: p => p.Length > 0,
    // A cancellation token that can be used to cancel the receiver task.
    cancellationToken: default
);
// </configure-receive-async>

{
    // <select-tile-async>
    async Task<(int X, int Y)> PromptForTileAsync()
    {
        // Show a prompt in-game.
        ext.Send(In.Chat, 0, "Click a tile to select its position...", 0, 34, 0, 0);
        // Wait for the user to click a tile.
        var p = await ext.ReceiveAsync(
            // The packet identifier to capture.
            Out.MoveAvatar,
            // Don't time out.
            timeout: -1,
            // Block the captured packet.
            block: true);
        // Read and return the coordinates of the tile that was clicked.
        return p.Read<int, int>();
    }
    // </select-tile-async>

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
    ext.Send(In.Chat, -1, $"{prompt} /y or /n", 0, 34, 0, 0);
    // Wait for the user to send '/y' or '/n'.
    var p = await ext.ReceiveAsync(
        // Capture outgoing Chat and Shout packets.
        [Out.Chat, Out.Shout],
        // Don't time out.
        timeout: -1,
        // Block the captured packet.
        block: true,
        // Only capture the packet if the user said '/y' or '/n'.
        shouldCapture: p => p.Read<string>() is "/y" or "/n"
    );
    // Read the message and return true if it is '/y'.
    return p.Read<string>() == "/y";
}
// </confirm-async>

// <confirm-async-caller>
Console.WriteLine("Awaiting user confirmation...");
bool result = await ConfirmAsync("Are you sure you want to ...?");
if (result) {
    Console.WriteLine("User selected yes.");
} else {
    Console.WriteLine("User selected no.");
}
// </confirm-async-caller>
