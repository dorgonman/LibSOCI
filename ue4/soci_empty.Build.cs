﻿/******************************************************
* Boost Software License - Version 1.0 - 2016/10/06
*
* Copyright (c) 2016 dorgon chang
* http://dorgon.horizon-studio.net/
*
* Permission is hereby granted, free of charge, to any person or organization
* obtaining a copy of the software and accompanying documentation covered by
* this license (the "Software") to use, reproduce, display, distribute,
* execute, and transmit the Software, and to prepare derivative works of the
* Software, and to permit third-parties to whom the Software is furnished to
* do so, all subject to the following:
*
* The copyright notices in the Software and this entire statement, including
* the above license grant, this restriction and the following disclaimer,
* must be included in all copies of the Software, in whole or in part, and
* all derivative works of the Software, unless such copies or derivative
* works are solely in the form of machine-executable object code generated by
* a source language processor.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT
* SHALL THE COPYRIGHT HOLDERS OR ANYONE DISTRIBUTING THE SOFTWARE BE LIABLE
* FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE,
* ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
* DEALINGS IN THE SOFTWARE.
**********************************************************/

using UnrealBuildTool;

using System;
using System.IO;

public class soci_empty : ModuleRules
{

   public soci_empty(ReadOnlyTargetRules Target)
        : base(Target)
    {
        PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;
        Type = ModuleType.External;
        checkExternalLibPath();

        bool bSuccess = AddStaticLibrary(Target);
        if (!bSuccess)
        {
            System.Diagnostics.Debug.Assert(false, "Failed to add library");
        }

        PublicDependencyModuleNames.AddRange(new string[] { "sqlite3Static" });



        PublicIncludePaths.AddRange(
          new string[] {
                ModuleSqlite3PublicIncludePath,

              // ... add public include paths required here ...
          }
          );


        //bFasterWithoutUnity = true;
        // Enable RTTI (prop. of superclass ModuleRules defined in UnrealEngine/Engine/Source/Programs/UnrealBuildTool/System/RulesCompiler.cs )
        bUseRTTI = false; // turn on RTTI
        // this seems be ignored on a mac, check UnrealEngine/Engine/Source/Programs/UnrealBuildTool/Mac/MacToolChain.cs

        // Enable C++ Exceptions for this module
        bEnableExceptions = true;
        

    }

    void checkExternalLibPath()
    {
        if (String.IsNullOrEmpty(ModuleLibRootPath))
        {
            System.Diagnostics.Debug.Assert(false, "checkExternalLibPath failed: Could not find " + ModuleLibRootPath + " in System Environment");
        }
    }



    private bool AddStaticLibrary(ReadOnlyTargetRules Target)
    {
        bool bSuccess = false;

        string libraryPath = "";
        string libraryTargetFilePath = "";
        string libName = this.GetType().Name;
        string libNamePrefix = "";
        string libNameSuffix = "";
        string libVersion = "4_0";
        if (Target.Platform == UnrealTargetPlatform.Win64)
        {
            libNamePrefix = "lib";
            libNameSuffix = ".lib";
            if (WindowsPlatform.Compiler == WindowsCompiler.VisualStudio2015)
            {
                libraryPath = getLibraryPath("win64", "vs2015");
                string libFullName = libNamePrefix + libName + "_" + libVersion + libNameSuffix;
                libraryTargetFilePath = Path.Combine(libraryPath, libFullName);
            }

            if (WindowsPlatform.Compiler == WindowsCompiler.VisualStudio2017)
            {
                libraryPath = getLibraryPath("win64", "vs2017");
                string libFullName = libNamePrefix + libName + "_" + libVersion + libNameSuffix;
                libraryTargetFilePath = Path.Combine(libraryPath, libFullName);
            }
        }
        else if (Target.Platform == UnrealTargetPlatform.Win32)
        {
            libNamePrefix = "lib";
            libNameSuffix = ".lib";
            if (WindowsPlatform.Compiler == WindowsCompiler.VisualStudio2015)
            {
                libraryPath = getLibraryPath("win32", "vs2015");
                string libFullName = libNamePrefix + libName + "_" + libVersion + libNameSuffix;
                libraryTargetFilePath = Path.Combine(libraryPath, libFullName);
            }

            if (WindowsPlatform.Compiler == WindowsCompiler.VisualStudio2017)
            {
                libraryPath = getLibraryPath("win32", "vs2017");
                string libFullName = libNamePrefix + libName + "_" + libVersion + libNameSuffix;
                libraryTargetFilePath = Path.Combine(libraryPath, libFullName);
            }


        }
        else if (Target.Platform == UnrealTargetPlatform.Android)
        {
            libNamePrefix = "lib";
            libNameSuffix = ".a";

            libraryPath = getLibraryPath("android", "armv7-a");

            string libFullName = libNamePrefix + libName + libNameSuffix;
            libraryTargetFilePath = Path.Combine(libraryPath, libFullName);


            PublicLibraryPaths.Add(getLibraryPath("android", "arm64-v8a"));
            PublicLibraryPaths.Add(getLibraryPath("android", "armv7-a"));
            PublicLibraryPaths.Add(getLibraryPath("android", "x86"));
            PublicLibraryPaths.Add(getLibraryPath("android", "x86_64"));
        }
        else if (Target.Platform == UnrealTargetPlatform.Mac)
        {
            libNamePrefix = "lib";
            libNameSuffix = ".a";

            libraryPath = getLibraryPath("macosx", "i386_x86_64");

            string libFullName = libNamePrefix + libName + libNameSuffix;
            libraryTargetFilePath = Path.Combine(libraryPath, libFullName);
        }
        else if (Target.Platform == UnrealTargetPlatform.IOS)
        {
            libNamePrefix = "lib";
            libNameSuffix = ".a";

            libraryPath = getLibraryPath("ios", "iphoneos");

            string libFullName = libNamePrefix + libName + libNameSuffix;
            libraryTargetFilePath = Path.Combine(libraryPath, libFullName);
        }
        else if (Target.Platform == UnrealTargetPlatform.Linux)
        {
            libNamePrefix = "lib";
            libNameSuffix = ".a";

            libraryPath = getLibraryPath("linux", "i386_x86_64");

            string libFullName = libNamePrefix + libName + libNameSuffix;
            libraryTargetFilePath = Path.Combine(libraryPath, libFullName);

        }



        System.Diagnostics.Debug.WriteLine(libName + " libraryPath:" + libraryPath);
        System.Diagnostics.Debug.WriteLine(libName + " libraryTargetFilePath:" + libraryTargetFilePath);
        if (File.Exists(libraryTargetFilePath))
        {
            if (Target.Platform != UnrealTargetPlatform.Android)
            {
                PublicLibraryPaths.Add(libraryPath);
                PublicAdditionalLibraries.Add(libraryTargetFilePath);
            }
            else
            {

                PublicAdditionalLibraries.Add(libName);

            }
            bSuccess = true;
        }
        else
        {

            System.Diagnostics.Debug.Assert(false, "Failed to add" + libName + "library");
        }

        Definitions.Add(string.Format("USE_SOCI_EMPTY={0}", bSuccess ? 1 : 0));

        return bSuccess;
    }

    private string getLibraryPath(string platform, string arch)
    {
        return Path.GetFullPath(Path.Combine(ModuleLibRootPath, platform, arch, "Release"));

    }

    private string ModuleRootPath
    {
        get
        {
            string ModuleCSFilename = UnrealBuildTool.RulesCompiler.GetFileNameFromType(GetType());
            string ModuleCSFileDirectory = Path.GetDirectoryName(ModuleCSFilename);
            return Path.Combine( ModuleCSFileDirectory, "..");
        }
    }

    private string ModuleLibRootPath
    {
        get
        {
            return Path.Combine(ModuleRootPath, "libs");
        }
    }

    private string ModuleLibPublicIncludePath
    {
        get
        {
            return Path.Combine(ModuleRootPath, "soci", "include");
        }
    }
    private string ModuleSqlite3PublicIncludePath
    {
        get
        {
            return Path.GetFullPath(Path.Combine(ModuleRootPath, "..", "libSqlite3", "sqlite3", "src"));
        }
    }
}

