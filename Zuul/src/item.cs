using System.ComponentModel;

class Item
{
    //fields
    public int Weight { get; }
    public string ItemDescription { get; }

    public string ItemType { get; }
    public string ItemModifier { get; }
    public string ItemModifierDescription { get; }
    public int ItemModValueMin { get; }
    public int ItemModValueMax{ get; }
    public string ItemName{get;}
    
    //constructor
    public Item(string name,int weight, string description, string type, string modifier,string moddesc, int minvalue,int maxvalue)
    {
        ItemName=name;
        Weight=weight;
        ItemDescription=description;
        ItemType=type;
        ItemModifier=modifier;
        ItemModifierDescription=moddesc;
        ItemModValueMin=minvalue;
        ItemModValueMax=maxvalue;
    }
}