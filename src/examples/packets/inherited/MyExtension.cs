using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages;
using Xabbo.Messages.Flash;
#if SHOCKWAVE
using Xabbo.Messages.Shockwave;
#endif

namespace Examples.Packets.Inherited;

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
        Send(In.Shout, 0, "Hello, world", 0, 0, 0, 0);

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
        // Replace multiple values (of different types).
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
}