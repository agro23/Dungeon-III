using System.Collections.Generic;
using MySql.Data.MySqlClient;
// using MySQLCore.Models;
using System;
using Dungeon.Models;
using Dungeon;

namespace Dungeon.Models
{

    public class Room
    {
        private string _name;
        private string _shortDescription;
        private string _fullDescription;
        private bool _light;
        private string _commands;
        private int _id;
        // We no longer declare _RoomId here

        public Room(string name, string shortDescription="", string fullDescription="", bool light=true, string commands="", int id = 0)
        {
            _name = name;
            _shortDescription = shortDescription;
            _fullDescription = fullDescription;
            _light = light;
            _commands = commands;
            _id = id;
        }

        public override bool Equals(System.Object otherRoom)
        {
          if (!(otherRoom is Room))
          {
            return false;
          }
          else
          {
             Room newRoom = (Room) otherRoom;
             bool idEquality = this.GetId() == newRoom.GetId();
             bool nameEquality = this.GetName() == newRoom.GetName();
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

        public string GetShortDescription()
        {
            return _shortDescription;
        }

        public string GetFullDescription()
        {
            return _fullDescription;
        }

        public bool GetLight()
        {
            return _light;
        }

        public string GetCommands()
        {
            return _commands;
        }

        public int GetId()
        {
            return _id;
        }

        public static List<Room> GetAll()
        {
            List<Room> allRooms = new List<Room> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM rooms;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int roomId = rdr.GetInt32(0);
              string roomName = rdr.GetString(1);
              string roomShortDescription = rdr.GetString(2);
              string roomFullDescription = rdr.GetString(3);
              bool roomLight = rdr.GetBoolean(4);
              string roomCommands = rdr.GetString(5);
              Room newRoom = new Room(roomName, roomShortDescription, roomFullDescription, roomLight, roomCommands, roomId);
              allRooms.Add(newRoom);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allRooms;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO rooms (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            // Code to declare, set, and add values to a roomId SQL parameters has also been removed.

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update(string newName, string newShortDescription, string newFullDescription, bool newLight, string newCommands, int id = 0)
        {

            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            // cmd.CommandText = @"UPDATE rooms SET name = @newName WHERE id = @searchId;";

            cmd.CommandText = @"UPDATE rooms SET name = @newName,
            short_description = @newShortDescription,
            full_description = @newFullDescription,
            light = @newLight,
            commands = @newCommands
             WHERE id = @searchId;";

             // search id
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            // name
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            // short description
            MySqlParameter shortDescription = new MySqlParameter();
            shortDescription.ParameterName = "@newShortDescription";
            shortDescription.Value = newShortDescription;
            cmd.Parameters.Add(shortDescription);

            // full description
            MySqlParameter fullDescription = new MySqlParameter();
            fullDescription.ParameterName = "@newFullDescription";
            fullDescription.Value = newFullDescription;
            cmd.Parameters.Add(fullDescription);

            // light
            MySqlParameter light = new MySqlParameter();
            light.ParameterName = "@newLight";
            if (newLight)
            {
                light.Value = 1;
            }
            else
            {
                light.Value = 0;
            }
            cmd.Parameters.Add(light);

            // commands
            MySqlParameter commands = new MySqlParameter();
            commands.ParameterName = "@newCommands";
            commands.Value = newCommands;
            cmd.Parameters.Add(commands);

            cmd.ExecuteNonQuery();
            _name = newName;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Room Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `rooms` WHERE id = (@searchId);";
            // cmd.CommandText = @"SELECT * FROM `rooms` WHERE id = 3;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int roomId = 0;
            string roomName = "";
            string roomShortDescription = "";
            string roomFullDescription = "";
            bool roomLight = false; // let's see what happens
            string roomCommands = "";

            while(rdr.Read())
            {
              roomId = rdr.GetInt32(0);
              roomName = rdr.GetString(1);
              roomShortDescription = rdr.GetString(2);
              roomFullDescription = rdr.GetString(3);
              roomLight = rdr.GetBoolean(4);
              roomCommands = rdr.GetString(5);
            }

            Room newRoom = new Room(roomName, roomShortDescription, roomFullDescription, roomLight, roomCommands, roomId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newRoom;
        }

        public void Delete()
        // Delete's the room
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("DELETE FROM rooms WHERE id = @RoomId; DELETE FROM contents WHERE room_id = @RoomId;", conn);
            MySqlParameter roomIdParameter = new MySqlParameter();
            roomIdParameter.ParameterName = "@RoomId";
            roomIdParameter.Value = this.GetId();

            cmd.Parameters.Add(roomIdParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public void AddItemToRoom(Item newItem)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO contents (rooms, items) VALUES (@RoomId, @ItemId);";

            MySqlParameter rooms = new MySqlParameter();
            rooms.ParameterName = "@RoomId";
            rooms.Value = _id;
            cmd.Parameters.Add(rooms);

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
            cmd.CommandText = @"SELECT items.* FROM rooms
              JOIN contents ON (rooms.id = contents.rooms)
              JOIN items ON (contents.items = items.id)
              WHERE rooms.id = @RoomId;";

            MySqlParameter roomIdParameter = new MySqlParameter();
            roomIdParameter.ParameterName = "@RoomId";
            roomIdParameter.Value = _id;
            cmd.Parameters.Add(roomIdParameter);

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

        public static void DeleteAll()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"DELETE FROM rooms; DELETE FROM contents;";
          cmd.ExecuteNonQuery();
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }

    }
}
