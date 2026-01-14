class Inventory
{
    // fields
    private int maxWeight;
    private Dictionary<string, Item> items;
    // constructor
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }
    // methods
    public bool Put(string itemName, Item engagedItem)
    {
        int weight=engagedItem.Weight;
        bool succes; 
        if (weight <= FreeWeight())
        {
            items.Add(itemName,engagedItem);
            succes=true;
        }
        else
        {
            succes=false;
        }
        return succes;
    }
    public Item Get(string itemName)
    {
        if (items.ContainsKey(itemName) == true)
        {
            Item gotItem=items[itemName];
            items.Remove(itemName);
            return gotItem;
        }
        else
        {
            return null;
        }
    }
    public int TotalWeight()
    {
        int total;
        int i;
        for(
            total=0,
            i=0;
            i<items.Count;
            i++
        )
        {
            Item LoopThrItem=items.Values.ElementAt(i);
            total=+LoopThrItem.Weight;
        }
        return total;
    }
    public int FreeWeight()
    {
        return maxWeight-TotalWeight();
    }
}