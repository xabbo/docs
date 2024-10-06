# Parsers and composers

Parsers and composers allow you to read and write complex objects to/from packets.

@Xabbo.Core provides many of these that you can use in your own extensions.

These articles detail how you can create your own parsers and composers.

## Parsers

Parsers are objects that can be read from a packet.
They implement the @Xabbo.Messages.IParser`1 interface, which contains a single method:

```csharp
public interface IParser<T> where T : IParser<T>
{
    static abstract T Parse(in PacketReader p);
}
```

The @Xabbo.Messages.PacketReader can read single *packet primitive* values via methods such as
@Xabbo.Messages.PacketReader.ReadInt?text=ReadInt and
@Xabbo.Messages.PacketReader.ReadString?text=ReadString, arrays of primivites e.g.
@Xabbo.Messages.PacketReader.ReadStringArray?text=ReadStringArray, and parsers via the
@Xabbo.Messages.PacketReader.Parse``1?text=Parse method.

## Composers

Composers are objects that can be written to a packet.
They implement the @Xabbo.Messages.IComposer interface, which contains a single method:

```csharp
public interface IComposer
{
    void Compose(in PacketWriter p);
}
```

Like the reader, the @Xabbo.Messages.PacketWriter can write single *packet primitive* values via
methods such as @Xabbo.Messages.PacketWriter.WriteInt(System.Int32)?text=WriteInt and
@Xabbo.Messages.PacketWriter.WriteString(System.String)?text=WriteString, arrays of primivites e.g.
@Xabbo.Messages.PacketWriter.WriteStringArray(System.Collections.Generic.IEnumerable{System.String})?text=WriteStringArray,
and composers via the @Xabbo.Messages.IComposer.Compose(Xabbo.Messages.PacketWriter@)?text=Compose
method.

## Combined parser and composer

By implementing @Xabbo.Messages.IParserComposer`1 which inherits
both @Xabbo.Messages.IParser`1 and @Xabbo.Messages.IComposer, we can create
our own classes that can be read from and written to a packet.

A class that implements @Xabbo.Messages.IParserComposer`1 must define both the
@Xabbo.Messages.PacketReader.Parse``1?text=Parse and
@Xabbo.Messages.IComposer.Compose(Xabbo.Messages.PacketWriter@)?text=Compose methods discussed above.
