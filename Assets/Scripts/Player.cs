using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;

    public bool grounded;
    public bool isDead;

    private Rigidbody2D rg2d;
    private SpriteRenderer rend;
    private Animator anim;

    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        isDead = false;
    }

    private void Update()
    {
        if (isDead) return;

        //moving
        float h = Input.GetAxis("Horizontal");

        rg2d.AddForce(Vector2.right * h * speed);

        //jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space)) rg2d.AddForce(Vector2.up * jump);
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        GroundCheck();
    }

    public void Die()
    {
        if (isDead) return;

        anim.SetTrigger("Dead");
        isDead = true;
        rend.color = Color.gray;
        rg2d.velocity = Vector2.zero;
        

        GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");

        for(int i = 0; i < controllers.Length; i++)
        {
            var aux = controllers[i].GetComponent<LevelController>();
            if (aux.isActive)
                aux.RespawnPlayer();
        }
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
}