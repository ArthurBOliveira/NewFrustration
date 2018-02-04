using UnityEngine;

public class Platform : MonoBehaviour
{
    public int status;

    private SpriteRenderer rend;
    private Rigidbody2D rg2d;

    private bool isKilling;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rg2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isKilling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isKilling && collision.collider.tag == "player")
            collision.collider.GetComponent<Player>().Die();
    }

    private void Update()
    {
        switch (status)
        {
            case 0:
                turnNormal();
                break;

            case 1:
                turnKilling();
                break;
        }
    }

    private void turnNormal()
    {
        rend.color = Color.white;
        rg2d.sharedMaterial = new PhysicsMaterial2D() {  };
    }

    private void turnKilling()
    {
        rend.color = Color.red;
        isKilling = true;
    }
}
