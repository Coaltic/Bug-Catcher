using UnityEngine;

public class Bug : MonoBehaviour
{
    

    public SpawnLocation.SpawnLocationTypes spawnLocations;
    public string bugName;

    public int rarity; // 2^ of number of total bugs - 1,,, 

    void Start()
    {
        if (spawnLocations.HasFlag(SpawnLocation.SpawnLocationTypes.Tree))
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeCollected(Inventory inventory)
    {
        CollectedBug thisCollectedBug = new CollectedBug();
        thisCollectedBug.bugName = this.bugName;
        thisCollectedBug.rarity = this.rarity;

        inventory.CollectBug(thisCollectedBug);
    }
}
