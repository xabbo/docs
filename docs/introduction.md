# Introduction

This website provides documentation for the C# xabbo libraries.
Below is an overview of the various libraries.

Head over to [Getting started](~/docs/getting-started.md) to learn how to create G-Earth extensions with xabbo.

## Xabbo.Common

This library contains common functionality that is shared across all of the xabbo libraries.

## Xabbo.GEarth

This library contains the @Xabbo.GEarth.GEarthExtension implementation that interfaces with G-Earth, allowing developers to create G-Earth extensions utilizing the xabbo libraries.

## Xabbo.Messages

This library provides access to named message identifiers for different clients.
The repository also provides a message map file, allowing xabbo to associate message names on different clients with each other.
For example, the incoming Flash @Xabbo.Messages.Flash.Out.MoveAvatar identifier is equivalent to Shockwave's @Xabbo.Messages.Shockwave.Out.MOVE identifier.
The message map ties these identifiers together using an entry like the following:
```txt
us:Move f:MoveAvatar
```
Each of the Unity, Flash and Shockwave clients is represented by the characters `u`, `f`, and `s`. The above entry means that the Unity and Shockwave `Move` message is equivalent to the Flash `MoveAvatar` message.
This means that if you send or intercept the Flash @Xabbo.Messages.Flash.Out.MoveAvatar identifier on a Shockwave session, it is mapped to the header for Shockwave's @Xabbo.Messages.Shockwave.Out.MOVE message. This makes it easier to develop extensions that work across different clients.

For more details on the message map format specification, see the [repository](https://github.com/xabbo/messages).

## Xabbo.Core

This library contains various data structures, parsers, composers, messages, game data and game state management for xabbo based extensions.
It provides a high-level API aimed at simplifying the creation of advanced extensions.
