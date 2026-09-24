using UnityEngine;

public class Caterpillar : Bug
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bugName = "Caterpillar";
        rarity = 2;
        baseSize = 15;
        basePrice = 0.3f;

        CalculateSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
