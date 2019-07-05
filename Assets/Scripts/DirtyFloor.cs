using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyFloor : MonoBehaviour
{
    public DustMote dirtPacketPrefab;
    //public Vector2 size;
    public int dustCount;
    public float innerRadius;
    public float outerRadius;



    // Start is called before the first frame update
    void Start()
    {
        int watchdog = 10000;
        float dustDistance = 1;
        List<DustMote> spawnedPockets = new List<DustMote>();
        while (spawnedPockets.Count < dustCount)
        {
            bool failed = false;
            var pos = transform.position + GetPointWithinLimits();
            foreach (var dust in spawnedPockets)
            {

                if (Vector3.Distance(pos, dust.transform.position) < dustDistance)
                {
                    failed = true;
                    break;
                }
            }
            if (failed)
            {
                dustDistance *= .9f;
            }
            else
            {
                dustDistance = 1;
                spawnedPockets.Add(Instantiate(dirtPacketPrefab, pos, Quaternion.identity));
            }
            if (watchdog-- <= 0) break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GetPointWithinLimits()
    {
        //return new Vector3(Random.Range(-size.x, size.x), Random.Range(-size.y, size.y)) / 2;
        return Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector3.up * Mathf.Lerp(innerRadius, outerRadius, Mathf.Pow(Random.Range(0f, 1), 5));
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position, size);
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
}
