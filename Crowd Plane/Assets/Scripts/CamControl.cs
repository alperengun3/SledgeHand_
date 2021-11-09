using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamControl : MonoBehaviour
{
    public Transform boxer;
    public Transform boss;
    [HideInInspector] public Transform targetPos;
    [SerializeField] private float dist;
    [SerializeField] private float distBoss;
    public float bossDist;

    void Start()
    {
        targetPos = boxer;
        dist = transform.position.z - targetPos.transform.position.z;
        bossDist = -3f;
    }

    void FixedUpdate()
    {
        if (targetPos == boss)
        {
            transform.position = new Vector3(transform.position.x, boss.transform.position.y + 5, targetPos.position.z + dist + bossDist);
        }
        else
        {
            transform.position = new Vector3(targetPos.position.x + 2.25f, transform.position.y, targetPos.position.z + dist);
        }
    }

    public void CamShake()
    {
        transform.DOShakeRotation(0.5f, 1);
    }
}
