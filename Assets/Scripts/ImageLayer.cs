using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLayer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TowerLayer layer;
    [SerializeField] Vector3 offset;

    private void Update()
    {
        transform.position = layer.transform.position + offset;
    }
}
