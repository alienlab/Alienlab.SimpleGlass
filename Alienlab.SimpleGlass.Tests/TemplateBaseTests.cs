namespace Sitecore.SimpleGlass.Tests
{
  using FluentAssertions;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.SecurityModel;

  [TestClass]
  public class TemplateBaseTests
  {
    [TestMethod]
    public void ItemNameTest()
    {
      var id = ID.NewID;
      var tid = ID.NewID;
      var db = new Db
      {
        new DbTemplate("TestTemplate", tid),
        new DbItem("TestItem", id, tid)
      };

      using (db)
      {
        var item = db.Get(id);
        var testItem = new TestTemplateItem(item);
        testItem.ItemName.Should().Be("TestItem");
      }
    }

    [TestMethod]
    public void TemplateNameTest()
    {
      var id = ID.NewID;
      var tid = ID.NewID;
      var db = new Db
      {
        new DbTemplate("TestTemplate", tid),
        new DbItem("TestItem", id, tid)
      };

      using (db)
      {
        var item = db.Get(id);
        var testItem = new TestTemplateItem(item);
        testItem.TemplateName.Should().Be("TestTemplate");
      }
    }

    [TestMethod]
    public void ReadTest()
    {
      var id = ID.NewID;
      var tid = ID.NewID;
      var fid = ID.NewID;
      var db = new Db
      {
        new DbTemplate("TestTemplate", tid)
        {
          new DbField("TestField", fid)
        },
        new DbItem("TestItem", id, tid)
        {
          new DbField(fid) { Value = "TestValue" }
        }
      };

      using (db)
      {
        var item = db.Get(id);
        var testItem = new TestTemplateItem(item);
        testItem.TestField.Value.Should().Be("TestValue");
      }
    }
    
    [TestMethod]
    public void WriteTest()
    {
      var id = ID.NewID;
      var tid = ID.NewID;
      var fid = ID.NewID;
      var db = new Db
      {
        new DbTemplate("TestTemplate", tid)
        {
          new DbField("TestField", fid)
        },
        new DbItem("TestItem", id, tid)
        {
          new DbField(fid) { Value = "TestValue" }
        }
      };

      using (db)
      {
        var item = db.Get(id);
        var testItem = new TestTemplateItem(item);
        using (new EditContext(item, SecurityCheck.Disable))
        {
          testItem.TestField.Value = "NewValue";
        }

        db.Get(id)["TestField"].Should().Be("NewValue");
      }
    }

    private class TestTemplateItem : ItemBase
    {
      public readonly ItemField TestField = new ItemField("TestField");

      public TestTemplateItem([NotNull] Item item) : base(item)
      {
        Sitecore.Diagnostics.Assert.ArgumentNotNull(item, "item");
      }
    }
  }
}