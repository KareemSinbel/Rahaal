using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderCalc : MonoBehaviour
{
    private BoxCollider boxCollider;
    private List<Floater> floaters;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        floaters = new List<Floater>(GetComponentsInChildren<Floater>());
    }

    void Start()
    {  
        if (boxCollider != null)
        {
            // Get the size and center in local space
            Vector3 boxSize = boxCollider.size;
            Vector3 boxCenter = boxCollider.center;

            // Calculate the local positions for each edge
            Vector3 topLeftEdge = boxCenter + new Vector3(boxSize.x / 2, 0, boxSize.z/2);
            Vector3 topRightEdge = boxCenter + new Vector3(boxSize.x / 2, 0, -boxSize.z/2);
            Vector3 bottomLeftEdge = boxCenter + new Vector3(-boxSize.x / 2, 0, boxSize.z/2);
            Vector3 bottomRightEdge = boxCenter + new Vector3(-boxSize.x / 2, 0, -boxSize.z/2);

            // Assign each child object to the calculated edge positions in local space
            floaters[0].transform.localPosition = topLeftEdge;
            floaters[1].transform.localPosition = topRightEdge;
            floaters[2].transform.localPosition = bottomLeftEdge;
            floaters[3].transform.localPosition = bottomRightEdge;
        }
    }
}
