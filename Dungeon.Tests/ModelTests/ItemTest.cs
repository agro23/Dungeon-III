using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
// using MySQLCore.Models;
using System;
using Dungeon.Models;
using Dungeon;

namespace Dungeon.Tests
{
  [TestClass]
  public class ItemTest : IDisposable
  {
    public ItemTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=dungeon_test;";
    }
    public void Dispose()
    {
      Item.DeleteAll();
      // Item.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Item.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_TrueForSameDescription_Item()
    {
      //Arrange, Act
      Item firstItem = new Item("Torch");
      Item secondItem = new Item("Torch");

      //Assert
      Assert.AreEqual(firstItem, secondItem);
    }

    [TestMethod]
    public void Save_ItemSavesToDatabase_ItemList()
    {
      //Arrange
      Item testItem = new Item("Torch");
      testItem.Save();

      //Act
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item>{testItem};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Test_Update_UpdateChangesItemName()
    {
        //Arrange
        Item testItem1 = new Item("Torch");
        testItem1.Save();

        Item testItem2 = new Item("Torch");
        testItem2.Save();

        //Act
        testItem1.Update("Burnt Torch", "", "", true);
        // string, string, string, bool

        //Assert
        Assert.AreNotEqual(testItem1.GetName(), testItem2.GetName());

    }


  }
}
