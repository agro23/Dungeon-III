using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Dungeon.Models;

namespace Dungeon.Controllers
{
  public class RoomController : Controller
  {
      [HttpGet("/rooms")]
      public ActionResult Index()
      {
        List<Room> allRooms = Room.GetAll();
        return View("RoomIndex", allRooms);
      }

      // [HttpPost("/rooms")]
      // public ActionResult Create()
      // {
      //   string name = Request.Form["newRoomName"];
      //   Room newRoom = new Room(name);
      //   newRoom.Save();
      //   List<Room> allRooms = Room.GetAll();
      //   return View("RoomIndex", allRooms);
      // }

      [HttpPost("/rooms")]
      public ActionResult Create()
      {
        string temp_Name = Request.Form["newRoomName"];
        Room newRoom = new Room(temp_Name);
        newRoom.Save();
        List<Room> allRooms = Room.GetAll();
        return View("RoomIndex", allRooms);
      }

      [HttpPost("/rooms/details/{id}")]
      public ActionResult Details(int id)
      {
        Room thisRoom = Room.Find(Int32.Parse(Request.Form["id"]));

        List<object> tempList = new List<object>{};
        tempList.Add(Room.GetAll());
        tempList.Add(thisRoom);
        tempList.Add(id);

        // Console.WriteLine("id passed to RoomDetails is: " + id);
        // Console.WriteLine("thisroom passed to RoomDetails is: " + thisRoom.GetName());
        // return View("RoomDetails", thisRoom);
        return View("RoomDetails", tempList);

      }

      [HttpPost("/rooms/update/{id}")]
      public ActionResult RoomDetails(int id)
      {
        // Room thisRoom = Room.Find(id);
        // thisRoom.Update(Request.Form["updatedRoomName"]);
        // return View("RoomDetails", thisRoom);

        Room thisRoom = Room.Find(id);
          int temp_RoomMapId = Int32.Parse(Request.Form["updatedRoomMapId"]);
        Console.WriteLine("Room ID is: " + id + " and Room MAP ID is: " + temp_RoomMapId);
        string temp_Name = Request.Form["updatedRoomName"];
        string temp_ShortDescription = Request.Form["updatedRoomShortDescription"];
        string temp_FullDescription= Request.Form["updatedRoomFullDescription"];
        bool temp_Light = false;
        if (Request.Form["light"] != "")
        {
            string selectedLight = Request.Form["light"].ToString();
            Console.WriteLine("Light is: " + selectedLight);
            if (selectedLight == "lit") { temp_Light = true; }
        }
        string temp_Commands= Request.Form["updatedRoomCommands"];

        thisRoom.Update(temp_Name, temp_ShortDescription, temp_FullDescription, temp_Light, temp_Commands, temp_RoomMapId);

        Room thisUpdatedRoom = Room.Find(id);
        List<object> tempList = new List<object>{};
        tempList.Add(Room.GetAll());
        tempList.Add(thisUpdatedRoom);
        tempList.Add(id);

        Console.WriteLine("thisUpdatedRoom: " + thisUpdatedRoom.GetMapId());
        Console.WriteLine("id: " + id);




        return View("RoomDetails", tempList);
        // return View("RoomDetails", thisUpdatedRoom);

      }

  }
}
