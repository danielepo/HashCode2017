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
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// PREPARATION/////////////////////////////////////////////////////////////////////

// Define directories.

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Setup(context =>
{
    // var bin =  "Training/bin";
    // if(DirectoryExists(bin))
    //     DeleteDirectory(bin,true);
    
    // var obj = "Training/obj";
    // if(DirectoryExists(obj))
    //     DeleteDirectory(obj,true);
    
    // if(FileExists(package))
    //     DeleteFile(package);
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
    var repositoryDirectoryPath = DirectoryPath.FromString(".");
    // var currentBranch = GitBranchCurrent(repositoryDirectoryPath);  
    // Information(currentBranch.Remotes[0].Url);
    // if(!DirectoryExists("./temp/"))
    //     CreateDirectory("./temp/");

    StartPowershellScript("git", args =>
    {
        args.Append("archive")
            .Append("-o")
            .Append("sources.zip")
            .Append("HEAD");
    }); 
    // GitClone(currentBranch.Remotes[0].Url, "./temp/","","");
    // Zip("./temp", "package.zip");
    // if(DirectoryExists("./temp")) 
    //    StartPowershellScript("Remove-Item", args =>
    //     {
    //         args.Append("-Recurse")
    //             .Append("-Force")
    //             .AppendQuoted("./temp");
    //     }); 
});



Task("Default")
    .IsDependentOn("Run-Unit-Tests");

Task("Pack")
    .IsDependentOn("Package-Sources");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
RunTarget(pack);
