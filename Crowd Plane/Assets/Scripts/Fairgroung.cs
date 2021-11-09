using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairgroung : MonoBehaviour
{
    [SerializeField] GameObject scoreBoard;
    [SerializeField] PlayerSettings settings;
    Rigidbody rb;
    CamControl cam;
    float timeFloat;
    bool bossDistBool;
    [SerializeField] GameObject confettiSpray1;
    [SerializeField] GameObject confettiSpray2;
    [SerializeField] GameObject confettiX20;
    [SerializeField] GameObject stoneExplosion;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = FindObjectOfType<CamControl>();
        timeFloat = 0;
        confettiSpray1.SetActive(false);
        confettiSpray2.SetActive(false);
        stoneExplosion.SetActive(false);
    }

    void Update()
    {
        if (bossDistBool)
        {
            timeFloat += Time.deltaTime;
            cam.bossDist -= Time.deltaTime;
            cam.transform.eulerAngles = new Vector3(12.5f, -12.5f + (timeFloat), 0);
        }

        if (transform.position.y > 85)
        {
            rb.drag = 3.5f;
        }

        if (transform.position.y > 92.5f)
        {
            rb.drag = 4f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringClass.TAG_SLEDGEHAMMER))
        {
            rb.AddForce(new Vector3(0,1,0) * settings.sledgeScale * 250);
            bossDistBool = true;
            stoneExplosion.SetActive(true);
            Invoke("Drag0", 0.15f);
            Invoke("Drag1", 1);
            Invoke("Drag2", 1.5f);
            Invoke("Drag3", 2.5f);
            Invoke("CamPos", 3.5f);
            Invoke("Kinematic", 5);
        }

        if (other.CompareTag(StringClass.TAG_X20))
        {
            rb.isKinematic = true;
            settings.score += 5;
        }

        if (other.CompareTag(StringClass.TAG_SCORE))
        {
            settings.score++;
        }
    }

    private void Drag0()
    {
        rb.drag = 0.25f;
    }

    private void Drag1()
    {
        rb.drag = 0.5f;
    }

    private void Drag2()
    {
        rb.drag = 1.25f;
    }

    private void Drag3()
    {
        rb.drag = 1.95f;
    }

    private void CamPos()
    {
        bossDistBool = false;
    }

    private void Kinematic()
    {
        rb.isKinematic = true;
        confettiSpray1.SetActive(true);
        confettiSpray2.SetActive(true);
    }
}
