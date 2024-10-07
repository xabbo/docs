using System.Collections.Immutable;
using System.Composition;
using System.Text;
using Docfx.Common;
using Docfx.Plugins;
using HtmlAgilityPack;

namespace Xabbo.Docs.Processors;

[Export(nameof(Rewrite404), typeof(IPostProcessor))]
public class Rewrite404 : IPostProcessor
{
    public ImmutableDictionary<string, object> PrepareMetadata(ImmutableDictionary<string, object> metadata)
    {
        return metadata;
    }

    public Manifest Process(Manifest manifest, string outputFolder)
    {
        var file404 = manifest
            .Files
            .SelectMany(x => x.Output.Values)
            .FirstOrDefault(x => x.RelativePath == "404.html");

        if (file404 is not null)
        {
            ProcessOutputFile(file404);
        }

        return manifest;
    }

    private static void ProcessOutputFile(OutputFileInfo output)
    {
        if (!EnvironmentContext.FileAbstractLayer.Exists(output.RelativePath))
            return;

        var doc = new HtmlDocument();

        try
        {
            using var stream = EnvironmentContext.FileAbstractLayer.OpenRead(output.RelativePath);
            doc.Load(stream, Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Logger.LogError($"Failed to load '{output.RelativePath}': {ex.Message}");
        }

        if (RewriteUrls(new Uri($"/{output.RelativePath}"), doc.DocumentNode))
        {
            Logger.LogInfo($"Rewriting URLs in {output.RelativePath}");

            try
            {
                using var stream = EnvironmentContext.FileAbstractLayer.Create(output.RelativePath);
                doc.Save(stream, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to save '{output.RelativePath}': {ex.Message}");
            }
        }
    }

    private static bool RewriteUrls(Uri baseUri, HtmlNode root) =>
        RewriteUrls(baseUri, root, "a", "href") |
        RewriteUrls(baseUri, root, "link", "href") |
        RewriteUrls(baseUri, root, "script", "src") |
        RewriteUrls(baseUri, root, "img", "src") |
        RewriteUrls(baseUri, root, "meta[@name='docfx:navrel']", "content") |
        RewriteUrls(baseUri, root, "meta[@name='docfx:tocrel']", "content") |
        RewriteUrls(baseUri, root, "meta[@name='docfx:rel']", "content");

    private static bool RewriteUrls(
        Uri baseUri,
        HtmlNode root,
        string elementName,
        string attributeName)
    {
        bool isModified = false;

        foreach (var node in root.SelectNodes($"//{elementName}[@{attributeName}]"))
        {
            var attr = node.Attributes[attributeName];
            if (string.IsNullOrWhiteSpace(attr.Value))
                continue;

            string adjustedPath = MakeAbsoluteUri(baseUri, attr.Value);
            if (adjustedPath != attr.Value)
            {
                isModified = true;
                Logger.LogVerbose($"{baseUri.AbsolutePath}: '{attr.Value}' -> '{adjustedPath}'");
                attr.Value = adjustedPath;
            }
        }

        return isModified;
    }

    private static string MakeAbsoluteUri(Uri baseUri, string targetPath)
    {
        if (!Uri.TryCreate(targetPath, UriKind.RelativeOrAbsolute, out Uri? uri))
            return targetPath;

        if (uri.IsAbsoluteUri)
            return uri.ToString();

        return new Uri(baseUri, uri).PathAndQuery;
    }
}