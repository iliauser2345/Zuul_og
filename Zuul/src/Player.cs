using System.Runtime;
using System.Security.AccessControl;

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
}