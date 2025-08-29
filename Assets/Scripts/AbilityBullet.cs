using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AbilityBullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] AbilityEffect abilityEffect;
    [SerializeField] Vector3 abilityOffset;

    [HideInInspector] public bool shot = false;

    private Vector2 p0, p1, p2;
    private float speed;
    private float t = 0f;
    private Rigidbody2D rb;

    public void SetBezierCurve(Vector2 start, Vector2 control, Vector2 end, float moveSpeed)
    {
        p0 = start;
        p1 = control;
        p2 = end;
        speed = moveSpeed;
        t = 0f;
        shot = true;
        rb.position = p0;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        if (!shot)
            return;

        if (t >= 1f)
        {
            rb.isKinematic = false;
            rb.gravityScale = 1f;
            return;
        }

        float distance = Vector2.Distance(p0, p2);
        if (distance <= Mathf.Epsilon) distance = 0.0001f;

        t += (speed * Time.fixedDeltaTime) / distance;
        t = Mathf.Clamp01(t);

        float oneMinusT = 1f - t;
        Vector2 bezierPos =
            (oneMinusT * oneMinusT) * p0 +
            2f * oneMinusT * t * p1 +
            (t * t) * p2;

        rb.MovePosition(bezierPos);
    }

    private void ActivateAbility()
    {
        Instantiate(abilityEffect, transform.position + abilityOffset, abilityEffect.transform.rotation);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ActivateAbility();
    }
}