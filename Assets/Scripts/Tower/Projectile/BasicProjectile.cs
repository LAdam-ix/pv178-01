using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile
{
    override protected void HitEnemy(GameObject enemy)
    {
        var healthComp = enemy.GetComponent<HealthComponent>();
        healthComp.HealthValue -= _damage;
    }
    
}
