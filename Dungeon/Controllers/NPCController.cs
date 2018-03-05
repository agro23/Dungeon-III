using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Dungeon.Models;

namespace Dungeon.Controllers
{
  public class NPCController : Controller
  {
      [HttpGet("/npcs")]
      public ActionResult Index()
      {
        List<NPC> allNPCs = NPC.GetAll();
        return View("NPCIndex", allNPCs);
      }

      [HttpPost("/npcs")]
      public ActionResult Create()
      {
        string name = Request.Form["newNPCName"];
        NPC newNPC = new NPC(name);
        newNPC.Save();
        List<NPC> allNPCs = NPC.GetAll();
        return View("NPCIndex", allNPCs);
      }

      [HttpPost("/npcs/details/{id}")]
      public ActionResult Details()
      {
        NPC thisNPC = NPC.Find(Int32.Parse(Request.Form["id"]));
        return View("NPCDetails", thisNPC);
      }

      [HttpPost("/npcs/update/{id}")]
      public ActionResult Details(int id)
      {
        NPC thisNPC = NPC.Find(id);
        string temp_Name = Request.Form["updatedNPCName"];
        string temp_Type = Request.Form["updatedNPCType"];
        int temp_HP = Int32.Parse(Request.Form["updatedNPCHP"]);
        int temp_AC = Int32.Parse(Request.Form["updatedNPCAC"]);
        int temp_Damage = Int32.Parse(Request.Form["updatedNPCDamage"]);
        int temp_LVL = Int32.Parse(Request.Form["updatedNPCLVL"]);
        int temp_RoomId = Int32.Parse(Request.Form["updatedRoomId"]);
        thisNPC.Update(temp_Name, temp_Type, temp_HP, temp_AC, temp_Damage, temp_LVL, temp_RoomId);

        NPC thisUpdatedNPC = NPC.Find(id);
        return View("NPCDetails", thisUpdatedNPC);
      }
  }
}
