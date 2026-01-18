class Hostile : Character
{
    //Fields
    private string HostileName;
    private string HostileDescription;
    public string WeaponHostile { get; set; }
    //Constructor
    public Hostile(string name,string EquipedWeapon, int HP, string desc) : base(EquipedWeapon)//example Name: Bandit, Weapon: Axe, HP: 150, Desc: A bandit-like bandit
    {
        inventory= new Inventory(99);
        CurrentRoom=null;
        Health=HP;
        HostileDescription=desc;
        HostileName=name;
        WeaponHostile=EquipedWeapon;
    }
    //methods
    public string GetHostileName()=>HostileName;
}