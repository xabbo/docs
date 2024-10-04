using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Shockwave;
#if FALSE
// - or -
using Xabbo.Messages.Flash;
#endif

// ** NOTE **
// This is just used as a source for the code snippets to ensure that they compile successfully.
// It won't work properly if it is run as is.

var ext = new GEarthExtension();

// ...

// ** Sending packets by identifier. **

// <send-by-identifier-implicit>
ext.Send((ClientType.Shockwave, Direction.Out, "MOVE"), (short)3, (short)4);
// </send-by-identifier-implicit>

// <send-by-identifier>
ext.Send(Out.MOVE, (short)3, (short)4);
// </send-by-identifier>

// ** Sending messages to the client. **

// Sends an incoming Shout message to the client:
ext.Send(In.CHAT_3, 0, "Hello, world");

// Sends an outgoing Shout message to the server:
ext.Send(Out.SHOUT, "Hello, world", 0);

// ** Sending packets by header. **
ext.Send((Direction.Out, 123), "packet data");

// ** Intercepting a single packet. **

ext.Intercept(Out.MOVE, e => {
    Console.WriteLine("Intercepted MOVE");
});

// ** Intercepting multiple packets. **

ext.Intercept([Out.CHAT, Out.SHOUT, Out.WHISPER], e => {
    Console.WriteLine("Intercepted Chat, Shout or Whisper");
    // To check which one was intercepted:
    if (e.Is(Out.WHISPER))
    {
        Console.WriteLine("Intercepted Whisper");
    }
});

// ** Blocking packets. **

ext.Intercept(Out.MOVE, e => {
    // Block the packet.
    e.Block();
});

// ** Replacing packet values. **

ext.Intercept([In.CHAT, In.CHAT_2, In.CHAT_3], e => {
    // Skip the avatar index.
    e.Packet.Read<int>();
    // Replace the chat message.
    e.Packet.Replace("Hello, from xabbo");
});

// ** Modifying packets. **

ext.Intercept([In.CHAT, In.CHAT_2, In.CHAT_3], e => {
    // Skip the avatar index.
    e.Packet.Read<int>();
    // Modify the chat message to all uppercase.
    e.Packet.Modify((string s) => s.ToUpper());
});

ext.Intercept(Out.MOVE, e => {
    e.Packet.Modify(
        (short x) => (short)(x + 1),
        (short y) => (short)(y - 1)
    );
});

static short AddOne(short n) => (short)(n + 1);

ext.Intercept(Out.MOVE, e => {
    e.Packet.Modify<short>(AddOne);
});
