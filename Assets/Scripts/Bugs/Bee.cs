using UnityEngine;

public class Bee : Bug
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bugName = "Bumble Bee";
        rarity = 3;
        baseSize = 10;
        basePrice = 0.4f;

        CalculateSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
