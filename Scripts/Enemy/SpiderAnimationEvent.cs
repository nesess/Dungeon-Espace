using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject AcidPrefab;
    private Vector3 position;

    void Start()
    {
        position = transform.position;
        position.x = transform.position.x - 0.5f;
    }

    public void Fire()
    {
        Instantiate(AcidPrefab, position, Quaternion.identity);
    }
}
