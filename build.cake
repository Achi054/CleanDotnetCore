var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");

var solution = "./CleanDotnetCore.sln";
var testProjects = "./test/**/*.csproj";
var artifactPath = "./artifacts";

var publishProjects = new [] { 
    "./src/OrderApi/OrderApi.csproj"
};
var packageProjects = new [] { 
    "./src/OrderRepository/Repository.csproj" 
};
var directoriesToClean = new [] {
    artifactPath
};

Task("CleanArtifacts")
    .Does(() => {
        CleanDirectories(directoriesToClean);
        Information("Clean Artifacts Complete");
    });

Task("Clean")
    .IsDependentOn("CleanArtifacts")
    .Does(() => {
        DotNetCoreClean(solution);
        Information("Clean Complete");
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => {
        DotNetCoreRestore(solution);
        Information("Restore Complete");
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        var settings = new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            OutputDirectory = "./artifacts/"
        };
        DotNetCoreBuild(solution);
        Information("Build Complete");
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        var settings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
            OutputDirectory = "./artifacts/Tests"
        };
        var projectFiles = GetFiles(testProjects);
        foreach(var file in projectFiles)
        {
            DotNetCoreTest(file.FullPath);
        }
        Information("Test run Complete");
    });

Task("Package")
    .IsDependentOn("Test")
    .DoesForEach(packageProjects, (project) => {
        var settings = new DotNetCorePackSettings
        {
            Configuration = configuration,
            OutputDirectory = artifactPath + "/package"
        };

        DotNetCorePack(project, settings);
        Information("Packing Complete");
    });

Task("Publish")
    .IsDependentOn("Test")
    .DoesForEach(publishProjects, (project) => {
        var settings = new DotNetCorePublishSettings
        {
            Configuration = configuration,
            OutputDirectory = artifactPath + "/website"
        };

        DotNetCorePublish(project, settings);
        Information("Publish Complete");
    });

Task("CI")
    .IsDependentOn("Test")
    .Does(() => {
        Information("Continuous Integration Complete");
    });

RunTarget(target);