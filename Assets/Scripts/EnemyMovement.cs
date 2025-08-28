using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float moveSpeed;
    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(moveSpeed * Time.fixedDeltaTime * Vector3.left);
    }
}
