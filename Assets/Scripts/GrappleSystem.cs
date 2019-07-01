using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController2D))]
public class GrappleSystem : MonoBehaviour
{

    public GrappleProjectile grapplePrefab;
    public float ropeExtendSpeed;
    public float maxRopeLength = 10;

    private GrappleProjectile _grappleInstance;
    public bool hooked { get; private set; }
    private LineRenderer _ropeRenderer;
    private DistanceJoint2D _distanceJoint;

    private float _maxRopeSegmentLength;

    private Vector2 _currentHookPoint;

    private List<Vector3> _hookPoints = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        _ropeRenderer = GetComponentInChildren<LineRenderer>();
        _distanceJoint = GetComponent<DistanceJoint2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_grappleInstance == null)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _grappleInstance = Instantiate(grapplePrefab, transform.position, Quaternion.identity);
                _grappleInstance.transform.up = mousePos - (Vector2)_grappleInstance.transform.position;
                _grappleInstance.OnCollision2D += OnHookCollide;
                _ropeRenderer.enabled = true;
            }
        }

        if (Input.GetMouseButtonUp(0) || (_grappleInstance != null && !hooked && Vector3.Distance(_grappleInstance.transform.position, transform.position) > maxRopeLength * 1.1f))
        {
            if (_grappleInstance)
                Destroy(_grappleInstance.gameObject);
            _distanceJoint.enabled = false;
            hooked = false;
            _ropeRenderer.enabled = false;
            _hookPoints.Clear();
        }

        if (hooked)
        {
            RaycastHit2D rch;
            _currentHookPoint = _hookPoints[_hookPoints.Count - 1];

            rch = Physics2D.Raycast(transform.position, _currentHookPoint - (Vector2)transform.position, _distanceJoint.distance * 1.1f, ~(1 << 8 | 1 << 9));
            if (rch.collider != null && Vector3.Distance(_currentHookPoint, rch.point) > .1f)
            {
                _currentHookPoint = GetClosestCorner(rch.collider, rch.point);
                _hookPoints.Add(_currentHookPoint);
                _distanceJoint.connectedAnchor = _currentHookPoint;
            }

            if (_hookPoints.Count > 1)
            {
                var _prevHookPoint = _hookPoints[_hookPoints.Count - 2];
                rch = Physics2D.Raycast(transform.position, _prevHookPoint - transform.position, Vector3.Distance(transform.position, _prevHookPoint), ~(1 << 8 | 1 << 9));
                if (rch.collider == null || Vector3.Distance(rch.point, _prevHookPoint) < .1f)
                {
                    _currentHookPoint = _prevHookPoint;
                    _hookPoints.RemoveAt(_hookPoints.Count - 1);
                    _distanceJoint.connectedAnchor = _currentHookPoint;
                }
            }

            _maxRopeSegmentLength = maxRopeLength;
            if (_hookPoints.Count > 1)
            {
                float usedLength = 0;
                for (int i = 0; i < _hookPoints.Count - 1; i++)
                {
                    usedLength += Vector3.Distance(_hookPoints[i], _hookPoints[i + 1]);
                }
                _maxRopeSegmentLength -= usedLength;
            }
            Debug.Log("Max rope segment length: " + _maxRopeSegmentLength);
            _distanceJoint.distance -= Input.GetAxis("Vertical") * ropeExtendSpeed * Director.GetManager<TimeManager>().deltaTime;
            _distanceJoint.distance = Mathf.Clamp(_distanceJoint.distance, 1, _maxRopeSegmentLength);
        }
        var linePoints = new List<Vector3>(_hookPoints);
        linePoints.Add(transform.position);
        _ropeRenderer.positionCount = linePoints.Count;
        _ropeRenderer.SetPositions(linePoints.ToArray());
    }

    Vector3 GetClosestCorner(Collider2D collider, Vector3 point)
    {
        List<Vector3> corners = GetColliderCorners(collider);
        Vector3 closest = corners[0];
        foreach (var corner in corners)
        {
            if (Vector3.Distance(corner, point) < Vector3.Distance(closest, point)) closest = corner;
        }
        if (Vector3.Distance(closest, point) > .1f) return collider.ClosestPoint(point);
        return closest;
    }

    List<Vector3> GetColliderCorners(Collider2D collider)
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(collider.transform.position + new Vector3(collider.transform.localScale.x, collider.transform.localScale.y) / 2);
        points.Add(collider.transform.position + new Vector3(collider.transform.localScale.x, -collider.transform.localScale.y) / 2);
        points.Add(collider.transform.position + new Vector3(-collider.transform.localScale.x, collider.transform.localScale.y) / 2);
        points.Add(collider.transform.position + new Vector3(-collider.transform.localScale.x, -collider.transform.localScale.y) / 2);
        return points;
    }


    void OnHookCollide(Collision2D collision)
    {
        _currentHookPoint = _grappleInstance.transform.position;
        _distanceJoint.enabled = true;
        _distanceJoint.connectedAnchor = _currentHookPoint;
        _distanceJoint.distance = Vector3.Distance(transform.position, _currentHookPoint);
        _hookPoints.Clear();
        _hookPoints.Add(_currentHookPoint);
        hooked = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        /* if (hooked)
         {
             Gizmos.DrawLine(transform.position, _currentHookPoint);
         }*/

        for (int i = 1; i < _hookPoints.Count; i++)
        {
            Gizmos.DrawLine(_hookPoints[i], _hookPoints[i - 1]);
        }
    }
}
