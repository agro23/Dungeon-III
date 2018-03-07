using System.Collections.Generic;
using MySql.Data.MySqlClient;
// using MySQLCore.Models;
using System;
using Dungeon.Models;
using Dungeon;

namespace Dungeon.Models
{

    public class Game
    {
        private string _name;
        private string _shortDescription;
        private string _fullDescription;
        private bool _light;
        private string _commands;
        private int _id;
        // We no longer declare _RoomId here

        // public Game(string name)

        public Game(string name, string shortDescription="", string fullDescription="", bool light=false, string commands="", int id = 0)
        {
            _name = name;
            _shortDescription = shortDescription;
            _fullDescription = fullDescription;
            _light = light;
            _commands = commands;
            _id = id;
        }

        // public override bool Equals(System.Object otherGame)
        // {
        //   if (!(otherGame is Game))
        //   {
        //     return false;
        //   }
        //   else
        //   {
        //      Game newGame = (Game) otherGame;
        //      bool idEquality = this.GetId() == newRoom.GetId();
        //      bool nameEquality = this.GetName() == newRoom.GetName();
        //      return (idEquality && nameEquality);
        //    }
        // }
        // public override int GetHashCode()
        // {
        //      return this.GetName().GetHashCode();
        // }

        public string GetName()
        {
            return _name;
        }





        public static Dictionary<int, int[]> GetMap()
        {
            Dictionary <int, int[]> map = new Dictionary <int, int[]>();
            //             {N,NE,E,SE,S,SW,W,NW,U,D,H});
            map.Add(1,new[]{0,0,0,0,2,0,0,0,0,0,0});
            map.Add(2,new[]{1,0,4,0,3,0,5,0,0,0,0});
            map.Add(3,new[]{2,0,4,0,0,0,5,0,0,0,0});
            map.Add(4,new[]{0,0,0,0,3,0,2,0,0,0,0});
            map.Add(5,new[]{0,0,2,0,3,0,0,0,0,0,0});
            return map;
        }

        public static List<NPC> GetAllNPCs(int Id)
        {
            List<NPC> allNPCs = new List<NPC> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM npcs WHERE room_id = @roomId;";

            MySqlParameter roomId = new MySqlParameter();
            roomId.ParameterName = "@roomId";
            roomId.Value = Id;
            cmd.Parameters.Add(roomId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int npcId = rdr.GetInt32(0);
              string npcName = rdr.GetString(1);
              string npcType = rdr.GetString(2);
              int npcHP = rdr.GetInt32(3);
              int npcAC = rdr.GetInt32(4);
              int npcDamage = rdr.GetInt32(5);
              int npcLVL = rdr.GetInt32(6);
              int npcRoomId = rdr.GetInt32(7);

              NPC newNPC = new NPC(npcName, npcType, npcHP, npcHP, npcDamage, npcLVL, npcRoomId, npcId);
              allNPCs.Add(newNPC);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allNPCs;
        }

        public static List<Item> GetAllItems(int Id)
        {
            List<Item> allItems = new List<Item> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            MySqlParameter roomId = new MySqlParameter();
            roomId.ParameterName = "@roomId";
            roomId.Value = Id;
            cmd.Parameters.Add(roomId);

            Console.WriteLine("In Game.GetAllItems the room id is: " + Id );

            cmd.CommandText = @"SELECT * FROM contents WHERE rooms = @roomId;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {

              int itemId = rdr.GetInt32(2);
              Console.WriteLine("In Game.GetAllItems the item read is: " + Item.Find(itemId).GetName());
              // string itemName = rdr.GetString(1);
              // string itemType = rdr.GetString(2);
              // string itemSpecial = rdr.GetString(3);
              // bool itemMagic = rdr.GetBoolean(4);
              Item newItem = Item.Find(itemId);
              allItems.Add(newItem);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allItems;
        }

    }
}
