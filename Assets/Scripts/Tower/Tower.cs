using System.Transactions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(HealthComponent))]
public class Tower : MonoBehaviour
{
    [Header("Unity Stuff")]
    [SerializeField]
    protected LayerMask _enemyLayerMask;

    [SerializeField]
    private HealthComponent _healthComponent;

    [SerializeField]
    protected Projectile _projectilePrefab;

    [SerializeField]
    private BoxCollider _boxCollider;

    [SerializeField]
    protected Transform _objectToPan;

    [SerializeField]
    protected Transform _projectileSpawn;

    [SerializeField]
    private GameObject _previewPrefab;

    public HealthComponent Health => _healthComponent;
    public BoxCollider Collider => _boxCollider;
    public GameObject BuildingPreview => _previewPrefab;

    public static string EnemyTag = "Enemy";

    [Header("Attributes")]
    public string Name;

    [SerializeField]
    protected int _fireRange;

    [SerializeField]
    protected float _reloadTime = 1;
    protected float _fireCountdoown = 0;

    public int Price;

    protected Transform _target = null;

    protected void Start()
    {
        _healthComponent.OnDeath += HandleDeath;
    }

    protected void Update()
    {
        if (!ValidateTarget())
        {
            _target = null;
            // as soon as we find enmy we shoot, it don't wait for another update call
            if (!FindTarget())
            {
                return;
            }
        }
        UpdateRotation();
        if (_fireCountdoown < 0)
        {
            Shoot();
            _fireCountdoown = _reloadTime;
        }
        _fireCountdoown -= Time.deltaTime;
    }

    protected void CreateProjectile()
    {
        Projectile projectile = Instantiate(
            _projectilePrefab,
            _projectileSpawn.transform.position,
            _projectileSpawn.transform.rotation
        );
        projectile.Target = _target;
    }

    protected void UpdateRotation()
    {
        if (_target != null)
        {
            _objectToPan.rotation = Quaternion.LookRotation(_target.position - transform.position);
        }
    }

    protected virtual void Shoot()
    {
        CreateProjectile();
    }

    protected bool ValidateTarget()
    {
        return _target != null
            && _fireRange >= Vector3.Distance(this.transform.position, _target.transform.position);
    }

    protected virtual bool FindTarget()
    {
        Collider[] colisions = Physics.OverlapSphere(
            this.transform.position,
            _fireRange,
            _enemyLayerMask
        );

        float shortestDistance = float.PositiveInfinity;
        GameObject targetedEnemy = null;
        foreach (var hit in colisions)
        {
            GameObject entity = hit.gameObject;
            // this is probably a little obselete because
            // of the layer mask, but im not 100% how unity
            // mask works so I leaving it here to be sure it's works
            if (entity.tag == EnemyTag)
            {
                float distance = Vector3.Distance(transform.position, entity.transform.position);
                if (shortestDistance > distance)
                {
                    shortestDistance = distance;
                    targetedEnemy = entity;
                }
            }
        }
        if (targetedEnemy != null)
        {
            _target = targetedEnemy.transform;
            return true;
        }
        return false;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _fireRange);
    }

    protected void OnDestroy()
    {
        _healthComponent.OnDeath -= HandleDeath;
    }

    protected void HandleDeath()
    {
        Destroy(gameObject);
    }
}
