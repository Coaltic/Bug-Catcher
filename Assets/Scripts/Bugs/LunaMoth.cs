using UnityEngine;

public class LunaMoth : Bug
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bugName = "Luna Moth";
        rarity = 5;
        baseSize = 75;
        basePrice = 1.5f;

        CalculateSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
