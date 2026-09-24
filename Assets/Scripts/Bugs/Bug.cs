using UnityEngine;

public class Bug : MonoBehaviour
{
    

    public SpawnLocation.SpawnLocationTypes spawnLocations;
    public string bugName;
    public int rarity; // 2^ of number of total bugs - 1,,, 
    public float basePrice;
    public float bugPrice;
    public float baseSize;
    public float bugSize;
    public Sprite bugSprite;

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

    public void CalculateSize()
    {
        float rnd = Random.Range(0.1f, 3f);
        bugSize = (float)System.Math.Round(baseSize * rnd, 2);
        CalculatePrice(rnd);
    }

    public void CalculatePrice(float rnd)
    {
        bugPrice = (float)System.Math.Round(basePrice * rnd, 2);
    }

    public void BeCollected(Inventory inventory)
    {
        CollectedBug thisCollectedBug = new CollectedBug();
        thisCollectedBug.bugType = this;
        thisCollectedBug.bugName = this.bugName;
        thisCollectedBug.rarity = this.rarity;
        thisCollectedBug.bugSize = this.bugSize;
        thisCollectedBug.sellPrice = this.bugPrice;
        thisCollectedBug.bugSprite = this.bugSprite;

        inventory.CollectBug(thisCollectedBug);
    }
}
