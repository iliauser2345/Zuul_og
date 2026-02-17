using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

class Player : Character
{
    //fields
    public bool IsDefending {get;set;}
    public Item WeaponPlayer { get; set; }
    public int ActionPoints {get; set;}
    public bool Engaged {get; set;}
   // private Dictionary<string,Item> PlayerEquipment;
    // constructor
    public Player(Item EquipedWeapon): base(EquipedWeapon)
    {   
        inventory= new Inventory(25);
        CurrentRoom = null;
        Health=100;
        WeaponPlayer=EquipedWeapon;
        Engaged=false;
        ActionPoints=2;
    }
    //methods
}
   