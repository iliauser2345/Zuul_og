using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

class Character
{
    Random rnd=new Random();
 // auto property
   public Room CurrentRoom { get; set; }
//fields
    protected int Health;
    protected Inventory inventory;

 // constructor
    public Character(Item equipment)
    {   
       // inventory= new Inventory(25);
        CurrentRoom = null;
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
    public int ModifierAplication(Room enteredroom,Item item, string source,Character target) //ULTIMATE MODIFIER APPLIANCE METHOD from either a room or a use of item

    //!!!BY GIVING A ONE OF TWO CLASS PARAMETERS(ROOM OR ITEM) MAKE SURE TO SET THE NOT USED ONE TO null AND TARGET(Character class) IS REQUIRED!!!
    {
        string mod;
        int valueMax;
        int valueMin;
        int roomValue;
        string modDescription;
        switch (source)
        {
            case "room"://From rooms
                mod= enteredroom.modifier;
                roomValue= enteredroom.modifierValue;
                modDescription=enteredroom.modifierDescpription;
                switch (mod)
                {
                    case "ModifyHP":
                        target.HpModify(roomValue);
                        Console.WriteLine(modDescription);
                        return roomValue;
                    case "none":
                        Console.WriteLine(modDescription);
                        break;
                    case "explored":
                        Console.WriteLine("(...)");
                        break;
                    
                    //... add more mods here

                }
                enteredroom.modifier="explored";
                return 0;

            case "item"://From items or weapons
                mod=item.ItemModifier;
                valueMax=item.ItemModValueMax;
                valueMin=item.ItemModValueMin;
                int resultValue=rnd.Next(valueMin,valueMax);
                modDescription=item.ItemModifierDescription;
                switch (mod)
                {
                    case "ModifyHP":
                        target.HpModify(resultValue);
                        Console.WriteLine(modDescription);
                    break;
                    case "none":
                        Console.WriteLine(modDescription);
                    break;
                            
                            //... add more mods here

                }
            return resultValue;
               
        }
        return 0;
        
    }
}