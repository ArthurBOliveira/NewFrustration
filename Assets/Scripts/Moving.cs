using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public bool left;
    public float speed;

    public float ending;

    private Rigidbody2D rg2d;

    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (left)
            rg2d.velocity = Vector2.left * speed;
        else
            rg2d.velocity = Vector2.right * speed;
    }

    private void Update()
    {
        if (left && transform.position.x <= ending)
            Destroy(gameObject);
        else if (!left && transform.position.x >= ending)
            Destroy(gameObject);
    }
}
