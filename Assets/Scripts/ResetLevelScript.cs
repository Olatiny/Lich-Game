using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevelScript : MonoBehaviour
{
    [SerializeField] GameObject foyer;
    [SerializeField] GameObject spawnPoint;

    public void ResetLevel()
    {
        GameObject[] LevelSegments = GameObject.FindGameObjectsWithTag("LevelSegment");

        for (int i = 0; i < LevelSegments.Length; i++)
        {
            Destroy(LevelSegments[i]);
        }

        Instantiate(foyer, spawnPoint.transform.position, foyer.transform.rotation);
    }
}
