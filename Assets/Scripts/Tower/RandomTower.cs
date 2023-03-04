using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RandomTower : Tower
{
    private Random random = new Random();

    protected override void Shoot()
    {
        int probability = (int)random.Next(0, 9);

        // If smaller then 2 then shoots twice
        if (probability < 2)
        {
            CreateProjectile();
        }

        if (probability < 8)
        {
            CreateProjectile();
        }

        return;
    }

    protected override bool FindTarget()
    {
        Collider[] entities = Physics.OverlapSphere(this.transform.position, _fireRange);
        List<GameObject> enemies = new List<GameObject>();
        foreach (var hit in entities)
        {
            GameObject entity = hit.gameObject;
            if (entity.tag == EnemyTag)
            {
                enemies.Add(entity);
            }
        }

        if (enemies.Count > 0)
        {
            _target = enemies[random.Next(0, enemies.Count)].gameObject.transform;
            return true;
        }
        return false;
    }
}
