using System.Collections.ObjectModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent), typeof(HealthComponent), typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    [Header("Unity Stuff")]
    [SerializeField]
    protected MovementComponent _movementComponent;

    [SerializeField]
    protected HealthComponent _healthComponent;

    [SerializeField]
    protected ParticleSystem _onDeathParticlePrefab;

    [SerializeField]
    protected ParticleSystem _onSuccessParticlePrefab;

    [SerializeField]
    protected LayerMask _attackLayerMask;

    [SerializeField]
    public event Action OnDeath;

    [Header("Attributes")]
    [SerializeField]
    protected int _speed;

    [SerializeField]
    protected int _damage;

    [SerializeField]
    protected int _reward;
    protected bool _deathParticles = true;
    public static string TowerTag = "Tower";

    protected void Start()
    {
        _healthComponent.OnDeath += HandleDeath;
        _movementComponent.MoveAlongPath();
    }

    protected virtual void MovementBehavior() { }

    protected void Update()
    {
        MovementBehavior();
    }

    // Default is normal damage then tou can edit it in children for example as in lazy enmies
    virtual protected int CalculateDamageTo(GameObject entity)
    {
        return _damage;
    }

    protected void OnCollisionEnter(Collision other)
    {
        var entity = other.gameObject;

        if (((1 << entity.layer) & _attackLayerMask.value) != 0)
        {
            int damage = CalculateDamageTo(entity);
            entity.GetComponent<HealthComponent>().HealthValue -= damage;

            Instantiate(_onDeathParticlePrefab, transform.position, transform.rotation);
            _deathParticles = false;
            HandleDeath();
        }
    }

    protected void OnDestroy()
    {
        _healthComponent.OnDeath -= HandleDeath;
    }

    public void Init(EnemyPath path)
    {
        _movementComponent.Init(path, _speed);
    }

    protected void HandleDeath()
    {
        if (_deathParticles)
        {
            Instantiate(_onDeathParticlePrefab, transform.position, transform.rotation);
        }
        GameObject.FindObjectOfType<Player>().Resources += _reward;
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
