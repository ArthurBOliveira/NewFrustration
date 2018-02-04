using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;

    public bool grounded;

    private Rigidbody2D rg2d;

    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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
        throw new NotImplementedException();
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