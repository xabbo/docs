# Create a model

To create a parser or composer, we must first define a model containing
all of the properties of the object we want to create a parser or composer for.

## Analyze the packet structure

As an example, let's create a model for a wall item when it is placed in the room.
Take a look at the packet in G-Earth's packet logger when you place a wall item:

```txt
[ItemAdd]
Incoming[3171] -> [0][0][0]:[12]c[0][10]2147418112[0][0][17]+[0][15]:w=9,1 l=0,50 r[0][0]ÿÿÿÿ[0][0][0][0][2]ø¢[132][0][5]xabbo[10]
{in:ItemAdd}{s:"2147418112"}{i:4395}{s:":w=9,1 l=0,50 r"}{s:""}{i:-1}{i:0}{i:49848964}{s:"xabbo"}
```

We can see that we have several fields in the packet expression. Here is what they are:

| Expression              | Type     | Description
| ----------------------- | -------- | -----------
| `{s:"2147418112"}`      | `string` | Unique ID of the item.
| `{i:4395}`              | `int`    | Furni kind (furni type ID). In this case, `4395` is the "Crazed Rorshach Inkblot".
| `{s:":w=9,1 l=0,50 r"}` | `string` | Wall location.
| `{s:""}`                | `string` | Item data. This could specify a poster variant or other information.
| `{i:-1}`                | `int`    | Seconds until expiration.
| `{i:0}`                 | `int`    | Usage policy. This specifies whether everyone, users with rights, or only the owner can use the furni.
| `{i:49848964}`          | `int`    | Owner ID.
| `{s:"xabbo"}`           | `string` | Owner name.

A parser should read each of these fields and store them into the properties of an object.\
A composer must write each of these fields in their correct order and format to construct a valid packet.

## Define the model

Define a class with the relevant properties and types from the packet structure.
For the `Id` and `OwnerId` we are using the @Xabbo.Id type from `Xabbo.Common`. It is good
practice to use this type for all IDs as it adjusts for different clients.

[!code-csharp[](~/src/examples/parser-composer/WallItem.cs?range=13,19,21-28,68)]

> [!NOTE]
> Although the the item ID in the packet is a `string`, we have defined it in our model with the type `Id`. This makes the model easier to with as it is a numeric type, and also makes it consistent with `OwnerId`. We will handle converting the `Id` to/from a `string` in our parser/composer implementation.

Using this model as a base, we can continue on to [creating a parser](create-a-parser.md) or [composer](create-a-composer.md).