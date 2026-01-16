using System;
using System.Runtime.CompilerServices;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;
	private Hostile enemy;


	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		
		CreateRooms();
	}
	private bool WinConditionCheck()
	{
		return false;
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university","none",0,"You feel a cool breeze as you observe leaves falling from the trees[no effect]"); // HOME/ start/ pochatok
		Room outsideUp= new Room("on a rooftop of the university","ModifyHP",-10,"on your way to climbing the building you have slipped and fell enduring a fall damage, however from the second try you succeded![10 HP of damage sustained]");
		Room outsideDown= new Room("at a basement","ModifyHP",-10,"BOOBY TRAP! you have been hit by a hidden explosive device, however you have found cover behind a pillar what prevented you from sustaining major wounds[10 HP of damage dealt]");

		Room theatre = new Room("in a lecture theatre","ModifyHP",5,"Room featured a sign with a motivational quote, you feel inspired which gives you a strange feeling of.... healing[5 HP received]");


		Room pub = new Room("in the campus pub","ModifyHP",-3,"After a refreshing pint of beer you feel pleased, however your liver did not approve[3 HP of damage received]");


		Room lab = new Room("in a computing lab","none",0,"Finally at home, with computers[no effect]");


		Room office = new Room("in the computing admin office","ModifyHP",0,"Ugh...Office, you are suffering from boredom. emotional damage sustained[0 HP of damage recieved]");

		player.CurrentRoom=outside; //Start Point
		

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);
		outside.AddExit("up", outsideUp); outsideUp.AddExit("down",outside);
		outside.AddExit("down",outsideDown); outsideDown.AddExit("up",outside);
		

		theatre.AddExit("west", outside);

		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);

		//Room events/modifiers


		// Create Items
		Item water= new Item("Water",2,"a bottle of refreshing water, in case you are thirsty","utility","ModifyHp","You took a sip of water and did not notice how it ran out",5);
		Item whiskey= new Item("Whiskey",2,"a Good ol' bottle of scotch, dated 1968. You could almost say it's a treasure.","utility","ModifyHP","You have took a sip of a scotch. The taste of it was unimaginable however you refuse to drink more of it to preserve your perception ability",3);
		/////////////////////player.GetInventoryPlayer().Put("Water", water);
		// And add them to the Rooms
		lab.Chest.Put("Whiskey",whiskey);

		// Start game outside
		player.CurrentRoom = outside;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished;
		bool dead;
		bool WinConAcomplished;
		for(
			finished=false,
			dead=false,WinConAcomplished=false;
			!finished && !dead && !WinConAcomplished;
			dead=player.DeathCheck(player.GetHealth()),
			WinConAcomplished=WinConditionCheck()
		)
		{	
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		if (player.DeathCheck(player.GetHealth()) == true)
		{
			Console.WriteLine("You have Perished");
			Console.WriteLine("Press [Enter] to continue.");
			Console.ReadLine();
		}
		else if (WinConAcomplished == true)
		{
			Console.WriteLine("WIN MSG");
			Console.WriteLine("Press [Enter] to continue.");
			Console.ReadLine();
		}
		else
		{
			Console.WriteLine("Thank you for playing.");
			Console.WriteLine("Press [Enter] to continue.");
			Console.ReadLine();
		}
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("You get confused while figuring out what are you going to do[ !WRONG COMMAND! try again or type help ]");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				Console.WriteLine(player.CurrentRoom.GetLongDescription()+"\n"+player.CurrentRoom.Chest.ShowListOfItems(player.CurrentRoom.Chest,"Here you found:"));
				break;
			case "stats":
				Console.WriteLine("HP: "+player.GetHealth());
				break;
			case "inventory":
				Console.WriteLine(player.GetInventory().ShowListOfItems(player.GetInventory(),"INVENTORY"));
				break;
			case "grab":
				Grab(command);
				break;
			case "drop":
				Drop(command);
				break;
			case "use":
				UseItem(command);
				break;

		}

		return wantToQuit;
	}


	// ######################################
	// implementations of user commands:
	// ######################################
	
	//Grab item
	public void Grab(Command command)
	{
		string itemName=command.SecondWord;
		Item item=player.CurrentRoom.Chest.Get(itemName);
		if (item != null)
		{
			player.GetInventory().Put(itemName,item);
			Console.WriteLine("You have succesfully retrieved: "+item.ItemName);
		}
		else
		{
			Console.WriteLine("After a several time of search you have failed to find "+itemName);
		}

	}
	// Drop item
	public void Drop(Command command)
	{
		string itemName=command.SecondWord;
		Item item=player.GetInventory().Get(itemName);
		if (item != null)
		{
			player.CurrentRoom.Chest.Put(itemName,item);
			Console.WriteLine("You have decided that "+item.ItemName+" won't be needed anymore and left it behind");
		}
		else
		{
			Console.WriteLine("You are suprised to find out that you didn't have "+item.ItemName+" at the first place. Nevermind then");
		}
		
	}
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		player.CurrentRoom = nextRoom;
		player.ModifierAplication(player.CurrentRoom,null,"room",player);
		
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
		
	}
	private void UseItem(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word there's no item to use
			Console.WriteLine("Which item?");
			return;
		}
		string itemName=command.SecondWord;
		Item item=player.GetInventory().Get(itemName);
		if (item == null)
		{
			Console.WriteLine("There is no such item in your inventory");
			return;
		}
		string typeItem=item.ItemType;
		switch (typeItem)
		{
			case "utility":
				player.ModifierAplication(null,item,"item",player);
				break;
			case "weapon":
				Console.WriteLine("You have to equip it in order to use");
				break;
		}
		
	}
}
