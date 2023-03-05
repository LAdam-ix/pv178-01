using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstTower : Tower
{
    protected IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    protected override bool FindTarget()
    {
        Collider[] colisions = Physics.OverlapSphere(
            this.transform.position,
            _fireRange,
            _enemyLayerMask
        );

        float highestHealt = -float.PositiveInfinity;
        GameObject targetedEnemy = null;
        foreach (var hit in colisions)
        {
            GameObject entity = hit.gameObject;
            // this is probably a little obselete because
            // of the layer mask, but im not 100% how unity
            // mask works so I leaving it here to be sure it's works
            if (entity.tag == EnemyTag)
            {
                int health = entity.GetComponent<HealthComponent>().HealthValue;
                if (highestHealt < health)
                {
                    highestHealt = health;
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

    protected override void Shoot()
    {
        CreateProjectile();
        Wait(0.2f);
        CreateProjectile();
    }
}
