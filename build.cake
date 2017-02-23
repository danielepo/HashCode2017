#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
// #addin "Cake.Npm"
// #addin "Cake.Git"
// #addin "Cake.Compression"
#addin "Cake.Powershell"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var pack = Argument("target", "Pack");
var all = Argument("target", "All");

var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// PREPARATION/////////////////////////////////////////////////////////////////////

// Define directories.
var zip = "sources.zip";
var projectFolders = new [] {"Program", "Tests"};
var outputFiles = new []{zip};

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Setup(context =>
{
    foreach(var folder in projectFolders)
    {
        foreach(var tmp in new [] {"/bin", "/obj"}){

        var del =  folder + tmp;
        if(DirectoryExists(del))
        {
            Information("Deleting " + del);
            DeleteDirectory(del, true);
        }
        }
    }
    foreach(var file in outputFiles)
    {
        if(FileExists(file)) {
            
            Information("Deleting " + file);
            DeleteFile(file);
        }
    }
});

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore("./HashScheleton.sln");
});


Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
       // Use MSBuild
      MSBuild("./HashScheleton.sln", settings =>
        settings.SetConfiguration(configuration));
    
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3("./**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
        NoResults = true
        });
});

Task("Package-Sources")
    .Does(() => 
{
    StartPowershellScript("git", args =>
    {
        args.Append("archive")
            .Append("-o")
            .Append("sources.zip")
            .Append("HEAD");
    }); 

});



Task("Default")
    .IsDependentOn("Run-Unit-Tests");

Task("Pack")
    .IsDependentOn("Package-Sources");


Task("All")
    .IsDependentOn("Default")
    .IsDependentOn("Package-Sources");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
// RunTarget(pack);
// RunTarget(all);
