# Debug Console for Unity3D

## Table of Contents
- [Introduction](#introduction)
- [Installation](#installation)
    - [Enable Package Dependencies](#enable-package-dependencies)
    - [Add Debug Console Package](#add-debug-console-package)
    - [Recompile](#recompile)
- [Dependencies](#dependencies)
- [Usage](#usage)
    - [Initialization](#initialization)
    - [Adding DebugCommands](#adding-debugcommands)
- [Extending with custom features](#extending-with-custom-features)
- [Notes](#notes)


## Introduction

This is a leightweight package to add a debug console and cheat codes into your unity project. This is especially helpful when playtesting in the exported build where you don't have access to the Unity Inspector etc. 

## Installation

### Enable Package Dependencies
This package has dependencies to other git repositories which Unity needs to resolve. The Unity Package Manager does not yet resolve packages correctly. This tool mitigates this and resolves all git dependencies correctly https://github.com/mob-sakai/GitDependencyResolverForUnity. 

To install this resolver tool you simply need to adapt the `manifest.json` file in the `Packages` directory in your project. Add the following:
```json
{
  "dependencies": {
    "com.coffee.git-dependency-resolver": "https://github.com/mob-sakai/GitDependencyResolverForUnity.git"
  }
}
```

### Add Debug Console Package

Similar to the step above you further need to add the following line to the dependencie-section in the `manifest.json`:

```json
"com.dehagge.debugconsole": "https://github.com/jnroesch/com.dehagge.debugconsole.git"
``` 

### Recompile

After saving the `manifest.json` Unity should automatically recompile and add the new packages. If not, it might help to restart Unity. 

## Dependencies
This package has the following dependencies:

```json
"com.svermeulen.extenject": "https://github.com/starikcetin/Extenject.git#9.1.0"
```

## Usage

### Initialization
Add the `DebugCommandCollection` and the desired Binding for the `DebugCommandHandler` to your DI System. Personally I like to have a single collection created in the ProjectInstaller however you are free to create individual collections / handlers per scene etc.

```csharp
public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IDebugCommandHandler>().To<DebugCommandHandler>().AsSingle();
        Container.Bind<DebugCommandCollection>().FromNew().AsSingle();
    }
}
```

Add the `DebugConsoleController` to any GameObject in your Scene. 

### Adding DebugCommands

Currently this package supports commands with up to two generic parameters out of the box. You can create more if you want.

A DebugCommand consists of 
1. id -> the command that will invoke the cheat
2. description -> a description stating what this cheat does
3. format -> used to indicate parameter order
4. command -> the actual action to be executed

Here you can see two sample commands.

```csharp
var command = new DebugCommand("help", "displays all available commands", "help", () =>
{
    showHelp = true;
});
_commandCollection.AddDebugCommand(command);
```

```csharp
var command = new DebugCommand<int>("test", "logs a number to the console", "test <number>",
    (x) =>
    {
        Debug.Log(x);
    });
_commandCollection.AddDebugCommand(command);
```

## Extending with custom features

### Extend Commands with more types
Simply inherit from `DebugCommandBase` and create commands to your liking.

### Change the way commands are handled
Create a new CommandHandler that implements `IDebugCommandHandler` and change the binding in the installer to the new implementation.

### Change DebugConsole layout / handling
Simply write your own implementation as an alternative to the `DebugConsoleController`. You can either create a new one and inject your CommandCollection and CommandHandler into it or get rid of the controller entirely as the ConsoleController merely acts as a wrapper/GUI for the CommandHandler.

## Notes
This implementation is based on the youtube tutorial of 'Game Dev Guide' (https://www.youtube.com/watch?v=VzOEM-4A2OM) but also includes some important fixes and makes the code much more reusable.