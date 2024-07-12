using System.Collections.Generic;
using UnityEngine;

public class NPCsManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
    [SerializeField] private List<NPCController> npcList = new List<NPCController>();
    [SerializeField] private NPCController npcPrefab;
    [SerializeField] private int npcCount = 10;
    [SerializeField] private float maxZOffset = 5f;
    [SerializeField] private float maxXOffset = 5f;

    private void Start()
    {
        SpawnNPCS();
    }

    private void SpawnNPCS()
    {
        if(npcList.Count <= 0)
        {
            for (int i = 0; i < npcCount; i++)
            {
                NPCController currentNPC = Instantiate(npcPrefab, RandomPosition(), Quaternion.identity);
                npcList.Add(currentNPC);
            }
        }
    }

    private Vector3 RandomPosition()
    {
        int randPos = Random.Range(0, spawnPositions.Count);
        Vector3 spawnPosition = spawnPositions[randPos].position;
        spawnPosition.x = Random.Range(-maxXOffset, maxXOffset);
        spawnPosition.z += Random.Range(-maxZOffset, maxZOffset);
        return spawnPosition;
    } 
}
