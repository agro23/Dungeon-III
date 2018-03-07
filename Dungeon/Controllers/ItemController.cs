using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Dungeon.Models;

namespace Dungeon.Controllers
{
  public class ItemController : Controller
  {
    [HttpGet("/items")]
    public ActionResult Index()
    {
      List<Item> allItems = Item.GetAll();
      return View("ItemIndex", allItems);
    }

    [HttpPost("/items")]
    public ActionResult Create()
    {
      string name = Request.Form["newItemName"];
      Item newItem = new Item(name);
      newItem.Save();
      List<Item> allItems = Item.GetAll();
      return View("ItemIndex", allItems);
    }

    [HttpPost("/items/details/{id}")]
    public ActionResult Details(int id)
    {
      Item thisItem = Item.Find(Int32.Parse(Request.Form["id"]));
      List<object> tempList = new List<object>{};
      tempList.Add(Room.GetAll());
      tempList.Add(thisItem);
      tempList.Add(id);
      // return View("ItemDetails", thisItem);
      return View("ItemDetails", tempList);

    }

    [HttpPost("/items/update/{itemId}")]
    public ActionResult ItemDetails(int itemId)
    {
      Item thisItem = Item.Find(itemId);
      string temp_Name = Request.Form["updatedItemName"];
      string temp_Type = Request.Form["updatedItemType"];
      string temp_Special = Request.Form["updatedItemSpecial"];
      int temp_MagicInt = Int32.Parse(Request.Form["updatedItemMagic"]);
      bool temp_MagicBool = false;
      if (temp_MagicInt == 1){temp_MagicBool = true;}
      else {}



      thisItem.Update(temp_Name, temp_Type, temp_Special, temp_MagicBool);

      Item thisUpdatedItem = Item.Find(itemId);

      thisItem.AddToContents(Int32.Parse(Request.Form["id"])); // *****
      Console.WriteLine("Inside ItemDetails in Item Controller id from the form is: " + Int32.Parse(Request.Form["roomId"]));

      List<object> tempList = new List<object>{};
      tempList.Add(Room.GetAll());
      tempList.Add(thisItem);
      // tempList.Add(itemId);
      tempList.Add(Int32.Parse(Request.Form["roomId"])); // ***********************************************

      // tempList.Add(ROOMID); // *******************************************************
      // return View("ItemDetails", thisUpdatedItem);
      return View("ItemDetails", tempList);

    }



    [HttpPost("/items/update/{itemId}/{roomId}")]
    public ActionResult ItemAndRoomDetails(int itemId, int roomId)
    {
      Item thisItem = Item.Find(itemId);
      string temp_Name = Request.Form["updatedItemName"];
      string temp_Type = Request.Form["updatedItemType"];
      string temp_Special = Request.Form["updatedItemSpecial"];
      int temp_MagicInt = Int32.Parse(Request.Form["updatedItemMagic"]);
      bool temp_MagicBool = false;
      if (temp_MagicInt == 1){temp_MagicBool = true;}
      else {}
      Console.WriteLine("in ItemAndRoomDetails itemId and roomId are: " + itemId + ", " + roomId);
      Console.WriteLine("Item And Room Details id from the form is: " + Int32.Parse(Request.Form["roomId"]));

      thisItem.Update(temp_Name, temp_Type, temp_Special, temp_MagicBool);

      Item thisUpdatedItem = Item.Find(itemId);

      // thisItem.AddToContents(Int32.Parse(Request.Form["id"])); // *****
      thisItem.AddToContents(roomId);

      List<object> tempList = new List<object>{};
      tempList.Add(Room.GetAll());
      tempList.Add(thisItem);
      tempList.Add(itemId);

      // return View("ItemDetails", thisUpdatedItem);
      return View("ItemDetails", tempList);

    }

  }
}
