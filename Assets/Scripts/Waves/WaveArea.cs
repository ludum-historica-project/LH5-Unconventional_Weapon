using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newWaveArea", menuName = "Scriptables/Wave/Area")]
[System.Serializable]
public class WaveArea : ScriptableObject
{
    public enum AreaType
    {
        Circle,
        Rectangle
    }
    public AreaType type;
    public Vector2 center;
    public float radius;
    public Vector2 size;

    public Vector2 GetRandomPoint(bool edge)
    {
        Vector2 pos = Vector2.zero;
        switch (type)
        {
            case AreaType.Circle:
                pos = center + (Vector2)(Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector3.up * (edge ? radius : Random.Range(0, radius)));
                break;
            case AreaType.Rectangle:
                Vector2 point = new Vector2(Random.Range(-size.x, size.x), Random.Range(-size.y, size.y)) / 2;
                if (edge)
                {
                    Vector2 verticalExtreme = (point / point.y) * size.y / 2;
                    Vector2 horizontalExtreme = (point / point.x) * size.x / 2;
                    pos = center + (verticalExtreme.magnitude > horizontalExtreme.magnitude ? horizontalExtreme : verticalExtreme);
                }
                else
                {
                    pos = center + point;
                }
                break;
            default:
                break;
        }
        return pos;
    }

}
