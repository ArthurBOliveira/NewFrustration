using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;

    public bool grounded;
    public bool isDead;

    private Rigidbody2D rg2d;
    private SpriteRenderer rend;
    private Animator anim;
    private BoxCollider2D coll;

    public bool isMovingLeft = false;
    public bool isMovingRight = false;

    #region Private
    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        isDead = false;
    }

    private void Start()
    {
        FixButtons();
    }

    private void Update()
    {
        if (isDead) return;

        //moving
        float h = Input.GetAxis("Horizontal");

        rg2d.AddForce(Vector2.right * h * speed);

        //jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space)) rg2d.AddForce(Vector2.up * jump);

        //moving mobile
        if (isMovingRight)
            rg2d.AddForce(Vector2.right * speed);
        if (isMovingLeft)
            rg2d.AddForce(Vector2.left * speed);
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        GroundCheck();
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        grounded = false;
    }

    private void GroundCheck()
    {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll(positionToCheck, new Vector2(0, -1), 0.01f);

        //if a collider was hit, we are grounded
        if (hits.Length > 0)
        {
            grounded = true;
        }
    }

    private void FixButtons()
    {
        GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");

        for (int i = 0; i < controllers.Length; i++)
        {
            LevelController aux = controllers[i].GetComponent<LevelController>();
            if (aux.isActive)
            {
                #region Right                
                EventTrigger etRight = aux.rightButton.GetComponent<EventTrigger>();

                etRight.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

                EventTrigger.Entry rDown = new EventTrigger.Entry();
                rDown.eventID = EventTriggerType.PointerDown;
                rDown.callback.AddListener((data) => { MovingRight((PointerEventData)data, true); });
                etRight.triggers.Add(rDown);

                EventTrigger.Entry rUp = new EventTrigger.Entry();
                rUp.eventID = EventTriggerType.PointerUp;
                rUp.callback.AddListener((data) => { MovingRight((PointerEventData)data, false); });
                etRight.triggers.Add(rUp);
                #endregion

                #region Left
                EventTrigger etLeft = aux.leftButton.GetComponent<EventTrigger>();

                etLeft.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

                EventTrigger.Entry lDown = new EventTrigger.Entry();
                lDown.eventID = EventTriggerType.PointerDown;
                lDown.callback.AddListener((data) => { MovingLeft((PointerEventData)data, true); });
                etLeft.triggers.Add(lDown);

                EventTrigger.Entry lUp = new EventTrigger.Entry();
                lUp.eventID = EventTriggerType.PointerUp;
                lUp.callback.AddListener((data) => { MovingLeft((PointerEventData)data, false); });
                etLeft.triggers.Add(lUp);
                #endregion

                #region Jump
                EventTrigger etJump = aux.jumpButton.GetComponent<EventTrigger>();

                etJump.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

                EventTrigger.Entry jDown = new EventTrigger.Entry();
                jDown.eventID = EventTriggerType.PointerDown;
                jDown.callback.AddListener((data) => { Jump(); });
                etJump.triggers.Add(jDown);
                #endregion
            }
        }
    }

    private void CleanObject()
    {
        tag = "body";
        Destroy(rg2d);
        //Destroy(anim);
        Destroy(coll);
        Destroy(this);
    }
    #endregion

    #region Public
    public void Die()
    {
        if (isDead) return;

        anim.SetTrigger("Dead");
        isDead = true;
        rend.color = Color.gray;
        rg2d.velocity = Vector2.zero;


        GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");

        for (int i = 0; i < controllers.Length; i++)
        {
            var aux = controllers[i].GetComponent<LevelController>();
            if (aux.isActive)
                aux.RespawnPlayer();
        }

        CleanObject();
    }

    public void MovingLeft(PointerEventData data, bool b)
    {
        isMovingLeft = b;
        transform.localScale = new Vector3(-2, 2, 1);
    }

    public void MovingRight(PointerEventData data, bool b)
    {
        Debug.Log("Test");
        isMovingRight = b;
        transform.localScale = new Vector3(2, 2, 1);
    }

    public void Jump()
    {
        if (grounded && !isDead)
            rg2d.AddForce(Vector2.up * jump);
    }
    #endregion
}