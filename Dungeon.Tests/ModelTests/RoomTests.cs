using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
// using MySQLCore.Models;
using System;
using Dungeon.Models;
using Dungeon;

namespace Dungeon.Tests
{
    [TestClass]
    public class RoomTest : IDisposable
    {
        public RoomTest()
        {
          DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=dungeon_test;";
        }
        public void Dispose()
        {
          Room.DeleteAll();
          Item.DeleteAll();
          // Room.DeleteAll();
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
          //Arrange, Act
          int result = Room.GetAll().Count;

          //Assert
          Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_TrueForSameDescription_Room()
        {
          //Arrange, Act
          Room firstRoom = new Room("Entryway");
          Room secondRoom = new Room("Entryway");

          //Assert
          Assert.AreEqual(firstRoom, secondRoom);
        }

        [TestMethod]
        public void Save_RoomSavesToDatabase_RoomList()
        {
          //Arrange
          Room testRoom = new Room("Entryway");
          testRoom.Save();

          //Act
          List<Room> result = Room.GetAll();
          List<Room> testList = new List<Room>{testRoom};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_id()
        {
          //Arrange
          Room testRoom = new Room("Entryway");
          testRoom.Save();

          //Act
          Room savedRoom = Room.GetAll()[0];

          int result = savedRoom.GetId();
          int testId = testRoom.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsRoomInDatabase_Room()
        {
          //Arrange
          Room testRoom = new Room("Entryway");
          testRoom.Save();

          //Act
          Room result = Room.Find(testRoom.GetId());

          //Assert
          Assert.AreEqual(testRoom, result);
        }

        [TestMethod]
        public void Test_Add_AddsItemToContents()
        {
            //Arrange
            Room testRoom = new Room("Entryway");
            testRoom.Save();

            Item testItem = new Item("Sword");
            testItem.Save();

            Item testItem2 = new Item("Torch");
            testItem2.Save();

            //Act
            testRoom.AddItemToRoom(testItem);
            testRoom.AddItemToRoom(testItem2);

            List<Item> result = testRoom.GetItems();
            List<Item> testList = new List<Item>{testItem, testItem2};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Test_Update_UpdateChangesRoomName()
        {
            //Arrange
            Room testRoom1 = new Room("Entryway");
            testRoom1.Save();

            Room testRoom2 = new Room("Entryway");
            testRoom2.Save();

            //Act
            testRoom1.Update("Foyer", "", "", true, "", 0);
            // string, string, string, bool, string, int

            //Assert
            Assert.AreNotEqual(testRoom1.GetName(), testRoom2.GetName());

        }

    }
}
