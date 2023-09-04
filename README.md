Simple Godot Tests
=========

About
-----

Addon for [Godot](https://github.com/godotengine/godot) that allows you to easily create tests for your C# scripts. Its designed to be as simple to use as possible and have as little dependency issues with different Godot versions.

I would recommend to use this testing addon along with [GUT](https://github.com/bitwes/Gut), Godot nodes have a lot of issues with mocking in C# so if you want to mock something its best to do it in GUT for the time being.

How to use
-----


**Disclaimer**:
*Everything for this repo on visual side is done for 4.1.1 (latest) version. If you want to use this addon for older versions some minor setup could be needed (look end of readme).*

Add `[SimpleTestClass]` attribute to your testing class and `[SimpleTestMethod]` attribute to test methods.

Run `EdditorRunner.tscn` provided in `addons/SGT`. Thats it, no additional setup is needed for it to work.

Example test class would look something like this:

```cs
[SimpleTestClass]
internal class ExampleAssertionTest
{
  [SimpleBeforeEach]
  public void BeforeEach()
  {
    // Setup
  }

  [SimpleAfterEach]
  public void AfterEach()
  {
    // Clean
  }

  [SimpleTestMethod]
  public void SomeTestCase()
  {
    // Given
    int a = 1;
    int b = 2;
    int expectedResult = 3;

    // When
    int result = a + b;

    // Then
    Assertions.AssertEqual(expectedResult, result);
  }
}
```

Tests are ran by namespace so you can manually run only one namespace at a time if you want.

For the time being test output is being shown in the console, I'm planning to integrate it in some prettier way, probably something resembling GUT.

Mocking
----

The library works well with [Moq](https://github.com/moq/moq) as far as I have tested. You can use mocks in all test methods without issue.

You can add Moq to your project using:
```
dotnet add package Moq --version <LATEST VERSION>
```


Dev info
-----

Everything is as Godot/IDE agnostic as possible to minigate all of compatibility issues that other plugins tend to have. The only interface entry points with Godot are Logger and EditorRunner classes.

Testing class can be in any `.cs` file in your project, I would recommend putting all tests in a test folder so its easier to exclude the testing files in your `.csproj` file from the realase version of the game:

```xml
<PropertyGroup>
  <DefaultItemExcludes Condition="'$(Configuration)' == 'ExportRelease'">
    $(DefaultItemExcludes);YourTestFolderName/**
  </DefaultItemExcludes>
</PropertyGroup>
```

For older versions of godot
 `EdditorRunner.tscn` can be simply deleted because it only serves as an entry point and a pretty way to show test results, all functions that actually start the tests are in [Runner.cs](https://github.com/RedouxG/SGT/blob/main/addons/SGT/Core/Runner.cs). So if you  really want to you can use this addon with minimal effort of setup in any other godot version.

TODO:
- Add some visually pleasing output of the tests to the runner node
- Add plugin integration so the tests can be ran from a Godot IDE tab

