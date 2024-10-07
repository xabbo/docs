using System.Collections.Immutable;
using System.Composition;
using Docfx.Common;
using Docfx.Plugins;

using Xabbo.Docs.Models;

namespace Xabbo.Docs.Processors;

[Export(nameof(GoModulesProcessor), typeof(IDocumentBuildStep))]
public class GoModulesBuildStep : IDocumentBuildStep
{
    public string Name => nameof(GoModulesBuildStep);

    public int BuildOrder => 0;

    public IEnumerable<FileModel> Prebuild(ImmutableList<FileModel> models, IHostService host)
    {
        List<FileModel> outputModels = [];

        foreach (var model in models)
        {
            var content = (Dictionary<string, object>)model.Content;
            var mods = (GoModules)content["mods"];

            foreach (var module in mods.Modules)
            {
                outputModels.Add(CreateModel(model, mods, module));
                foreach (string installPath in module.Submodules)
                    outputModels.Add(CreateModel(model, mods, module, installPath));
            }
        }

        return outputModels;
    }

    public void Build(FileModel model, IHostService host) { }

    public void Postbuild(ImmutableList<FileModel> models, IHostService host) { }

    private static FileModel CreateModel(FileModel model, GoModules modules, GoModule module, string? submodulePath = null)
    {
        var contents = new Dictionary<string, object>() {
            ["type"] = "GoMod",
            ["domain"] = modules.Domain,
            ["module_name"] = module.Name,
            ["module_path"] = module.Path,
            ["module_import"] = modules.Domain + "/" + module.Path,
            ["submodule_path"] = submodulePath ?? "",
            ["repository_type"] = module.RepositoryType,
            ["repository_url"] = module.RepositoryUrl,
        };

        string outputFilePath = Path.Join(module.Path, submodulePath) + Path.GetExtension(model.File);
        string destinationFilePath = Path.Join(model.FileAndType.DestinationDir, outputFilePath);

        var localPathFromRoot = PathUtility.MakeRelativePath(
            EnvironmentContext.BaseDirectory, destinationFilePath);

        var ft = new FileAndType(
            model.BaseDir,
            outputFilePath,
            DocumentType.Article
        );

        return new FileModel(ft, contents)
        {
            LocalPathFromRoot = localPathFromRoot
        };
    }
}
