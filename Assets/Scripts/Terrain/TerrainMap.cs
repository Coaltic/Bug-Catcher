using UnityEngine;

public class TerrainMap : MonoBehaviour
{
    public Vector3 terrainSize;
    public GameObject terrain;
    public Terrain terrainCom;
    void Start()
    {
        terrainCom = terrain.GetComponent<Terrain>();
        terrainSize = terrainCom.terrainData.size;
        Debug.Log($"Terrain Size: {terrainSize}");
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
