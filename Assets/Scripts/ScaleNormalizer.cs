using UnityEngine;

public class ScaleNormalizer : MonoBehaviour
{
    private Vector3 baseScale;

    private void Awake()
    {
        baseScale = transform.localScale; 
    }

    private void LateUpdate()
    {
        Vector3 animatedScale = transform.localScale;
        transform.localScale = new Vector3(
            baseScale.x * animatedScale.x,
            baseScale.y * animatedScale.y,
            baseScale.z * animatedScale.z
        );
    }
}