using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [Header("Unity Stuff")]
    [SerializeField]
    protected Rigidbody _rb;

    [SerializeField]
    protected LayerMask _enemyLayerMask;

    [SerializeField]
    protected ParticleSystem _onHitParticleSystem;

    public Transform Target { get; set; }

    [Header("Arguments")]
    [SerializeField]
    protected int _damage;

    [SerializeField]
    protected int _speed;

    [SerializeField]
    protected float lifespan;
    protected Vector3 moveTo;

    protected void Start()
    {
        moveTo = transform.position;
    }

    protected void Update()
    {
        // I dont use = so it has "last" try to hit something before dying
        // if (lifespan < 0 || (Target == null && moveTo == transform.position))
        if (lifespan < 0)
        {
            Destroy(gameObject);
        }

        // If Projectile targeted enemy is dead the it's reaches it's last coordinates and then act
        // as a "mine" (stay there and wait), until he runs out of lifespan
        if (Target != null)
        {
            moveTo = Target.position;
        }

        float step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, moveTo, step);

        lifespan -= Time.deltaTime;
    }

    abstract protected void HitEnemy(GameObject enemy);

    protected void OnTriggerEnter(Collider other)
    {
        var entity = other.gameObject;
        if (entity.tag != Tower.EnemyTag)
        {
            return;
        }

        Instantiate(_onHitParticleSystem, transform.position, transform.rotation);
        HitEnemy(entity);
        Destroy(gameObject);
    }
}
