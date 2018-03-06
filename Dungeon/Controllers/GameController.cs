using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Dungeon.Models;

namespace Dungeon.Controllers
{
    public class GameController : Controller
    {
        [HttpGet("/game")]
        public ActionResult Index()
        {
          // List<Room> allRooms = Room.GetAll();
          PC newPC = PC.Find(2);
          Dictionary<int, int[]> myMap = new Dictionary<int, int[]>{};
          Dictionary<string, object> myGame = new Dictionary<string, object>{{"room", Room.Find(newPC.GetRoomId()) }};
          //           Dictionary<string, object> myGame = new Dictionary<string, object>{"room", Room.Find(PC.GetRoomId()) };
          myGame.Add("pc", PC.Find(newPC.GetId()));
          myGame.Add("npc", Game.GetAllNPCs(newPC.GetRoomId()));
          myGame.Add("item", Game.GetAllItems(newPC.GetRoomId()));

          myGame.Add("command", Room.Find(newPC.GetRoomId()).GetCommands());

          myMap = Game.GetMap();
          myGame.Add("map", myMap);
          return View("GameIndex", myGame);
        }

        [HttpGet("/game/room/{roomId}")]
        public ActionResult Move(int roomId)
        {

          PC newPC = PC.Find(2);
          newPC.SetRoomId(roomId);
          Dictionary<int, int[]> myMap = new Dictionary<int, int[]>{};
          Dictionary<string, object> myGame = new Dictionary<string, object>{{"room", Room.Find(newPC.GetRoomId()) }};
          //           Dictionary<string, object> myGame = new Dictionary<string, object>{"room", Room.Find(PC.GetRoomId()) };
          myGame.Add("pc", PC.Find(newPC.GetId()));
          myGame.Add("npc", Game.GetAllNPCs(newPC.GetRoomId()));
          myGame.Add("item", Game.GetAllItems(newPC.GetRoomId()));

          myGame.Add("command", Room.Find(newPC.GetRoomId()).GetCommands());

          myMap = Game.GetMap();
          myGame.Add("map", myMap);
          return View("GameIndex", myGame);
        }

        [HttpGet("/game/look/{roomId}")]
        public ActionResult Look(int roomId)
        {

          PC newPC = PC.Find(2);
          newPC.SetRoomId(roomId);
          Dictionary<int, int[]> myMap = new Dictionary<int, int[]>{};
          Dictionary<string, object> myGame = new Dictionary<string, object>{{"room", Room.Find(newPC.GetRoomId()) }};
          //           Dictionary<string, object> myGame = new Dictionary<string, object>{"room", Room.Find(PC.GetRoomId()) };
          myGame.Add("pc", PC.Find(newPC.GetId()));
          myGame.Add("npc", Game.GetAllNPCs(newPC.GetRoomId()));
          myGame.Add("item", Game.GetAllItems(newPC.GetRoomId()));

          myGame.Add("command", Room.Find(newPC.GetRoomId()).GetCommands());

          myMap = Game.GetMap();
          myGame.Add("map", myMap);
          return View("Look", myGame);
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

        // [HttpPost("/rooms")]
        // public ActionResult Create()
        // {
        //   string temp_Name = Request.Form["newRoomName"];
        //
        //   Room newRoom = new Room(temp_Name);
        //
        //   newRoom.Save();
        //   List<Room> allRooms = Room.GetAll();
        //   return View("RoomIndex", allRooms);
        // }
        //
        // [HttpPost("/rooms/details/{id}")]
        // public ActionResult Details()
        // {
        //   Room thisRoom = Room.Find(Int32.Parse(Request.Form["id"]));
        //   return View("RoomDetails", thisRoom);
        // }
        //
        // [HttpPost("/rooms/update/{id}")]
        // public ActionResult Details(int id)
        // {
        //   // Room thisRoom = Room.Find(id);
        //   // thisRoom.Update(Request.Form["updatedRoomName"]);
        //   // return View("RoomDetails", thisRoom);
        //
        //   Room thisRoom = Room.Find(id);
        //
        //   string temp_Name = Request.Form["updatedRoomName"];
        //   string temp_ShortDescription = Request.Form["updatedRoomShortDescription"];
        //   string temp_FullDescription= Request.Form["updatedRoomFullDescription"];
        //   bool temp_Light = false;
        //   if (Request.Form["light"] != "")
        //   {
        //       string selectedLight = Request.Form["light"].ToString();
        //       Console.WriteLine("Light is: " + selectedLight);
        //       if (selectedLight == "lit") { temp_Light = true; }
        //   }
        //   string temp_Commands= Request.Form["updatedRoomCommands"];
        //
        //   thisRoom.Update(temp_Name, temp_ShortDescription, temp_FullDescription, temp_Light, temp_Commands);
        //
        //   Room thisUpdatedRoom = Room.Find(id);
        //
        //   return View("RoomDetails", thisUpdatedRoom);
        //
        // }

    }
}
