using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

class Player
{
 // auto property
    public Room CurrentRoom { get; set; }
//fields
    private int Health;
    private Inventory backpack;

 // constructor
    public Player()
    {   
        backpack= new Inventory(25);
        CurrentRoom = null;
        Health=10;
    }
    public int GetHealth()=>Health;
    public Inventory GetInventoryPlayer()=>backpack;
    public void HpModify(int amount)
    {
        Health=Health+amount;
    }
    public bool DeathCheck(int health)
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ModifierUponEnteranceRoom(Room enteredroom) //aplliance of modifiers to the player.Each is prescribed per room
    {
        string mod= enteredroom.modifier;
        int value= enteredroom.modifierValue;
        string modDescription=enteredroom.modifierDescpription;

        switch (mod)
        {
            case "ModifyHP":
            HpModify(value);
            Console.WriteLine(modDescription);
            break;
            case "none":
            Console.WriteLine(modDescription);
            break;
            
            //... add more mods here

        }
    }
}