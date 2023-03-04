using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstTower : Tower
{
    protected IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    protected override void Shoot()
    {
        CreateProjectile();
        Wait(0.2f);
        CreateProjectile();
    }
}
