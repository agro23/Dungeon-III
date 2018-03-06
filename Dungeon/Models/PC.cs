using System.Collections.Generic;
using MySql.Data.MySqlClient;
// using MySQLCore.Models;
using System;
using Dungeon.Models;
using Dungeon;

namespace Dungeon.Models
{
    public class PC
    {
        private string _name;
        private string _type;
        private int _hp;
        private int _ac;
        private int _damage;
        private int _lvl;
        private int _exp;
        private int _roomId;
        private int _id;

        public PC(string Name, string Type = "Type", int HP = 0, int AC = 0, int Damage = 0, int LVL = 0, int EXP = 0, int RoomId = 0, int id = 0)
        {
            _name = Name;
            _type = Type;
            _hp = HP;
            _ac = AC;
            _damage = Damage;
            _lvl = LVL;
            _exp = EXP;
            _roomId = RoomId;
            _id = id;
        }

        public override bool Equals(System.Object otherPC)
        {
          if (!(otherPC is PC))
          {
            return false;
          }
          else
          {
             PC newPC = (PC) otherPC;
             bool idEquality = this.GetId() == newPC.GetId();
             bool nameEquality = this.GetName() == newPC.GetName();
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

        public string GetPCType()
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

        public int GetEXP()
        {
            return _exp;
        }

        public int GetRoomId()
        {
            return _roomId;
        }

        public void SetRoomId(int roomId)
        {
            _roomId = roomId;
        }

        public int GetId()
        {
            return _id;
        }

        public static string GetString()
        {
            return "this is a string from the model";
        }

        public static List<PC> GetAll()
        {
            List<PC> allPCs = new List<PC> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM pcs;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int pcId = rdr.GetInt32(0);
              string pcName = rdr.GetString(1);
              string pcType = rdr.GetString(2);
              int pcHP = rdr.GetInt32(3);
              int pcAC = rdr.GetInt32(4);
              int pcDamage = rdr.GetInt32(5);
              int pcLVL = rdr.GetInt32(6);
              int pcEXP = rdr.GetInt32(7);
              int pcRoomId = rdr.GetInt32(8);
              PC newPC = new PC(pcName, pcType, pcHP, pcAC, pcDamage, pcLVL, pcEXP, pcRoomId, pcId);
              allPCs.Add(newPC);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allPCs;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO pcs (name) VALUES (@name);";

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

        public void Update(string newName, string newType, int newHP, int newAC, int newDamage, int newLVL, int newEXP, int newRoomId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"UPDATE pcs SET name = @newName, type = @newType,
            hp = @newHP, ac = @newAC,
            damage = @newDamage, lvl = @newLVL,
            exp = @newEXP, room_id = @newRoomId
             WHERE id = @searchId; ";

            //search id
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

            //exp
            MySqlParameter exp = new MySqlParameter();
            exp.ParameterName = "@newEXP";
            exp.Value = newEXP;
            cmd.Parameters.Add(exp);

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

        public static PC Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM pcs WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int pcId = 0;
            string pcName = "";
            string pcType = "";
            int pcHP = 0;
            int pcAC = 0;
            int pcDamage = 0;
            int pcLVL = 0;
            int pcEXP = 0;
            int pcRoomId = 0;

            while(rdr.Read())
            {
              pcId = rdr.GetInt32(0);
              pcName = rdr.GetString(1);
              pcType = rdr.GetString(2);
              pcHP = rdr.GetInt32(3);
              pcAC = rdr.GetInt32(4);
              pcDamage = rdr.GetInt32(5);
              pcLVL = rdr.GetInt32(6);
              pcEXP = rdr.GetInt32(7);
              pcRoomId = rdr.GetInt32(8);
            }

            PC newPC = new PC(pcName, pcType, pcHP, pcAC, pcDamage, pcLVL, pcEXP, pcRoomId, pcId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newPC;
        }

        public void Delete()
        {
            // Delete PC entirely
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("DELETE FROM pcs WHERE id = @PCId; DELETE FROM inventory WHERE pc_id = @PCId;", conn);
            MySqlParameter pcIdParameter = new MySqlParameter();
            pcIdParameter.ParameterName = "@PCId";
            pcIdParameter.Value = this.GetId();

            cmd.Parameters.Add(pcIdParameter);
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
            cmd.CommandText = @"DELETE FROM PCs;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddItemToPC(Item newItem)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO inventory (pcs, items) VALUES (@PCId, @ItemId);";

            MySqlParameter pcs = new MySqlParameter();
            pcs.ParameterName = "@PCId";
            pcs.Value = _id;
            cmd.Parameters.Add(pcs);

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
            cmd.CommandText = @"SELECT items.* FROM pcs
              JOIN inventory ON (pcs.id = inventory.pcs)
              JOIN items ON (inventory.items = items.id)
              WHERE pcs.id = @PCId;";

            MySqlParameter pcIdParameter = new MySqlParameter();
            pcIdParameter.ParameterName = "@PCId";
            pcIdParameter.Value = _id;
            cmd.Parameters.Add(pcIdParameter);

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
