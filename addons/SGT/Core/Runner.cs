namespace SGT;
using System.Diagnostics;


public class Runner
{
  private readonly Logger logger;
  private readonly long timeoutMs;

  public Runner(Logger logger, long timeoutMs = 60 * 1000)
  {
    this.logger = logger;
    this.timeoutMs = timeoutMs;
  }

  public bool RunAllTests()
  {
    bool testsPassed = true;
    var stopwatch = Stopwatch.StartNew();

    logger.AnnounceBlockStart("> Starting Tests");
    foreach (string namespaceName in AssemblyExtractor.GetAllTestNamespaces())
    {
      testsPassed = testsPassed && RunTestsInNamespace(namespaceName);
    }
    logger.AnnounceBlockEnd($"> {MessageTemplates.GetTestResultString(testsPassed)} Finishing Tests | took: {stopwatch.ElapsedMilliseconds}ms");

    return testsPassed;
  }

  public bool RunTestsInNamespace(string namespaceName)
  {
    bool testsPassed = true;
    var testObjects = AssemblyExtractor.GetTestObjectsInNamespace(namespaceName);
    var stopwatch = Stopwatch.StartNew();

    logger.AnnounceBlockStart($"> Begin tests for namespace: {namespaceName}");
    foreach (var instance in testObjects)
    {
      testsPassed = testsPassed &&
        new TestObjectRunner(logger, instance, timeoutMs).RunAllTestsInObject();
    }
    logger.AnnounceBlockEnd($"> {MessageTemplates.GetTestResultString(testsPassed)} End tests for namespace: {namespaceName} | took: {stopwatch.ElapsedMilliseconds}ms");

    return testsPassed;
  }
}
