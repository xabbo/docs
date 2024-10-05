namespace Examples.ExtensionTypes.Derived
{
    partial class MyExtension : global::Xabbo.Extension.IExtensionInfoInit
    {
        global::Xabbo.Extension.ExtensionInfo global::Xabbo.Extension.IExtensionInfoInit.Info => new global::Xabbo.Extension.ExtensionInfo(
            Name: "Derived",
            Description: "a derived example",
            Author: "xabbo",
            Version: "1.0"
        );
    }
}
