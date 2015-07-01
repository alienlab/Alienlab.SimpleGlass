namespace Sitecore.SimpleGlass.Tests
{
  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.FakeDb;

  public static class Extensions
  {
    [NotNull]
    public static Item Get([NotNull] this Db db, [NotNull] string itemPathOrId)
    {
      Assert.ArgumentNotNull(db, "db");
      Assert.ArgumentNotNull(itemPathOrId, "itemPathOrId");

      var database = Z.NotNull(db.Database);

      return database.Get(itemPathOrId);
    }

    [NotNull]
    public static Item Get([NotNull] this Db db, [NotNull] ID id)
    {
      Assert.ArgumentNotNull(db, "db");
      Assert.ArgumentNotNull(id, "id");

      var database = Z.NotNull(db.Database);

      return database.Get(id.ToString());
    }
  }
}
