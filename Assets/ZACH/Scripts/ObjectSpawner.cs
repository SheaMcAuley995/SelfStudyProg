using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    List<GameObject> prefabList = new List<GameObject>();
    public GameObject rocketPart1, rocketPart2, /*rocketPart3, rocketPart4,*/ rocketPartsParent;

    public Vector3 center, size;

    int prefabIndex;

    [SerializeField] int totalRocketParts;

    void Start()
    {
        AddPrefabs();

        totalRocketParts = Random.Range(1, 5);

        for (int objectNumber = 0; objectNumber < totalRocketParts; objectNumber++)
        {
            SpawnObjects();
        }
    }

    public void SpawnObjects()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));

        prefabIndex = Random.Range(0, 2);

        Instantiate(prefabList[prefabIndex], pos, Quaternion.identity, rocketPartsParent.transform);
    }

    void AddPrefabs()
    {
        prefabList.Add(rocketPart1);
        prefabList.Add(rocketPart2);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
