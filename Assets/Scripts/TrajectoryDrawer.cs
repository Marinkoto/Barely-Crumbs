using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float bulletSpeed = 5f;
    public Transform firePoint;

    private Vector3 endPoint;
    public bool isDragging = false;
    public bool active = false;

    void Update()
    {
        if (!active)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            isDragging = false;
            Time.timeScale = 1;
            lineRenderer.positionCount = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }
        if (Input.GetMouseButton(0) && isDragging)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 0;
            DrawCurvePreview(firePoint.position, endPoint);
            Time.timeScale = 0.5f;
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            ShootBulletAlongCurve(firePoint.position, endPoint);
            lineRenderer.positionCount = 0;
            Time.timeScale = 1;
        }
        CharacterEffects.Instance.SlowMoEffect(isDragging);
    }

    void DrawCurvePreview(Vector3 start, Vector3 end)
    {
        Vector3 control = (start + end) / 2 + Vector3.up * 2f;
        int segments = 20;
        lineRenderer.positionCount = segments + 1;

        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 point = GetQuadraticBezierPoint(t, start, control, end);
            lineRenderer.SetPosition(i, point);
        }
    }

    Vector3 GetQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
    }

    void ShootBulletAlongCurve(Vector3 start, Vector3 end)
    {
        Vector3 control = (start + end) / 2 + Vector3.up * 2f;
        PlayerAbilities.Instance.currentAbility.SetBezierCurve(start, control, end, bulletSpeed);
        active = false;
    }
}