using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyFloor : MonoBehaviour
{
    public DirtPocket dirtPacketPrefab;
    public Vector2Int size;
    public Vector2 cellSize;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = -size.x / 2; x <= size.x / 2; x++)
        {
            for (int y = -size.y / 2; y <= size.y / 2; y++)
            {
                if (Random.Range(0f, 1) > .5)
                    Instantiate(dirtPacketPrefab, new Vector2(x * cellSize.x, y * cellSize.y), Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        for (int x = -size.x / 2; x <= size.x / 2; x++)
        {
            for (int y = -size.y / 2; y <= size.y / 2; y++)
            {
                Gizmos.DrawWireSphere(new Vector2(x * cellSize.x, y * cellSize.y), .1f);
            }
        }
    }
}
