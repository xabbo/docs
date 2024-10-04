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
                var contents = new Dictionary<string, object>() {
                    ["type"] = "GoMod",
                    ["domain"] = mods.Domain,
                    ["name"] = module.Name,
                    ["import_path"] = module.ImportPath,
                    ["repository_type"] = module.RepositoryType,
                    ["repository_url"] = module.RepositoryUrl,
                };

                string newFileName = module.Name + Path.GetExtension(model.File);
                string destPath = Path.Join(model.FileAndType.DestinationDir, newFileName);

                var localPathFromRoot = PathUtility.MakeRelativePath(
                    EnvironmentContext.BaseDirectory, destPath);

                var ft = new FileAndType(
                    model.BaseDir,
                    newFileName,
                    DocumentType.Article
                );

                outputModels.Add(new FileModel(ft, contents)
                {
                    LocalPathFromRoot = localPathFromRoot
                });
            }
        }

        return outputModels;
    }

    public void Build(FileModel model, IHostService host) { }

    public void Postbuild(ImmutableList<FileModel> models, IHostService host) { }
}
