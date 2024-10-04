using System.Collections.Immutable;
using System.Composition;
using System.Text.Json;
using System.Text.Json.Serialization;
using Docfx.Common;
using Docfx.Plugins;

using Xabbo.Docs.Models;

namespace Xabbo.Docs.Processors;

[Export(typeof(IDocumentProcessor))]
public class GoModulesProcessor : IDocumentProcessor
{
    static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public string Name => nameof(GoModulesProcessor);

    [ImportMany(nameof(GoModulesProcessor))]
    public IEnumerable<IDocumentBuildStep>? BuildSteps { get; set; }

    public ProcessingPriority GetProcessingPriority(FileAndType file)
    {
        string fileName = Path.GetFileName(file.File);

        if (file.Type == DocumentType.Article &&
            (fileName.Equals("go.mods.json", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".go.mods.json", StringComparison.OrdinalIgnoreCase)))
        {
            return ProcessingPriority.Normal;
        }

        return ProcessingPriority.NotSupported;
    }

    public FileModel Load(FileAndType file, ImmutableDictionary<string, object> metadata)
    {
        string json = File.ReadAllText(file.FullPath);
        var mods = JsonSerializer.Deserialize<GoModules>(json)
            ?? throw new FormatException($"Deserializing GoModules from file '{file.File}' returned null.");

        Logger.LogInfo($"Loaded {mods.Modules.Count} modules for domain \"{mods.Domain}\".");

        var content = new Dictionary<string, object>
        {
            ["mods"] = mods,
            ["type"] = "GoMod",
            ["path"] = file.File
        };

        string basePath = EnvironmentContext.BaseDirectory;
        string absolutePath = EnvironmentContext.FileAbstractLayer.GetPhysicalPath(file.File);
        var localPathFromRoot = PathUtility.MakeRelativePath(basePath, absolutePath);

        return new FileModel(file, content)
        {
            LocalPathFromRoot = localPathFromRoot
        };
    }

    public SaveResult Save(FileModel model)
    {
        return new SaveResult
        {
            DocumentType = "GoMod",
            FileWithoutExtension = Path.ChangeExtension(model.File, null)
        };
    }

    public void UpdateHref(FileModel model, IDocumentBuildContext context) { }
}
