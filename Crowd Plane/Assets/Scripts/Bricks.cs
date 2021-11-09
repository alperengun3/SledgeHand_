using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bricks : MonoBehaviour
{
    [SerializeField] List<GameObject> bricks;
    GameObject targetPos;
    Wall wall;
    void Start()
    {
        wall = FindObjectOfType<Wall>();
    }

    void Update()
    {
        if (wall.wallBool)
        {
            //foreach (GameObject brick in bricks)
            //{
            //    brick.transform.DOMove(new Vector3(targetPos.transform.position.x, targetPos.transform.position.y, targetPos.transform.position.z + 0.6f), 0.5f);
            //}

            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].transform.DOMove(new Vector3(targetPos.transform.position.x, targetPos.transform.position.y, targetPos.transform.position.z + 0.6f), 0.005f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringClass.TAG_SLEDGEHAMMER))
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                Destroy(bricks[i]);
            }
        }
    }
}
