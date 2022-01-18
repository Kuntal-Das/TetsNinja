using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class StackTests
{
    private Stack<string> _stack;

    [SetUp]
    public void SetUp()
    {
        _stack = new();
    }

    [Test]
    public void Push_ArgumentIsNull_ThrowArgNullException()
    {
        Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Push_ValidArg_AddTheArgToTheStack()
    {
        _stack.Push("a");

        Assert.That(_stack.Count, Is.EqualTo(1));
    }

    [Test]
    public void Count_EmptyStack_ReturnZero()
    {
        Assert.That(_stack.Count, Is.EqualTo(0));
    }

    [Test]
    // [Ignore("UnImplemented")]
    public void Pop_EmptyStack_ThrowInvalidOperationException()
    {
        Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_StackWithAFewObjects_ReturnsObjOnTheTop()
    {
        //Arrange
        _stack.Push("a");
        _stack.Push("b");
        _stack.Push("c");

        //Act
        var result = _stack.Pop();

        //Assert
        Assert.That(result, Is.EqualTo("c"));
    }

    [Test]
    public void Pop_StackWithAFewObjects_RemoveObjFromTheTop()
    {
        //Arrange
        _stack.Push("a");
        _stack.Push("b");
        _stack.Push("c");

        //Act
        _stack.Pop();

        //Assert
        Assert.That(_stack.Count, Is.EqualTo(2));
    }

    [Test]
    public void Peek_EmptyStack_ThrowInvalidOperationException()
    {
        Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
    }

    [Test]
    public void Peek_StackWithAFewObj_ReturnObjectOnTopOfTheStack()
    {
        //Arrange
        _stack.Push("a");
        _stack.Push("b");
        _stack.Push("c");

        //Act
        var result = _stack.Peek();

        //Assert
        Assert.That(result, Is.EqualTo("c"));
    }

    [Test]
    public void Peek_StackWithAFewObj_DoesNotRemoveTheObjectOnTopOfTheStack()
    {
        //Arrange
        _stack.Push("a");
        _stack.Push("b");
        _stack.Push("c");

        //Act
        _stack.Peek();

        //Assert
        Assert.That(_stack.Count, Is.EqualTo(3));
    }
}