using UnityEngine;

public class Platform : MonoBehaviour
{
    public int status;
    public bool isKilling;

    private SpriteRenderer rend;
    private Rigidbody2D rg2d;
    private PolygonCollider2D pc2d;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rg2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
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

            case 4:
                turnPhantom();
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
        pc2d.isTrigger = false;
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
        pc2d.isTrigger = false;
    }

    private void turnIcy()
    {
        rend.color = Color.cyan;
        isKilling = false;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { friction = 0 };
        pc2d.isTrigger = false;
    }

    private void turnPhantom()
    {
        rend.color = Color.blue;
        isKilling = false;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { };
        pc2d.isTrigger = true;
    }
}
