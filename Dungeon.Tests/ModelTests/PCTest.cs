using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
// using MySQLCore.Models;
using System;
using Dungeon.Models;
using Dungeon;

namespace Dungeon.Tests
{
  [TestClass]
  public class PCTest : IDisposable
  {
    public PCTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=dungeon_test;";
    }
    public void Dispose()
    {
      PC.DeleteAll();
      // PC.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = PC.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_TrueForSameDescription_PC()
    {
      //Arrange, Act
      PC firstPC = new PC("Crom");
      PC secondPC = new PC("Crom");

      //Assert
      Assert.AreEqual(firstPC, secondPC);
    }

    [TestMethod]
    public void Save_PCSavesToDatabase_PCList()
    {
      //Arrange
      PC testPC = new PC("Crom");
      testPC.Save();

      //Act
      List<PC> result = PC.GetAll();
      List<PC> testList = new List<PC>{testPC};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_id()
    {
      //Arrange
      PC testPC = new PC("Crom");
      testPC.Save();

      //Act
      PC savedPC = PC.GetAll()[0];

      int result = savedPC.GetId();
      int testId = testPC.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsPCInDatabase_PC()
    {
      //Arrange
      PC testPC = new PC("Crom");
      testPC.Save();

      //Act
      PC result = PC.Find(testPC.GetId());

      //Assert
      Assert.AreEqual(testPC, result);
    }

    [TestMethod]
    public void Test_Add_AddsItemToInventory()
    {
        //Arrange
        PC testPC = new PC("Crom");
        testPC.Save();

        Item testItem = new Item("Sword");
        testItem.Save();

        Item testItem2 = new Item("Torch");
        testItem2.Save();

        //Act
        testPC.AddItemToPC(testItem);
        testPC.AddItemToPC(testItem2);

        List<Item> result = testPC.GetItems();
        List<Item> testList = new List<Item>{testItem, testItem2};

        //Assert
        CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Test_Update_UpdateChangesPCName()
    {
        //Arrange
        PC testPC1 = new PC("Fred");
        testPC1.Save();

        PC testPC2 = new PC("Fred");
        testPC2.Save();

        //Act
        testPC1.Update("Derf", "", 0, 0, 0, 0, 0, 0);
        // string, string, int, int, int, int, int, int

        //Assert
        Assert.AreNotEqual(testPC1.GetName(), testPC2.GetName());

    }


  }
}
