using UnityEngine;

public class Platform : MonoBehaviour
{
    public int status;
    public bool isKilling;

    private SpriteRenderer rend;
    private Rigidbody2D rg2d;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rg2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isKilling = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var col = collision.GetComponent<Collider2D>();
        if (isKilling && col.tag == "Player")
            col.GetComponent<Player>().Die();
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

            case 2:
                turnBouncy();
                break;

            case 3:
                turnIcy();
                break;

            default:
                turnNormal();
                break;
        }
    }

    private void turnNormal()
    {
        rend.color = Color.white;
        isKilling = false;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { };
    }

    private void turnKilling()
    {
        rend.color = Color.red;
        isKilling = true;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { };
    }

    private void turnBouncy()
    {
        rend.color = new Color(128, 0, 128); //purple
        isKilling = false;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { bounciness = 2 };
    }

    private void turnIcy()
    {
        rend.color = new Color(212, 240, 255); //Icy
        isKilling = false;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { friction = 0 };
    }
}
