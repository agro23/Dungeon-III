using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
// using MySQLCore.Models;
using System;
using Dungeon.Models;
using Dungeon;

namespace Dungeon.Tests
{
  [TestClass]
  public class NPCTest : IDisposable
  {
    public NPCTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=dungeon_test;";
    }
    public void Dispose()
    {
      NPC.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = NPC.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_TrueForSameDescription_NPC()
    {
      //Arrange, Act
      NPC firstNPC = new NPC("Orc");
      NPC secondNPC = new NPC("Orc");

      //Assert
      Assert.AreEqual(firstNPC, secondNPC);
    }

    [TestMethod]
    public void Save_NPCSavesToDatabase_NPCList()
    {
      //Arrange
      NPC testNPC = new NPC("Orc");
      testNPC.Save();

      //Act
      List<NPC> result = NPC.GetAll();
      List<NPC> testList = new List<NPC>{testNPC};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_id()
    {
      //Arrange
      NPC testNPC = new NPC("Orc");
      testNPC.Save();

      //Act
      NPC savedNPC = NPC.GetAll()[0];

      int result = savedNPC.GetId();
      int testId = testNPC.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsNPCInDatabase_NPC()
    {
      //Arrange
      NPC testNPC = new NPC("Orc");
      testNPC.Save();

      //Act
      NPC result = NPC.Find(testNPC.GetId());

      //Assert
      Assert.AreEqual(testNPC, result);
    }

    [TestMethod]
    public void Test_Add_AddsItemToInventory()
    {
        //Arrange
        NPC testNPC = new NPC("Orc");
        testNPC.Save();

        Item testItem = new Item("Sword");
        testItem.Save();

        Item testItem2 = new Item("Torch");
        testItem2.Save();

        //Act
        testNPC.AddItemToNPC(testItem);
        testNPC.AddItemToNPC(testItem2);

        List<Item> result = testNPC.GetItems();
        List<Item> testList = new List<Item>{testItem, testItem2};

        //Assert
        CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Test_Update_UpdateChangesNPCName()
    {
        //Arrange
        NPC testNPC1 = new NPC("Jim Orc");
        testNPC1.Save();

        NPC testNPC2 = new NPC("Jim Orc");
        testNPC2.Save();

        //Act
        testNPC1.Update("James Orc", "", 0,0,0,0,0);


        //Assert
        Assert.AreNotEqual(testNPC1.GetName(), testNPC2.GetName());

    }

  }
}
