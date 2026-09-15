using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public List<GameObject> bugPrefabList;
    public GameObject spawnLocationObject;
    public List<SpawnLocation> spawnLocationsList;

    void Start()
    {
        for (int i = 0; i < spawnLocationObject.transform.childCount; i++)
        {
            spawnLocationsList.Add(spawnLocationObject.transform.GetChild(i).GetComponent<SpawnLocation>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForRespawns();
    }

    void CheckForRespawns()
    {
        foreach (SpawnLocation spawnLocal in spawnLocationsList)
        {
            if (spawnLocal.respawnTimer < 0 && !spawnLocal.hasBug)
            {
                spawnLocal.SpawnBug(SelectBugToSpawn(spawnLocal));
            }
        }
    }
    
    GameObject SelectBugToSpawn(SpawnLocation spawnLocal)
    {
        Shuffle(bugPrefabList);
        foreach (GameObject bug in bugPrefabList)
        {
            Bug thisBug = bug.GetComponent<Bug>();
            int ranNum = Random.Range(1, 6);
            Debug.Log($"Trying to load {bug.name} and rolled {ranNum}");
            if (thisBug.spawnLocations.HasFlag(spawnLocal.spawnLocation) && thisBug.rarity <= ranNum) return bug;
        }

        return null;
    }

    // code provided by jasonmarziani on github - Fisher-Yates meathod
    void Shuffle(List<GameObject> a)
    {
        // Loops through array
        for (int i = a.Count - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            GameObject temp = a[i];

            // Swap the new and old values
            a[i] = a[rnd];
            a[rnd] = temp;
        }
    }

}
