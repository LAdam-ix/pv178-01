using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggresiveEnemy : Enemy
{
    [Header("AggresiveEnemyAttributes")]
    [SerializeField]
    protected int _detectionRange = 10;
    protected bool _hasTarget = false;

    protected Transform _target = null;

    protected override void MovementBehavior()
    {
        if (_target == null)
        {
            if (_hasTarget)
            {
                _hasTarget = false;
                _movementComponent.MoveAlongPath();
                return;
            }

            Collider[] colisions = Physics.OverlapSphere(
                this.transform.position,
                _detectionRange,
                _attackLayerMask
            );

            foreach (var hit in colisions)
            {
                GameObject entity = hit.gameObject;
                if (entity.tag == TowerTag)
                {
                    _target = entity.transform;
                    _hasTarget = true;
                    _movementComponent.MoveTowards(_target);
                }
            }
        }
    }
}
