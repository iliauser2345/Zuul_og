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
    // constructor
    public Player(Item EquipedWeapon): base(EquipedWeapon)
    {   
        inventory= new Inventory(25);
        CurrentRoom = null;
        Health=100;
        WeaponPlayer=EquipedWeapon;
    }
    //methods
}
   