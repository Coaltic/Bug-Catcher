using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    [System.Flags]
    public enum SpawnLocationTypes
    {
        None = 0,
        Tree = 1 << 0, // 1
        Bush = 1 << 1, // 2
        Rock = 1 << 2, // 4
        River = 1 << 3,  // 8
        Ground = 1 << 4 // 16
    }

    public float respawnTimerMax;
    public float respawnTimer;

    public SpawnLocationTypes spawnLocation;

    public bool hasBug;

    void Start()
    {
        // SetTimer();
        respawnTimer = respawnTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBug)
        {
            respawnTimer -= Time.deltaTime;
            // if (respawnTimer < 0) SpawnBug();
        }
    }

    public void SpawnBug(GameObject newBug)
    {
        Instantiate(newBug, this.transform);
        this.hasBug = true;
    }

    public void SetTimer()
    {
        switch (spawnLocation)
        {
            case SpawnLocationTypes.Tree:
                respawnTimerMax = Random.Range(3f, 10f);
                hasBug = false;
                respawnTimer = respawnTimerMax;
                Debug.Log("Set timer for Tree");
                break;
            case SpawnLocationTypes.Bush:
                respawnTimerMax = Random.Range(3f, 10f);
                respawnTimer = respawnTimerMax;
                Debug.Log("Set timer for Bush");
                hasBug = false;
                break;
            case SpawnLocationTypes.Rock:
                respawnTimerMax = Random.Range(3f, 10f);
                respawnTimer = respawnTimerMax;
                hasBug = false;
                Debug.Log("Set timer for Rock");
                break;
            case SpawnLocationTypes.River:
                respawnTimerMax = Random.Range(3f, 10f);
                respawnTimer = respawnTimerMax;
                hasBug = false;
                Debug.Log("Set timer for River");
                break;
            case SpawnLocationTypes.Ground:
                respawnTimerMax = Random.Range(3f, 10f);
                respawnTimer = respawnTimerMax;
                hasBug = false;
                Debug.Log("Set timer for Ground");
                break;
        }
    }
}
