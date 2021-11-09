using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wall : MonoBehaviour
{
    [SerializeField] PlayerSettings settings;
    [SerializeField] List<GameObject> bricks;
    public bool wallBool;
    MeshRenderer rend;
    GameObject targetPos;
    CamControl cam;
    public Collider childCollider;
    PlayerControl playerControl;
    Rigidbody rb;
    float wallCrackTime;
    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        rend = GetComponent<MeshRenderer>();
        cam = FindObjectOfType<CamControl>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (wallBool)
        {
            wallCrackTime += Time.deltaTime;

            foreach (GameObject brick in bricks)
            {
                if (brick != null && wallCrackTime <= 0.1f)
                {
                    brick.transform.DOMove(new Vector3(targetPos.transform.position.x, targetPos.transform.position.y, targetPos.transform.position.z + 0.7f), 0.3f);
                    Debug.Log("girfi");
                }
                    StartCoroutine(DestroyBrick(brick));
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringClass.TAG_SLEDGEHAMMER) && rend.sharedMaterial == playerControl.sledge.GetComponent<MeshRenderer>().sharedMaterial)
        {
            playerControl.anim.SetTrigger(StringClass.TAG_HIT);
            foreach (GameObject brick in bricks)
            {
                brick.AddComponent<Rigidbody>();
                brick.GetComponent<Rigidbody>().mass = 0.005f;
                brick.GetComponent<Rigidbody>().useGravity = true;
                brick.GetComponent<Rigidbody>().isKinematic = true;
            }
            StartCoroutine(SledgeHit(other));
            GetComponent<Collider>().enabled = false;
            Invoke("DestroyWall", 5);
        }

        if (other.CompareTag(StringClass.TAG_SLEDGEHAMMER) && rend.sharedMaterial != playerControl.sledge.GetComponent<MeshRenderer>().sharedMaterial)
        {
            playerControl.anim.SetTrigger(StringClass.TAG_STUNWALKING);
            foreach (GameObject brick in bricks)
            {
                brick.AddComponent<Rigidbody>();
                brick.GetComponent<Rigidbody>().mass = 0.005f;
                brick.GetComponent<Rigidbody>().useGravity = true;
                brick.GetComponent<Rigidbody>().isKinematic = true;
            }
            Destroy(this.GetComponent<Collider>());
            other.transform.DOScale(new Vector3(other.transform.localScale.x - 0.5f, other.transform.localScale.y - 0.1f, other.transform.localScale.z - 0.05f), 1f);
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            StartCoroutine(BrickScale(other));
            Invoke("DestroyWall", 5);
            cam.CamShake();
            Invoke("ScoreTime", 0.2f);
        }

    }
    private IEnumerator SledgeHit(Collider other)
    {
        yield return new WaitForSeconds(0.2f);
        settings.score++;
        Invoke("wallBoolTime", 0.75f);
        targetPos = other.gameObject;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(this.GetComponent<Collider>());
        StartCoroutine(BrickScale(other));
        foreach (GameObject brick in bricks)
        {
            if (brick != null)
            {
                brick.GetComponent<Rigidbody>().isKinematic = false;
                brick.GetComponent<Rigidbody>().useGravity = true;
                brick.GetComponent<BoxCollider>().enabled = true;
                brick.GetComponent<Collider>().isTrigger = false;
            }
        }
        cam.CamShake();
        yield return new WaitForSeconds(0.9f);
        other.transform.DOScale(new Vector3(other.transform.localScale.x + 5f, other.transform.localScale.y + 1f, other.transform.localScale.z + 0.8f), 0.1f).
                OnComplete(() => other.transform.DOScale(new Vector3(other.transform.localScale.x - 4f, other.transform.localScale.y - 0.8f, other.transform.localScale.z - 0.7f), 0.2f));
    }

    void wallBoolTime()
    {
        wallBool = true;
    }

    IEnumerator BrickScale(Collider other)
    {
        yield return new WaitForSeconds(0.35f);
        foreach (GameObject brick in bricks)
        {
            if (brick != null)
            {
                brick.GetComponent<Rigidbody>().isKinematic = false;
                brick.GetComponent<Rigidbody>().useGravity = true;
                brick.GetComponent<BoxCollider>().enabled = true;
                brick.GetComponent<Collider>().isTrigger = false;
            }
        }
        gameObject.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 3);
        yield return new WaitForSeconds(0.3f);
        //gameObject.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 1);
        foreach (GameObject brick in bricks)
        {
            if (brick != null)
            {
                brick.GetComponent<BoxCollider>().enabled = false;
            }
        }

        yield return new WaitForSeconds(0.3f);
        foreach (GameObject brick in bricks)
        {
            brick.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void ScoreTime()
    {
        settings.score--;
    }

    IEnumerator DestroyBrickComp(GameObject brick)
    {
        if (brick != null)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                //yield return new WaitForSeconds(0.1f);
                //Destroy(bricks[i].GetComponent<Rigidbody>());
                //yield return new WaitForSeconds(0.01f);
                //Destroy(bricks[i].GetComponent<BoxCollider>());
                //yield return new WaitForSeconds(0.01f);
                //Destroy(bricks[i].GetComponent<MeshRenderer>());
                //yield return new WaitForSeconds(0.01f);
                //Destroy(bricks[i].GetComponent<MeshFilter>());
                yield return new WaitForSeconds(0.01f);
                Destroy(bricks[i]);
                //brick.SetActive(false);
            }
           
        }
    }

    void DestroyWall()
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyBrick(GameObject brick)
    {
        if (brick != null)
        {
                yield return new WaitForSeconds(0.3f);
                StartCoroutine(DestroyBrickComp(brick));
        }
    }

}