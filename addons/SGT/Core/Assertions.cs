using System;

namespace SGT;

public static class Assertions
{
  public static void AssertTrue(bool shoudlBeTrue)
  {
    if (!shoudlBeTrue)
    {
      throw new AssertionException($"Value is false, but expected true.");
    }
  }

  public static void AssertFalse(bool shoudlBeFalse)
  {
    if (shoudlBeFalse)
    {
      throw new AssertionException($"Value is true, but expected false.");
    }
  }

  public static void AssertEqual(object shouldBe, object isNow)
  {
    if (!Equals(shouldBe, isNow))
    {
      throw new AssertionException(
        $"Value is not equal, is: '{shouldBe}', but should be: '{isNow}'.");
    }
  }

  public static void AssertNotEqual(object shouldNotBe, object isNow)
  {
    if (Equals(shouldNotBe, isNow))
    {
      throw new AssertionException(
        $"Value is equal to: '{shouldNotBe}'.");
    }
  }

  public static void AssertLessThan<T>(T maxValue, T value)
        where T : IComparable<T>
  {
    if (!(value.CompareTo(maxValue) < 0))
    {
      throw new AssertionException(
          $"Value '{value}' is not lesser than '{maxValue}'.");
    }
  }

  public static void AssertMoreThan<T>(T minValue, T value)
      where T : IComparable<T>
  {
    if (!(value.CompareTo(minValue) > 0))
    {
      throw new AssertionException(
          $"Value '{value}' is not larger than '{minValue}'.");
    }
  }

  public static void AssertEqualOrLessThan<T>(T maxValue, T value)
        where T : IComparable<T>
  {
    if (!(value.CompareTo(maxValue) <= 0))
    {
      throw new AssertionException(
          $"Value '{value}' is greater than '{maxValue}'.");
    }
  }

  public static void AssertEqualOrMoreThan<T>(T minValue, T value)
      where T : IComparable<T>
  {
    if (!(value.CompareTo(minValue) >= 0))
    {
      throw new AssertionException(
          $"Value '{value}' is less than '{minValue}'.");
    }
  }

  public static void AssertNotInRange<T>(T minValue, T maxValue, T value)
      where T : IComparable<T>
  {
    if (value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0)
    {
      throw new AssertionException(
          $"Value '{value}' is in range: '{minValue}' - '{maxValue}'.");
    }
  }

  public static void AssertInRange<T>(T minValue, T maxValue, T value)
      where T : IComparable<T>
  {
    if (value.CompareTo(minValue) < 0 || value.CompareTo(maxValue) > 0)
    {
      throw new AssertionException(
          $"Value '{value}' is not in range: '{minValue}' - '{maxValue}'.");
    }
  }

  public static void AssertThrows<T>(Action action) where T : Exception
  {
    try
    {
      action();
    }
    catch (T)
    {
      return;
    }
    catch (Exception ex)
    {
      throw new AssertionException(
        $"Expected {typeof(T)}, but got {ex.GetType()} instead.");
    }

    throw new AssertionException($"Expected {typeof(T)} was not thrown.");
  }
}