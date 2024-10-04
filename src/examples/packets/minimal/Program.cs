using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;
#if FALSE
// - or -
using Xabbo.Messages.Shockwave;
#endif

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

// <send-to-client>
// Sends an incoming Shout message to the client:
ext.Send(In.Shout, 0, "Hello, world", 0, 0, 0, 0);

// Sends an outgoing Shout message to the server:
ext.Send(Out.Shout, "Hello, world", 0);
// </send-to-client>

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
