using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
Random rnd=new Random();
class Game
{
	Random rnd=new Random();
	// Private fields
	private Parser parser;
	private Player player;
	private int ActionPoints;
	public bool combat;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player(null);
		
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
		Item water= new Item("water",2,"a bottle of refreshing water, in case you are thirsty","utility","ModifyHp","You took a sip of water and did not notice how it ran out",5);
		Item whiskey= new Item("scotch",2,"a Good ol' bottle of scotch, dated 1968. You could almost say it's a treasure.","utility","ModifyHP","You have took a sip of a scotch. The taste of it was unimaginable however you refuse to drink more of it to preserve your perception ability",-3);
		Item knife= new Item("switchblade",1,"a fancy switchblade. God prohibits violence but you never know when you need it...","weapon","ModifyHP","it's not the size, it's how you wield it",rnd.Next(-7,-10));
		Item axe=new Item("fireaxe",5,"a fireaxe, usually found as a fire rescue tool but becomes a weapon to reckon with in good hands","weapon","ModifyHP","a powerfull swing of an axe makes the one enduring it actually feel",rnd.Next(-20,-30));

		// And add them to the Rooms
		lab.Chest.Put("scotch",whiskey);
		outside.Chest.Put("switchblade",knife);
		//Create Hostiles
		Hostile professor=new Hostile("Professor","fireaxe",100,"an underpaid professor who guards this university as a side hustle is not pleased with your presence...");
		//...
		//Assign a Hostile to the Room
		theatre.AddHostile("in a lecture theatre",professor); professor.GetInventory().Put("fireaxe",axe);
		//...

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
			dead=false,
			WinConAcomplished=false;
			!finished && !dead && !WinConAcomplished;
			dead=player.DeathCheck(player.GetHealth()),
			WinConAcomplished=WinConditionCheck()
		)
		{	
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
			if (combat == true)
			{
				CombatLoop(player,player.CurrentRoom.SummonHostile(player.CurrentRoom.GetShortDescription()));
				combat=false;
			}
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
			case "equip":
				EquipWeapon(command);
				break;
			case "unequip":
				UnequipWeapon(command);
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
		if (player.CurrentRoom.SummonHostile(player.CurrentRoom.GetShortDescription()) != null)
		{

			combat=true;
		}
		{
			
		}
		
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
				player.GetInventory().Put(itemName,item);
				break;
		}
		
	}
	private void EquipWeapon(Command command)
	{	
		if(!command.HasSecondWord())
		{
			// if there is no second word there's no weapon to equip
			Console.WriteLine("Equip what?");
			return;
		}
		string itemName=command.SecondWord;
		Item item=player.GetInventory().Get(itemName);
		if (item == null)
		{
			Console.WriteLine("There is no such weapon in your inventory");
			player.GetInventory().Put(itemName,item);
			return;
		}
		string typeItem=item.ItemType;
		if (typeItem != "weapon")
		{
			Console.WriteLine("can't fight with a "+itemName+" can you?");
			player.GetInventory().Put(itemName,item);
			return;
		}
		if (player.WeaponPlayer != null)
		{
			Console.WriteLine("Your hands are busy with a "+player.WeaponPlayer);
			player.GetInventory().Put(itemName,item);
			return;
		}
		player.WeaponPlayer=itemName;
		Console.WriteLine("Now you are armed and dangerous with a "+itemName);
		player.GetInventory().Put(itemName,item);
	}
	private void UnequipWeapon(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word there's no weapon to equip
			Console.WriteLine("Unequip what?");
			return;
		}
		Item item=player.GetInventory().Get(command.SecondWord);
		player.WeaponPlayer=null;
		Console.WriteLine("Succesfully unequiped a "+item.ItemName);
		player.GetInventory().Put(item.ItemName,item);
	}
	//#########################
	//COMBAT RELATED STUFF
	//#########################
	private bool PlayerHasInitiative()
	{
		int initiative=rnd.Next(0,2);
		return initiative switch
		{
			1=>true,
			0=>false,
			_=>false
		};
	}
	private bool ProcessCombatCommand(Command command,Character target)
	{
		if(command.IsUnknown())
		{
			Console.WriteLine("[ !WRONG COMMAND! ]");
		 	return false;
		}
		switch (command.CommandWord)
		{
			case "attack":
				Attack(target);
				break;
			case "defend":
				Defend();
				break;
		}
		return false;
	}
	private void Attack(Character target)
	{
		if (ActionPoints >= 1)
		{
			Item item=player.GetInventory().Get(player.WeaponPlayer);
			player.ModifierAplication(null,item,"item",target);
			ActionPoints=-1;
			player.GetInventory().Put(item.ItemName,item);
		}
	}
	private bool Defend()
	{
		if (ActionPoints >= 2)
		{	
			ActionPoints=-2;
			return true;
		}
		return false;
	}
	private void PlayerCombatSeq(Hostile hostile)
	{
		Console.WriteLine("ACT!\n");
		bool error=false;
		for(int i = 0; i < ActionPoints; i++)
		{
			Console.WriteLine("[ "+i.ToString()+" / "+ActionPoints.ToString()+" ] Action Points used");
			parser.PrintCombatCommands();
			Command command=parser.GetCombatCommand();
			error=ProcessCombatCommand(command,hostile);
			if (error==true){ i=-1; }
					
		}
	}
	private void HostileCombatSeq(Hostile hostile)
	{
		Console.WriteLine(hostile.GetHostileName()+" ATTACKS!\n");
		int succes=rnd.Next(0,3);
		if (succes == 0)
		{
			Item item=hostile.GetInventory().Get(hostile.WeaponHostile);
			hostile.ModifierAplication(null,item,"source",player);
			Console.WriteLine("You endured "+item.ItemModifierDescription+" [ "+item.ItemModValue+" of damage sustained]");
			hostile.GetInventory().Put(item.ItemName,item);
		}
		else
		{
			Console.WriteLine(hostile.GetHostileName()+" MISSES!");
		}
	}
	//COMBAT LOOP
	private void CombatLoop(Player player, Hostile hostile)
	{

		//-----------INITIALYSING COMBAT-------------------------
		bool DeathPlayer=false;
		bool DeathHostile=false;
		Console.WriteLine("---!!!-ENTERING COMBAT-!!!---\n\n");
		ActionPoints=2;
		bool PlayerBegins=PlayerHasInitiative();
		string initvMsg=(!PlayerBegins)?"---ENEMY gets the initiative, meaning, you get to act SECOND---\n\n":"---YOU get the initiative, meaning, you get to act FIRST---\n\n";
		Console.WriteLine(initvMsg);

		//----------MAIN TURN SEQUENCE---------------------------
		for(
			int turn=1;
			!DeathPlayer && !DeathHostile;
			turn++
		)
		{
			Console.WriteLine("--[TURN: "+turn.ToString()+" ]--\n\n");
			//--------SECONDARY TURN SEQUENCE----(action seq inside 1 turn)--------------
			if (PlayerBegins == true)
			{//if player has initiative, player begins, else enemy begins
				PlayerCombatSeq(hostile);
				HostileCombatSeq(hostile);
			}
			else
			{	//Enemy action
				HostileCombatSeq(hostile);
				PlayerCombatSeq(hostile);
				
			}

			//-----------END TURN SEQUENCE------------------------------
			int playerHP=player.GetHealth();
			int hostileHP=hostile.GetHealth();//getting both combatants hp
			Console.WriteLine("Your HP: "+playerHP.ToString());
			Console.WriteLine(hostile.GetHostileName()+"'s HP: "+hostileHP.ToString());// letting player know
			DeathPlayer=player.DeathCheck(playerHP);
			DeathHostile=player.DeathCheck(hostileHP);//checking if any of combatants are dead
			
		}
	}
}
