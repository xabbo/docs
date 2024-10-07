using System.Text.Json.Serialization;

namespace Xabbo.Docs.Models;

/// <summary>
/// Represents a list of go modules under a domain.
/// </summary>
public class GoModules
{
    /// <summary>
    /// The domain of the module import path.
    /// </summary>
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = "";

    /// <summary>
    /// The list of modules.
    /// </summary>
    [JsonPropertyName("modules")]
    public List<GoModule> Modules { get; set; } = [];
}

/// <summary>
/// Represents a go module.
/// </summary>
public class GoModule
{
    /// <summary>
    /// The name of the module.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    /// <summary>
    /// The relative import path from the domain.
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; } = "";

    /// <summary>
    /// Additional install paths relative from the import path.
    /// </summary>
    [JsonPropertyName("submodules")]
    public List<string> Submodules { get; set; } = [];

    /// <summary>
    /// The repository type. Defaults to 'git'.
    /// </summary>
    [JsonPropertyName("repositoryType")]
    public string RepositoryType { get; set; } = "git";

    /// <summary>
    /// The repository URL.
    /// </summary>
    [JsonPropertyName("repositoryUrl")]
    public string RepositoryUrl { get; set; } = "";
}
