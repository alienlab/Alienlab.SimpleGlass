namespace Sitecore.SimpleGlass.Exceptions
{
  using System;
  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Diagnostics;

  public class ItemNotFoundException : Exception
  {
    public ItemNotFoundException([NotNull] string itemPathOrId, [NotNull] Database database)
      : base(GetMessage(itemPathOrId, database))
    {
      Assert.ArgumentNotNull(itemPathOrId, "itemPathOrId");
      Assert.ArgumentNotNull(database, "database");
    }

    [NotNull]
    public static string GetMessage([NotNull] string itemPathOrId, [NotNull] Database database)
    {
      Assert.ArgumentNotNull(itemPathOrId, "itemPathOrId");
      Assert.ArgumentNotNull(database, "database");

      return string.Format("The {0} item does not exist in the {1} database", itemPathOrId, database.Name);
    }
  }
}