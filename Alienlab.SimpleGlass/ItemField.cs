namespace Sitecore.SimpleGlass
{
  using System;
  using Sitecore.Diagnostics;

  public class ItemField
  {
    [NotNull]
    public readonly string FieldName;

    [CanBeNull]
    private ItemBase item;

    public ItemField([NotNull] string fieldName)
    {
      Assert.ArgumentNotNull(fieldName, "fieldName");

      this.FieldName = fieldName;
      this.item = null;
    }

    [CanBeNull]
    public string Value
    {
      get
      {
        var itemBase = this.item;
        if (itemBase == null)
        {
          throw new InvalidOperationException("ItemField is not initialized");
        }

        return itemBase.InnerItem[this.FieldName];
      }

      set
      {
        var itemBase = this.item;
        if (itemBase == null)
        {
          throw new InvalidOperationException("ItemField is not initialized");
        }

        itemBase.InnerItem[this.FieldName] = value;
      }
    }

    [CanBeNull]
    protected ItemBase Item
    {
      get
      {
        return this.item;
      }
    }

    public void Initialize([NotNull] ItemBase itemBase)
    {
      Assert.ArgumentNotNull(itemBase, "itemBase");

      this.item = itemBase;
    }
  }
}