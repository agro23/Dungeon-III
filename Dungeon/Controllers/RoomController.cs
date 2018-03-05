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
      public ActionResult Details()
      {
        Room thisRoom = Room.Find(Int32.Parse(Request.Form["id"]));
        return View("RoomDetails", thisRoom);
      }

      [HttpPost("/rooms/update/{id}")]
      public ActionResult Details(int id)
      {
        // Room thisRoom = Room.Find(id);
        // thisRoom.Update(Request.Form["updatedRoomName"]);
        // return View("RoomDetails", thisRoom);

        Room thisRoom = Room.Find(id);

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

        thisRoom.Update(temp_Name, temp_ShortDescription, temp_FullDescription, temp_Light, temp_Commands);

        Room thisUpdatedRoom = Room.Find(id);

        return View("RoomDetails", thisUpdatedRoom);

      }

  }
}
