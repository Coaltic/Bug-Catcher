using UnityEngine;

public class Ladybug : Bug
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bugName = "Ladybug";
        rarity = 1;
        baseSize = 5;
        basePrice = 0.15f;

        CalculateSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
