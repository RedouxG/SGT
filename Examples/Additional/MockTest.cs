/* Disclaimer:
    This test uses Moq library: https://github.com/moq/moq
    Use this mocking library for your c# classes that are not derived from Godot,
    for godot mocks use GUT.

    I would run all node tests from GDScript, its easy to use c# godot scripts from
    GDScript as long as the methods return and take supported types.
    
    GUT: https://github.com/bitwes/Gut
*/
namespace MoqTest;
using Moq;
using SGT;

public class TestedClass
{
  virtual public int AddNumbers(int a, int b)
  {
    return a + b;
  }
}

public class MockTest : SimpleTestClass
{
  [SimpleTestMethod]
  public void TestMock()
  {
    // Given
    var mock = new Mock<TestedClass>();
    mock.Setup(obj => obj.AddNumbers(1, 1)).Returns(3);

    //When
    var returnedValue = mock.Object.AddNumbers(1, 1);

    // Then
    mock.Verify(obj => obj.AddNumbers(1, 1), Times.AtLeastOnce());
    Assertions.AssertEqual(returnedValue, 3);
  }
}