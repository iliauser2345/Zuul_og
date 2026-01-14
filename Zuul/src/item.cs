using System.ComponentModel;

class Item
{
    //fields
    public int Weight { get; }
    public string ItemDescription { get; }

    public string ItemType { get; }
    public string ItemModifier { get; }
    public int ItemModValue { get; }
    
    //constructor
    public Item(int weight, string description, string type, string modifier, int value)
    {
        Weight=weight;
        ItemDescription=description;
        ItemType=type;
        ItemModifier=modifier;
        ItemModValue=value;
    }
}