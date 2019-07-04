using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDuster : Enemy
{
    public float damage = 5;
    public DirtyFloor limits;
    public float targetUpdateCooldown = 5;
    public DustMote motePrefab;

    public System.Action OnKill = () => { };

    public ScriptableEvent OnEnemyDeathEvent;

    //float _currentTargetUpdateCooldown = 0;

    //Vector3 _currentTarget;

    public float dustDropDistanceThreshold;

    Vector3 _lastDropLocation = Vector3.forward * 1000;

    CharacterController2D _target;
    private void Awake()
    {
        _target = FindObjectOfType<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, _lastDropLocation) > dustDropDistanceThreshold)
        {
            Instantiate(motePrefab, transform.position, Quaternion.identity).transform.localScale = Vector3.zero;
            _lastDropLocation = transform.position;
        }

        HandleAnimation((_target.transform.position - transform.position).normalized);
        _movement.MoveTo(_target.transform.position);
    }

    void HandleAnimation(Vector2 direction)
    {
        GetComponent<Animator>().SetFloat("Horizontal", direction.x);
        GetComponent<Animator>().SetFloat("Vertical", direction.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<DustBullet>())
        {
            Destroy(gameObject);
            Director.GetManager<ScoreManager>().AddScore(5);
        }
        if (collision.collider.GetComponent<PlayerHealth>())
        {
            Destroy(gameObject);
            collision.collider.GetComponent<PlayerHealth>().Damage(damage);
        }
    }


    private void OnDestroy()
    {
        OnKill();
        OnEnemyDeathEvent.Raise();
    }
}
