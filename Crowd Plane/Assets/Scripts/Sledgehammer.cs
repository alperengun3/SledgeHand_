using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sledgehammer : MonoBehaviour
{
    [SerializeField] Material color_A;
    [SerializeField] Material color_B;
    [SerializeField] float timer;
    MeshRenderer rend;
    [SerializeField] bool colorBool;
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (timer < 0.15f)
            {
                colorBool = !colorBool;
            }

            timer = 0;
        }

        if (colorBool)
        {
            rend.sharedMaterial = color_A;
        }

        if (!colorBool)
        {
            rend.sharedMaterial = color_B;
        }

    }
}
