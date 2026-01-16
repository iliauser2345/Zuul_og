using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

class Player : Character
{
//fields
// constructor
    public Player()
    {   
        inventory= new Inventory(25);
        CurrentRoom = null;
        Health=100;
    }
//methods
}
   