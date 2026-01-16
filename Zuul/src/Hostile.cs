class Hostile : Character
{
    //Fields
    private Item WeaponEnemy;
    private string HostileName;
    private string HostileDescription;
    //Constructor
    public Hostile(string name,string weapon, int HP, string desc) //example Name: Bandit, Weapon: Axe, HP: 150, Desc: A bandit-like bandit
    {
        inventory= new Inventory(99);
        CurrentRoom=null;
        Health=HP;
        HostileDescription=desc;

        HostileName=name;
        WeaponEnemy=inventory.Get(weapon);
    }
    //methods
    
}