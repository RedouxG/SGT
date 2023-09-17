namespace SGT;
using System;
using Godot;


public partial class GodotRunner : Control
{
  private readonly GodotTestRoot godotTestRoot = new();
  private RichTextLabel output;

  public GodotRunner()
  {
    godotTestRoot.logger.messageLogObservers.AddObservers(
      new MessagePrinter(UpdateLog, true).Print);
  }

  public override void _Ready()
  {
    output = GetNode<RichTextLabel>("Panel/Output");
    AddChild(godotTestRoot);

    RunnerConfig runnerConfig = new();
    try
    {
      runnerConfig = RunnerConfig.LoadFromFile();
      godotTestRoot.RunTestsInNamespaces(runnerConfig.namespaces);
    }
    catch (Exception ex)
    {
      throw new TestSetupException("Failed to load config!", ex);
    }
  }

  public void UpdateLog(string message)
  {
    output.CallDeferred(RichTextLabel.MethodName.AppendText, message += "\n");
  }
}
