using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyEnemy : Enemy
{
    [Header("LazyEnemyAttributes")]
    [SerializeField]
    protected float walkTime;

    [SerializeField]
    protected float stopTime = 1;
    protected float movementCountdown = 5;
    protected bool _onBreak = false;

    protected override void MovementBehavior()
    {
        movementCountdown -= Time.deltaTime;

        if (_onBreak)
        {
            if (movementCountdown <= 0)
            {
                _onBreak = false;
                _movementComponent.MoveAlongPath();
                movementCountdown = walkTime;
            }
            return;
        }
        if (movementCountdown <= 0)
        {
            _onBreak = true;
            _movementComponent.CancelMovement();
            movementCountdown = stopTime;
        }
    }

    override protected int CalculateDamageTo(GameObject entity)
    {
        if (entity.tag == TowerTag)
        {
            return _damage * 2;
        }
        return _damage;
    }
}
