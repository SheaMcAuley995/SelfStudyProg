using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFinder : MonoBehaviour {

    public static PositionFinder Instance;

    public LayerMask layerMask;
    public Vector3 area;
    public int seed;

    [Range(1, 50)]
    public int locationDensity;
    // Use this for initialization

    public Transform positionParent;
    List<Transform> nodes;
    List<Transform> gizNodes;
   // private int currentNode = 0;

    public List<Vector3> applicablePositions;

    private void Awake()
    {
        
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        Transform[] posTransforms = positionParent.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for(int i = 1; i < posTransforms.Length; i++ )
        {
            nodes.Add(posTransforms[i]);
        }
        foreach(Transform node in nodes)
        {
            spawnObject(node);
        }
    }

    private void spawnObject(Transform node)
    {
        for(int i = 0; i < locationDensity; i++)
        {
            Vector3 pos = node.position + new Vector3(Random.Range(-area.x / 2, area.x / 2), Random.Range(-area.y / 2, area.y / 2), Random.Range(-area.z / 2, area.z / 2));

            Ray ray = new Ray(pos, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
            {
                applicablePositions.Add(hit.point);
            }
        }
        
    }


    public Vector3 giveNewPosition()
    {
        return applicablePositions[Random.Range(0,applicablePositions.Count)];
    }


   private void OnDrawGizmos()
   {
       Gizmos.color = Color.red;
       Transform[] pathTransforms = GetComponentsInChildren<Transform>();
       gizNodes = new List<Transform>();
       for (int i = 0; i < pathTransforms.Length; i++)
       {
           if (pathTransforms[i] != transform)
           {
             
               gizNodes.Add(pathTransforms[i]);
               Gizmos.DrawRay(pathTransforms[i].position, Vector3.down * 30);
           }
       }
       for (int i = 0; i < gizNodes.Count; i++)
       {

           Vector3 currentNode = gizNodes[i].position;
           Vector3 previousNode = Vector3.zero;
           if (i > 0)
           {
               Gizmos.DrawWireSphere(currentNode, 1);
               previousNode = gizNodes[i - 1].position;
           }
           else if (i == 0 && gizNodes.Count > 1)
           {
               Gizmos.DrawWireSphere(currentNode, 1);
               previousNode = gizNodes[gizNodes.Count - 1].position;
           }
         
           Gizmos.DrawLine(previousNode, currentNode);
       }

       Gizmos.color = Color.green;
       foreach(var pos in applicablePositions)
       {
           Gizmos.DrawWireSphere(pos, 0.75f);
       }
   }
}
