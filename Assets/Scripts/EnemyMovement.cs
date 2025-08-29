using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float moveSpeed;
    [HideInInspector] public bool isSpeedReduced;
    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(moveSpeed * Time.fixedDeltaTime * Vector3.left);
    }
    public void ReduceMoveSpeed()
    {
        if (isSpeedReduced)
            return;

        isSpeedReduced = true;
        moveSpeed /= 2;
    }
    public void ResetMoveSpeed()
    {
        if (!isSpeedReduced)
            return;

        isSpeedReduced = false;
        moveSpeed *= 2;
    }
}
