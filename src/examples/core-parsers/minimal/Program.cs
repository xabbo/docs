using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;
using Xabbo.Core;

var ext = new GEarthExtension(new GEarthOptions {
    Name = "How to",
    Description = "use core parsers",
    Author = "xabbo"
});

// <parsers>
ext.Intercept(In.Users, e => {
    // Parsing an array of Avatars.
    var avatars = e.Packet.Read<Avatar[]>();
    foreach (var avatar in avatars)
    {
        // You can now access avatar.Id, avatar.Name, avatar.Location, etc.
        Console.WriteLine(avatar.Name);
    }
});
// </parsers>

ext.Activated += () =>
{
    // <composers>
    // Injecting a client-side user into the room.
    Id id = 1000; int index = 1000;
    ext.Send(In.Users, new Avatar[] {
        new User(id, index)
        {
            Name = "xabbo",
            Motto = "hello from xabbo/core",
            Location = (6, 6, 0),
            Figure = "hr-3090-42.hd-180-1.ch-3110-64-1408.lg-275-64.ha-1003-64",
            Gender = Gender.Male
        }
    });
    // </composers>
};

// <parser-composers>
ext.Intercept(In.Users, e =>
{
    // Read the number of avatars in the packet.
    int n = e.Packet.Read<Length>();
    // Loop through each avatar.
    for (int i = 0; i < n; i++)
    {
        // Modify each avatar.
        e.Packet.Modify<Avatar>(avatar => {
            // If the avatar is a user...
            if (avatar is User user)
            {
                // Replace the avatar's name, figure and gender.
                user.Name = "xabbo";
                user.Figure = "hr-3090-42.hd-180-1.ch-3110-64-1408.lg-275-64.ha-1003-64";
                user.Gender = Gender.Male;
            }
            // Return the modified avatar.
            return avatar;
        });
    }
});
// </parser-composers>

ext.Run();