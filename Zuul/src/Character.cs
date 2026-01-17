using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

class Character
{
 // auto property
   public Room CurrentRoom { get; set; }
//fields
    protected int Health;
    protected Inventory inventory;
    protected Item EquipedWeapon;

 // constructor
    public Character(string equipment)
    {   
       // inventory= new Inventory(25);
        CurrentRoom = null;
        EquipedWeapon=null;
    }
    public int GetHealth()=>Health;
    public Inventory GetInventory()=>inventory;
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
    public void ModifierAplication(Room enteredroom,Item item, string source,Character target) //ULTIMATE MODIFIER APPLIANCE METHOD from either a room or a use of item

    //!!!BY GIVING A ONE OF TWO CLASS PARAMETERS(ROOM OR ITEM) MAKE SURE TO SET THE NOT USED ONE TO null AND TARGET(Character class) IS REQUIRED!!!
    {
        string mod;
        int value;
        string modDescription;
        switch (source)
        {
            case "room"://From rooms
                mod= enteredroom.modifier;
                value= enteredroom.modifierValue;
                modDescription=enteredroom.modifierDescpription;
                switch (mod)
                {
                    case "ModifyHP":
                        target.HpModify(value);
                        Console.WriteLine(modDescription);
                        break;
                    case "none":
                        Console.WriteLine(modDescription);
                        break;
                    case "explored":
                        Console.WriteLine("(...)");
                        break;
                    
                    //... add more mods here

                }
                enteredroom.modifier="explored";
            break;
            case "item"://From items or weapons
                mod=item.ItemModifier;
                value=item.ItemModValue;
                modDescription=item.ItemModifierDescription;
                switch (mod)
                {
                    case "ModifyHP":
                        target.HpModify(value);
                        Console.WriteLine(modDescription);
                    break;
                    case "none":
                        Console.WriteLine(modDescription);
                    break;
                            
                            //... add more mods here

                }
            break;   
        }
        
    }
}