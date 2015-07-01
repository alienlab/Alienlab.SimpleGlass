namespace Sitecore.SimpleGlass
{
  using System.Reflection;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;

  public abstract class ItemBase
  {
    [NotNull]
    public readonly string ItemName;

    [NotNull]
    public readonly Item InnerItem;

    [NotNull]
    public readonly string TemplateName;

    protected ItemBase([NotNull] Item item)
    {
      Assert.ArgumentNotNull(item, "item");

      var itemName = item.Name;
      Assert.IsNotNull(itemName, "itemName");

      var typeName = this.GetType().Name;
      var templateName = typeName.EndsWith("Item") ? typeName.Substring(0, typeName.Length - "Item".Length) : typeName;

      this.ItemName = itemName;
      this.InnerItem = item;
      this.TemplateName = templateName;

      var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
      foreach (var fieldInfo in fields)
      {
        var field = fieldInfo.GetValue(this) as ItemField;
        if (field == null)
        {
          continue;
        }

        field.Initialize(this);
      }
    }
  }
}