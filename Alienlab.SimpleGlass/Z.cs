namespace Sitecore.SimpleGlass
{
  using System;
  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.SimpleGlass.Exceptions;

  public static class Z
  {
    //[NotNull]
    //public static T Throw<T>([CanBeNull] string message = null)
    //{
    //  throw new InvalidOperationException(message);
    //}

    [NotNull]
    public static T NotNull<T>([CanBeNull] T obj, [CanBeNull] string message = null)
    {
      Assert.IsNotNull(obj, message);

      return obj;
    }

    [NotNull]
    public static string Get([NotNull] this Item item, [NotNull] string itemPathOrId)
    {
      Assert.ArgumentNotNull(item, "item");
      Assert.ArgumentNotNull(itemPathOrId, "itemPathOrId");

      var fieldValue = item[itemPathOrId];
      if (string.IsNullOrEmpty(fieldValue))
      {
        throw new FieldNotSpecifiedException(item, itemPathOrId);
      }

      return fieldValue;
    }

    [NotNull]
    public static Item Get([NotNull] this Database database, [NotNull] string itemPathOrId)
    {
      Assert.ArgumentNotNull(database, "database");
      Assert.ArgumentNotNull(itemPathOrId, "itemPathOrId");

      var item = database.GetItem(itemPathOrId);
      if (item == null)
      {
        throw new ItemNotFoundException(itemPathOrId, database);
      }

      return item;
    }

    //public static void Invoke<T>([NotNull] this IEnumerable<T> arr)
    //{
    //  Assert.ArgumentNotNull(arr, "arr");

    //  // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
    //  arr.ToArray();
    //}

    //[CanBeNull]
    //public static T Safe<T>([NotNull] Func<T> func) where T : class
    //{
    //  Assert.ArgumentNotNull(func, "func");

    //  try
    //  {
    //    return func();
    //  }
    //  catch
    //  {
    //    return null;
    //  }
    //}

    [CanBeNull]
    public static T Safe<T>([NotNull] Func<T> func, [NotNull] Type exceptionType, [NotNull] string exceptionMessage) where T : class
    {
      Assert.ArgumentNotNull(func, "func");
      Assert.ArgumentNotNull(exceptionType, "exceptionType");
      Assert.ArgumentNotNull(exceptionMessage, "exceptionMessage");

      try
      {
        return func();
      }
      catch (Exception ex)
      {
        if (ex.GetType() == exceptionType && ex.Message.Equals(exceptionMessage))
        {
          return null;
        }

        throw;
      }
    }
  }
}
