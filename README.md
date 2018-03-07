# Dungeon

#### By Andy Grossberg and Michael Albers

## Description
A Dungeon Crawling setup program using MySQL and many to many relationships.

## Rules

A project for the advanced database week of C#. Rules to follow.

## Specifications

* Load a database

* Create Objects for Room, Item, NPCs, PCs

* Connect to database

* Create GetAll() method for Room
  - EXPECTED INPUT: Rooms.GetAll()
  - EXPECTED OUTPUT: Empty List

* Create GetAll() method for Item
  - EXPECTED INPUT: Rooms.GetAll()
  - EXPECTED OUTPUT: Empty List

* Create GetAll() method for NPC
  - EXPECTED INPUT: Rooms.GetAll()
  - EXPECTED OUTPUT: Empty List

* Create GetAll() method for PC
  - EXPECTED INPUT: Rooms.GetAll()
  - EXPECTED OUTPUT: Empty List

* Test Equals method override for Rooms
  - EXPECTED INPUT: EntryWay in two Lists
  - EXPECTED OUTPUT: None.

* Test Equals method override for Items
  - EXPECTED INPUT: Torch in two Lists
  - EXPECTED OUTPUT: None.

* Test Equals method override for NPCs
  - EXPECTED INPUT: Orc in two Lists
  - EXPECTED OUTPUT: None.

* Test Equals method override for PCs
  - EXPECTED INPUT: Crom in two Lists
  - EXPECTED

* Test Save() method for Rooms
  - EXPECTED INPUT: EntryWay in two Lists
  - EXPECTED OUTPUT: None.

* Test Save() method for Items
  - EXPECTED INPUT: Torch in two Lists
  - EXPECTED OUTPUT: None.

* Test Save() method for NPCs
  - EXPECTED INPUT: Orc in two Lists
  - EXPECTED OUTPUT: None.

* Test Save() method for PCs
  - EXPECTED INPUT: Crom in two Lists
  - EXPECTED OUTPUT: None.

* Test Find() method for Rooms
  - EXPECTED INPUT: EntryWay
  - EXPECTED OUTPUT: None.

* Test AddItems() method for Items and Contents join table
  - EXPECTED INPUT: EntryWay, Sword, Torch
  - EXPECTED OUTPUT: None.

* Test Save and other functions.

* Build a feature to let users Create a Room and populate with:
  - Items
  - NPCs
  - PC(s)

* Build a feature to let users Create an Item and place it:
  - in a Room
  - in an NPC inventory
  - in a PC inventory

* Build a feature to let users Create an NPC, place it in a Room, and give it:
  - Items

* Build a feature to let users Create a PC, place it in a Room, and give it:
  - Items


* Create Views

* Refactor code as needed.

**March 5th Update as Dungeon II**

* Add ability to put Item in Room to Item Editor.

* Add Move

* Add Look

* Add Get /Drop

## Setup/Installation Requirements

* Clone the git repository from 'https://github.com/agro23/Dungeon.git'.
* Run the command 'dotnet restore' to download the necessary packages.
* Run the command 'dotnet build' to build to build the app.
* Run the command 'dotnet run' to run the server on localhost.
* Use your preferred web browser to navigate to localhost:5000

## Support and contact details

* Contact the authors at andy.grossberg@gmail.com

## Technologies Used

* C#
* Asp .NET Core 1.1 MVC
* HTML
* CSS
* Javascript
* Bootstrap
* JQuery

### License

Copyright (c) 2018 Andy Grossberg & Michael Albers

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
