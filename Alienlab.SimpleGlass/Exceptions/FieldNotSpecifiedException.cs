namespace Sitecore.SimpleGlass.Exceptions
{
  using System;
  using Sitecore;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;

  public class FieldNotSpecifiedException : Exception
  {
    public FieldNotSpecifiedException([NotNull] Item item, [NotNull] string itemPathOrId)
      : base(GetMessage(item, itemPathOrId))
    {
      Assert.ArgumentNotNull(item, "item");
      Assert.ArgumentNotNull(itemPathOrId, "itemPathOrId");
    }

    [NotNull]
    public static string GetMessage([NotNull] Item item, [NotNull] string itemPathOrId)
    {
      Assert.ArgumentNotNull(item, "item");
      Assert.ArgumentNotNull(itemPathOrId, "itemPathOrId");

      return string.Format("The {0} item does not have {1} field specified", item, itemPathOrId);
    }
  }
}