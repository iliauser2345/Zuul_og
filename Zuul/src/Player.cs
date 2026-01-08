using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;

class Player
{
 // auto property
    public Room CurrentRoom { get; set; }
//fields
    public int health;
 // constructor
    public Player()
    {
        CurrentRoom = null;
        health=100;
    }
    public int damage(int amount)
    {
        int result= health-amount;
        return result;
    }
    public int heal(int amount)
    {
        int result=health+amount;
        if (result > 100)
        {
            return result-(result-100);
        }
        else
        {
            return result;
        }
    }
    public bool isalive(int health)
    {   
        bool stat=true; 
        if (health == 0)
        {
            stat=false;
        }else if (health != 0)
        {
            stat=true;
        }
        return stat; 
    }
    public void ModifierUponEnteranceRoom(Room enteredroom) //aplliance of modifiers to the player.Each is prescribed per room
    {
        string mod= enteredroom.modifier;
        int value= enteredroom.modifierValue;
        string modDescription=enteredroom.modifierDescpription;

        switch (mod)
        {
            case "heal":
            health=heal(value);
            Console.WriteLine(modDescription);
            break;
            case "damage":
            health=damage(value);
            Console.WriteLine(modDescription);
            break;
            case "none":
            Console.WriteLine(modDescription);
            break;
            
            //... add more mods here

        }
    }
}