using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField]
    protected float _explosionRange = 5;

    [SerializeField]
    protected int _explosiveDamge = 10;

    protected void Explode()
    {
        Collider[] colisions = Physics.OverlapSphere(
            transform.position,
            _explosionRange,
            _enemyLayerMask
        );
        foreach (var hit in colisions)
        {
            GameObject entity = hit.gameObject;
            // same as in tower - rather safe then sorry
            if (entity.tag == Tower.EnemyTag)
            {
                entity.GetComponent<HealthComponent>().HealthValue -= _explosiveDamge;
            }
        }
    }

    override protected void HitEnemy(GameObject enemy)
    {
        var healthComp = enemy.GetComponent<HealthComponent>();
        healthComp.HealthValue -= _damage;

        Explode();
    }

    protected void OnDestroy()
    {
        Explode();
    }
}
