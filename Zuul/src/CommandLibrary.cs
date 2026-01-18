using System.Collections.Generic;

class CommandLibrary
{
	// A List that holds all valid command words
	private readonly List<string> validCommands;
	private readonly List<string> combatCommands; //only accessible in combat

	// Constructor - initialise the command words.
	public CommandLibrary()
	{
		validCommands = new List<string>();

		validCommands.Add("help");
		validCommands.Add("go");
		validCommands.Add("quit");
		validCommands.Add("look");
		validCommands.Add("stats");
		validCommands.Add("inventory");
		validCommands.Add("grab");
		validCommands.Add("drop");
		validCommands.Add("use");
		validCommands.Add("equip");
		validCommands.Add("unequip");
		//combat commands
		combatCommands=new List<string>();

		combatCommands.Add("attack");
		combatCommands.Add("defend");
	}

	// Check whether a given string is a valid command word.
	// Return true if it is, false if it isn't.
	public bool IsValidCommandWord(string instring)
	{
		return validCommands.Contains(instring);
	}
	//Checks a command in case of combat, returns true or false
	public bool IsValidCombatCommandWord(string instring)
	{
		return combatCommands.Contains(instring);
	}

	// returns a list of valid command words as a comma separated string.
	public string GetCommandsString()
	{
		return String.Join(", ", validCommands);
	}
	//returns a list of valid combat commands as a "menu" when in combat.
	public string GetCombatCommandString()
	{
		return String.Join(" ] [ ",combatCommands);
	}
}
