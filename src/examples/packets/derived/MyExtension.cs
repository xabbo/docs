using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages;
using Xabbo.Messages.Flash;
#if SHOCKWAVE
using Xabbo.Messages.Shockwave;
#endif

namespace Examples.Packets.Derived;

[Extension]
partial class MyExtension : GEarthExtension
{
    void Examples()
    {
        // <send-by-identifier-implicit>
        Send(new Identifier(ClientType.Flash, Direction.Out, "MoveAvatar"), 3, 4);
        // </send-by-identifier-implicit>

        // <send-by-identifier-implicit-shockwave>
        Send(new Identifier(ClientType.Shockwave, Direction.Out, "MOVE"), (short)3, (short)4);
        // </send-by-identifier-implicit-shockwave>

        // <send-by-identifier>
        Send(Out.MoveAvatar, 3, 4);
        // </send-by-identifier>

        #if SHOCKWAVE
        // <send-by-identifier-shockwave>
        Send(Out.MOVE, (short)3, (short)4);
        // </send-by-identifier-shockwave>
        #endif

        // <send-to-client>
        // Sends an incoming Shout message to the client:
        Send(In.Shout, -1, "Hello, world", 0, 34, 0, 0);

        // Sends an outgoing Shout message to the server:
        Send(Out.Shout, "Hello, world", 0);
        // </send-to-client>

        // <send-header>
        Send(new Header(Direction.Out, 123), "packet data");
        // </send-header>
    }

    // <intercept-single-identifier>
    [InterceptOut("MoveAvatar")]
    void InterceptSingleIdentifier(Intercept e)
    {
        Console.WriteLine("Intercepted MoveAvatar");
    }
    // </intercept-single-identifier>

    // <intercept-multiple-identifiers>
    [InterceptOut("Chat", "Shout", "Whisper")]
    void InterceptMultipleIdentifiers(Intercept e)
    {
        Console.WriteLine("Intercepted Chat, Shout or Whisper");
        // To check which one was intercepted:
        if (e.Is(Out.Whisper))
        {
            Console.WriteLine("Intercepted Whisper");
        }
    }
    // </intercept-multiple-identifiers>

    // <intercept-client>
    // Intercept on modern clients only (Flash or Unity)
    [Intercept(ClientType.Modern)]
    // Handle GetSelectedBadges sent when clicking on a user in the room.
    [InterceptOut("GetSelectedBadges")]
    void HandleGetSelectedBadges(Intercept e)
    {
        var userId = e.Packet.Read<Id>();
        Console.WriteLine($"You clicked on a user with ID: {userId}.");
    }
    // </intercept-client>

    // <block-packets>
    [InterceptOut("MoveAvatar")]
    void BlockPackets(Intercept e)
    {
        // Block the packet.
        e.Block();
    }
    // </block-packets>

    // <replace-single-value>
    [InterceptIn("Chat", "Shout", "Whisper")]
    void ReplaceSingleValue(Intercept e)
    {
        // Skip the avatar index.
        e.Packet.Read<int>();
        // Replace the chat message.
        e.Packet.Replace("Hello, from xabbo");
    }
    // </replace-single-value>

    // <replace-multiple-values>
    [InterceptIn("Chat", "Shout", "Whisper")]
    void ReplaceMultipleValues(Intercept e)
    {
        // Skip the avatar index.
        e.Packet.Read<int>();
        // Replace multiple values.
        e.Packet.Replace("Hello, from xabbo", 1, 13);
    }
    // </replace-multiple-values>

    // <modify-single-value>
    [InterceptIn("Chat", "Shout", "Whisper")]
    void ModifySingleValue(Intercept e)
    {
        // Skip the avatar index.
        e.Packet.Read<int>();
        // Modify the chat message to all uppercase.
        e.Packet.Modify((string s) => s.ToUpper());
    }
    // </modify-single-value>

    // <modify-multiple-values>
    [InterceptOut("MoveAvatar")]
    void ModifyMultipleValues(Intercept e)
    {
        e.Packet.Modify(
            (int x) => x + 1,
            (int y) => y - 1
        );
    }
    // </modify-multiple-values>

    // <receive-packet-run-task>
    [InterceptOut("Chat", "Shout")]
    void HandleCommand(Intercept e)
    {
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
    }
    // </receive-packet-run-task>

    // <receive-packet-task>
    async Task GetUserDataAsync()
    {
        // Request the user's data.
        Console.WriteLine("Requesting user data...");
        Send(Out.InfoRetrieve);
        try
        {
            // Asynchronously receive the first UserObject packet.
            var packet = await ReceiveAsync(In.UserObject);
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

    // <select-tile-async>
    async Task<(int X, int Y)> PromptForTileAsync()
    {
        // Show a prompt in-game.
        Send(In.Chat, 0, "Click a tile to select its position...", 0, 34, 0, 0);
        // Wait for the user to click a tile.
        var p = await ReceiveAsync(
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

    // <confirm-async>
    async Task<bool> ConfirmAsync(string prompt)
    {
        // Show the prompt in-game.
        Send(In.Chat, -1, $"{prompt} /y or /n", 0, 34, 0, 0);
        // Wait for the user to send '/y' or '/n'.
        var p = await ReceiveAsync(
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
}