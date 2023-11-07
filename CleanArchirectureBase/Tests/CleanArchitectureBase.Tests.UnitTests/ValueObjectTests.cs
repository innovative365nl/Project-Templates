using NUnit.Framework;
using CleanArchitectureBase.Domain.Primitives;

namespace CleanArchitectureBase.Tests.UnitTests;

public class ValueObjectTests
{
    private class TestValueObject : ValueObject<int, TestValueObject>
    {
        protected override void Validate()
        {
            if (Value < 0)
            {
                throw new Exception("Value cannot be negative.");
            }
            base.Validate();

        }
    
    }

    [Test]
    public void From_ShouldCreateValueObject()
    {
        var value = 5;
        var valueObject = TestValueObject.From(value);

        Assert.AreEqual(value, valueObject.Value);
    }

    [Test]
    public void EqualityOperator_ShouldCompareValueObjectsCorrectly()
    {
        var valueObject1 = TestValueObject.From(5);
        var valueObject2 = TestValueObject.From(5);
        var valueObject3 = TestValueObject.From(6);

        Assert.IsTrue(valueObject1 == valueObject2);
        Assert.IsFalse(valueObject1 == valueObject3);
    }
    
    [Test]
    public void Equals_WhenCalledWithEqualObjects_ReturnsTrue()
    {
        // Arrange
        var value1 = TestValueObject.From(1);
        var value2 = TestValueObject.From(1);

        // Act
        var result = value1.Equals(value2);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Equals_WhenCalledWithDifferentObjects_ReturnsFalse()
    {
        // Arrange
        var value1 = TestValueObject.From(1);
        var value2 = TestValueObject.From(2);

        // Act
        var result = value1.Equals(value2);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Equals_WhenCalledWithNull_ReturnsFalse()
    {
        // Arrange
        var value1 = TestValueObject.From(1);

        // Act
        var result = value1.Equals(null);

        // Assert
        Assert.IsFalse(result);
    }
    
    [Test]
    public void Equals_WhenCalledWithSameReference_ReturnsTrue()
    {
        // Arrange
        var valueObject1 = TestValueObject.From(1);

        // Act
        var result = valueObject1.Equals(valueObject1);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Equals_WhenCalledWithDifferentReference_ReturnsFalse()
    {
        // Arrange
        var valueObject1 = TestValueObject.From(1);
        var valueObject2 = TestValueObject.From(2);

        // Act
        var result = valueObject1.Equals(valueObject2);

        // Assert
        Assert.IsFalse(result);
    }
    
    [Test]
    public void GetHashCode_ReturnsSameHashCodeForEqualObjects()
    {
        // Arrange
        var value1 = TestValueObject.From(1);
        var value2 = TestValueObject.From(1);

        // Act
        var hash1 = value1.GetHashCode();
        var hash2 = value2.GetHashCode();

        // Assert
        Assert.AreEqual(hash1, hash2);
    }

    [Test]
    public void GetHashCode_ReturnsDifferentHashCodesForDifferentObjects()
    {
        // Arrange
        var value1 = TestValueObject.From(1);
        var value2 = TestValueObject.From(2);

        // Act
        var hash1 = value1.GetHashCode();
        var hash2 = value2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hash1, hash2);
    }
    
    [Test]
    public void TryFrom_ValidInput_ReturnsTrueAndSetsValue()
    {
        // Arrange
        var value = 1;

        // Act
        bool result = TestValueObject.TryFrom(value, out var valueObject);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(value, valueObject.Value);
    }
   
    [Test]
    public void ToString_ReturnsCorrectStringRepresentation()
    {
        // Arrange
        var value = 1;
        var valueObject = TestValueObject.From(value);

        // Act
        var result = valueObject.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(value.ToString()));
    }
    
    [Test]
    public void From_WhenValueIsNegative_ThrowsException()
    {
        // Arrange
        var value = -1;

        // Act & Assert
        Assert.Throws<Exception>(() => TestValueObject.From(value));
    }

    [Test]
    public void From_WhenValueIsGreaterThanNine_DoesNotThrowException()
    {
        // Arrange
        var value = 10;

        // Act & Assert
        Assert.DoesNotThrow(() => TestValueObject.From(value));
    }
}
