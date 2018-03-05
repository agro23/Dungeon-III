using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Dungeon.Models;
using Dungeon;

namespace Dungeon.Models
{
    public class Item
    {
        private string _name;
        private string _type;
        private string _special;
        private bool _magic;
        private int _id;

        public Item(string Name, string Type = "Type", string Special = "Special", bool Magic = true, int id = 0)
        {
            _name = Name;
            _type = Type;
            _special = Special;
            _magic = Magic;
            _id = id;
        }

        public override bool Equals(System.Object otherItem)
        {
          if (!(otherItem is Item))
          {
            return false;
          }
          else
          {
             Item newItem = (Item) otherItem;
             bool idEquality = this.GetId() == newItem.GetId();
             bool nameEquality = this.GetName() == newItem.GetName();

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

        public string GetItemType()
        {
            return _type;
        }

        public string GetSpecial()
        {
            return _special;
        }

        public bool GetMagic()
        {
            return _magic;
        }

        public int GetId()
        {
            return _id;
        }

        public static string GetString()
        {
          return "this is a string from the model";
        }

        public static List<Item> GetAll()
        {
            List<Item> allItems = new List<Item> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM items;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int itemId = rdr.GetInt32(0);
              string itemName = rdr.GetString(1);
              string itemType = rdr.GetString(2);
              string itemSpecial = rdr.GetString(3);
              bool itemMagic = rdr.GetBoolean(4);
              Item newItem = new Item(itemName, itemType, itemSpecial, itemMagic, itemId);
              allItems.Add(newItem);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allItems;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO items (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update(string newName, string newType, string newSpecial, bool newMagic)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE items SET name = @newName, type = @newType, special = @newSpecial, magic = @newMagic WHERE id = @searchId;";

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

            // type
            MySqlParameter type = new MySqlParameter();
            type.ParameterName = "@newType";
            type.Value = newType;
            cmd.Parameters.Add(type);

            //special
            MySqlParameter special = new MySqlParameter();
            special.ParameterName = "@newSpecial";
            special.Value = newSpecial;
            cmd.Parameters.Add(special);

            //magic
            MySqlParameter magic = new MySqlParameter();
            magic.ParameterName = "@newMagic";
            magic.Value = newMagic;
            cmd.Parameters.Add(magic);

            cmd.ExecuteNonQuery();
            _name = newName;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Item Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM items WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int itemId = 0;
            string itemName = "";
            string itemType = "";
            string itemSpecial = "";
            bool itemMagic = false;

            while(rdr.Read())
            {
              itemId = rdr.GetInt32(0);
              itemName = rdr.GetString(1);
              itemType = rdr.GetString(2);
              itemSpecial = rdr.GetString(3);
              itemMagic = rdr.GetBoolean(4);
            }

            Item newItem = new Item(itemName, itemType, itemSpecial, itemMagic, itemId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newItem;
        }

        public void DeleteFrom(string joinTable)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("DELETE FROM @JoinTable WHERE itemId = @ItemId;", conn);
            MySqlParameter itemIdParameter = new MySqlParameter();
            itemIdParameter.ParameterName = "@ItemId";
            itemIdParameter.Value = this.GetId();

            MySqlParameter joinTableParameter = new MySqlParameter();
            joinTableParameter.ParameterName = "@JoinTable";
            joinTableParameter.Value = joinTable;

            cmd.Parameters.Add(itemIdParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }

        }

        public void Delete()
        {
            // Delete Item entirely
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("DELETE FROM items WHERE id = @ItemId; DELETE FROM contents WHERE items = @ItemId;", conn);
            MySqlParameter itemIdParameter = new MySqlParameter();
            itemIdParameter.ParameterName = "@ItemId";
            itemIdParameter.Value = this.GetId();

            cmd.Parameters.Add(itemIdParameter);
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
            cmd.CommandText = @"DELETE FROM items; DELETE FROM contents";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
