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
        //
        // [HttpGet("/game/room/{roomId}")]
        // public ActionResult Move(int roomId)
        // {
        //   PC newPC = PC.Find(2);
        //   newPC.SetRoomId(roomId);
        //   Console.WriteLine("Passed Room Id data is: " + roomId);
        //   Console.WriteLine("PC's new room is: " + newPC.GetRoomId());
        //   Console.WriteLine("Room Lighted is: " + Room.Find(newPC.GetRoomId()).GetLight());
        //   Console.WriteLine("Room Name is: " + Room.Find(newPC.GetRoomId()).GetName());
        //   Console.WriteLine("Room 2's Name is: " + Room.Find(2).GetName());
        //
        //
        //   Dictionary<int, int[]> myMap = new Dictionary<int, int[]>{};
        //   Dictionary<string, object> myGame = new Dictionary<string, object>{{"room", Room.Find(newPC.GetRoomId()) }};
        //   //           Dictionary<string, object> myGame = new Dictionary<string, object>{"room", Room.Find(PC.GetRoomId()) };
        //   myGame.Add("pc", PC.Find(newPC.GetId()));
        //   myGame.Add("npc", Game.GetAllNPCs(newPC.GetRoomId()));
        //   myGame.Add("item", Game.GetAllItems(newPC.GetRoomId()));
        //
        //   myGame.Add("command", Room.Find(newPC.GetRoomId()).GetCommands());
        //
        //   myMap = Game.GetMap();
        //   myGame.Add("map", myGame);
        //
        //
        //   // return View("/game");
        //   return View("GameIndex", myGame);
        //
        //
        // }

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

          // PC newPC = PC.Find(2);
          // newPC.SetRoomId(roomId);
          //
          // Dictionary<int, int[]> myMap = new Dictionary<int, int[]>{};
          // Dictionary<string, object> myGame = new Dictionary<string, object>{{"room", Room.Find(newPC.GetRoomId()) }};
          // //           Dictionary<string, object> myGame = new Dictionary<string, object>{"room", Room.Find(PC.GetRoomId()) };
          // myGame.Add("pc", PC.Find(newPC.GetId()));
          // myGame.Add("npc", Game.GetAllNPCs(newPC.GetRoomId()));
          // myGame.Add("item", Game.GetAllItems(newPC.GetRoomId()));
          //
          // myGame.Add("command", Room.Find(newPC.GetRoomId()).GetCommands());
          //
          // myMap = Game.GetMap();
          // myGame.Add("map", myMap);
          return View(roomId);
        }



    }
}
