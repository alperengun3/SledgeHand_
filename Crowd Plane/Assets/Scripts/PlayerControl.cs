using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    [HideInInspector] public Animator anim;
    [SerializeField] private PlayerSettings settings;
    CamControl cam;
    private Vector3 mousePos;
    private Vector3 firstPos;
    private Vector3 mouseDif;
    [SerializeField] private Camera ortho;
    public GameObject sledge;
    [SerializeField] GameObject finishBoard;
    [SerializeField] GameObject finishBoardParticle;
    [SerializeField] Transform sledgeScale;
    bool isColliding = false;
    InGameUI gameUI;
    [SerializeField] Image imageBar;
    float currentDist;
    float startDist;
    [SerializeField] Text scoreText;
    public static PlayerControl instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        settings.score = 0;
        finishBoard.SetActive(false);
        finishBoardParticle.SetActive(false);
        startDist = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(0, 0, 118.0f));
        gameUI = FindObjectOfType<InGameUI>();
        cam = FindObjectOfType<CamControl>();
        rb = GetComponent<Rigidbody>();
        settings.isPlaying = false;
        anim = GetComponentInChildren<Animator>();
        anim.SetBool(StringClass.TAG_ISIDLE, true);
        anim.SetBool(StringClass.TAG_ISRUNNING, false);
    }

    void Update()
    {
        scoreText.text = "Score: " + settings.score;
        currentDist = (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(0, 0, 118.0f))) / startDist;
        imageBar.fillAmount = 1 - currentDist;

        isColliding = false;
        if (Input.GetMouseButtonDown(0))
        {
            settings.isPlaying = true;
        }

        if (settings.isPlaying)
        {
            anim.SetBool(StringClass.TAG_ISIDLE, false);
            anim.SetBool(StringClass.TAG_ISRUNNING, true);

            Move();

            firstPos = Vector3.Lerp(firstPos, mousePos, 0.1f);

            if (Input.GetMouseButtonDown(0))
                MouseDown(Input.mousePosition);

            else if (Input.GetMouseButtonUp(0))
                MouseUp();

            else if (Input.GetMouseButton(0))
                MouseHold(Input.mousePosition);

        }

        settings.sledgeScale = sledgeScale.transform.localScale.x;
    }

    void Move()
    {
        if (settings.isPlaying)
        {
            float xPos = Mathf.Clamp(transform.position.x, -2f, 2f);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            rb.velocity = new Vector3(mouseDif.x, rb.velocity.y, 1 * settings.ForwardSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringClass.TAG_FINISH))
        {
            finishBoardParticle.SetActive(true);
            Invoke("FinishBoardTrue", 0.25f);
            rb.isKinematic = true;
            settings.isPlaying = false;
            Invoke("HitFinal", 0.5f);
            other.GetComponent<BoxCollider>().enabled = false;
            Invoke("IdleAnim", 1.5f);
            Invoke("LevelComplate", 8);
            rb.isKinematic = true;
            transform.DOMove(new Vector3(0.25f, 0.5f, 118.15f), 1f);
            //LevelManager.Instance.Invoke("LoadNextLevel", 7.5f);
        }
        if (isColliding) return;
        else isColliding = true;

    }

    private void MouseDown(Vector3 inputPos)
    {
        mousePos = ortho.ScreenToWorldPoint(inputPos);
        firstPos = mousePos;
    }

    private void MouseHold(Vector3 inputPos)
    {
        mousePos = ortho.ScreenToWorldPoint(inputPos);
        mouseDif = mousePos - firstPos;
        mouseDif *= settings.sensitivity;
    }

    private void MouseUp()
    {
        mouseDif = Vector3.zero;
    }

    private void IdleAnim()
    {
        anim.SetBool(StringClass.TAG_ISIDLE, true);
    }

    void LevelComplate()
    {
        gameUI.StartCoroutine(gameUI.levelComplete());
    }

    void FinishBoardTrue()
    {
        finishBoard.SetActive(true);
        cam.targetPos = cam.boss;
    }

    void HitFinal()
    {
        anim.SetTrigger(StringClass.TAG_HITFINAL);
        anim.SetBool(StringClass.TAG_ISRUNNING, false);
    }

}
