using System.Collections.Generic;
using MySql.Data.MySqlClient;
// using MySQLCore.Models;
using System;
using Dungeon.Models;
using Dungeon;

namespace Dungeon.Models
{
    public class NPC
    {
        private string _name;
        private string _type;
        private int _hp;
        private int _ac;
        private int _damage;
        private int _lvl;
        private int _roomId;
        private int _id;

        public NPC(string name, string type = "empty", int HP = 0, int AC = 0, int damage = 0, int lvl = 0, int roomId = 0, int id = 0)
        {
            _name = name;
            _type = type;
            _hp = HP;
            _ac = AC;
            _damage = damage;
            _lvl = lvl;
            _roomId = roomId;
            _id = id;
        }

        public override bool Equals(System.Object otherNPC)
        {
          if (!(otherNPC is NPC))
          {
            return false;
          }
          else
          {
             NPC newNPC = (NPC) otherNPC;
             bool idEquality = this.GetId() == newNPC.GetId();
             bool nameEquality = this.GetName() == newNPC.GetName();
             return (idEquality && nameEquality);
           }
        }
        public override int GetHashCode()
        {
             return this.GetName().GetHashCode();
        }

        public string GetName()
        {
            return _name;
        }

        public string GetNPCType()
        {
          return _type;
        }

        public int GetHP()
        {
            return _hp;
        }

        public int GetAC()
        {
            return _ac;
        }

        public int GetDamage()
        {
            return _damage;
        }

        public int GetLVL()
        {
            return _lvl;
        }

        public int GetRoomId()
        {
            return _roomId;
        }

        public int GetId()
        {
            return _id;
        }

        public static string GetString()
        {
            return "this is a string from the model";
        }

        public static List<NPC> GetAll()
        {
            List<NPC> allNPCs = new List<NPC> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM npcs;";
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

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO npcs (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            // Code to declare, set, and add values to a categoryId SQL parameters has also been removed.

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update(string newName, string newType, int newHP, int newAC, int newDamage, int newLVL, int newRoomId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE npcs SET name = @newName, type = @newType, hp = @newHP, ac = @newAC, damage = @newDamage, lvl = @newLVL, room_id = @newRoomId WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            //name
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            //type
            MySqlParameter type = new MySqlParameter();
            type.ParameterName = "@newType";
            type.Value = newType;
            cmd.Parameters.Add(type);

            //hp
            MySqlParameter hp = new MySqlParameter();
            hp.ParameterName = "@newHP";
            hp.Value = newHP;
            cmd.Parameters.Add(hp);

            //ac
            MySqlParameter ac = new MySqlParameter();
            ac.ParameterName = "@newAC";
            ac.Value = newAC;
            cmd.Parameters.Add(ac);

            //damage
            MySqlParameter damage = new MySqlParameter();
            damage.ParameterName = "@newDamage";
            damage.Value = newDamage;
            cmd.Parameters.Add(damage);

            //lvl
            MySqlParameter lvl = new MySqlParameter();
            lvl.ParameterName = "@newLVL";
            lvl.Value = newLVL;
            cmd.Parameters.Add(lvl);

            //room id
            MySqlParameter roomId = new MySqlParameter();
            roomId.ParameterName = "@newRoomId";
            roomId.Value = newRoomId;
            cmd.Parameters.Add(roomId);


            cmd.ExecuteNonQuery();
            _name = newName;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static NPC Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM npcs WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int npcId = 0;
            string npcName = "";
            string npcType = "";
            int npcHP = 0;
            int npcAC = 0;
            int npcDamage = 0;
            int npcLVL = 0;
            int npcRoomId = 0;

            while(rdr.Read())
            {
              npcId = rdr.GetInt32(0);
              npcName = rdr.GetString(1);
              npcType = rdr.GetString(2);
              npcHP = rdr.GetInt32(3);
              npcAC = rdr.GetInt32(4);
              npcDamage = rdr.GetInt32(5);
              npcLVL = rdr.GetInt32(6);
              npcRoomId = rdr.GetInt32(7);
            }

            NPC newNPC = new NPC(npcName, npcType, npcHP, npcAC, npcDamage, npcLVL, npcRoomId, npcId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newNPC;
        }

        public void Delete()
        {
            // Delete NPC entirely
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("DELETE FROM npcs WHERE id = @NPCId; DELETE FROM loot WHERE npc_id = @NPCId;", conn);
            MySqlParameter npcIdParameter = new MySqlParameter();
            npcIdParameter.ParameterName = "@NPCId";
            npcIdParameter.Value = this.GetId();

            cmd.Parameters.Add(npcIdParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM NPCs;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddItemToNPC(Item newItem)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO loot (npcs, items) VALUES (@NPCId, @ItemId);";

            MySqlParameter npcs = new MySqlParameter();
            npcs.ParameterName = "@NPCId";
            npcs.Value = _id;
            cmd.Parameters.Add(npcs);

            MySqlParameter items = new MySqlParameter();
            items.ParameterName = "@ItemId";
            items.Value = newItem.GetId();
            cmd.Parameters.Add(items);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Item> GetItems()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT items.* FROM npcs
              JOIN loot ON (npcs.id = loot.npcs)
              JOIN items ON (loot.items = items.id)
              WHERE npcs.id = @NPCId;";

            MySqlParameter npcIdParameter = new MySqlParameter();
            npcIdParameter.ParameterName = "@NPCId";
            npcIdParameter.Value = _id;
            cmd.Parameters.Add(npcIdParameter);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Item> items = new List<Item>{};

            while(rdr.Read())
            {
              int itemId = rdr.GetInt32(0);
              string itemName = rdr.GetString(1);
              string itemType = rdr.GetString(2);
              string itemSpecial = rdr.GetString(3);
              bool itemMagic = rdr.GetBoolean(4);
              Item newItem = new Item(itemName, itemType, itemSpecial, itemMagic, itemId);
              items.Add(newItem);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        return items;
        }

    }
}
