using UnityEngine;

public class Platform : MonoBehaviour
{
    public int status;
    public bool isKilling;

    private SpriteRenderer rend;
    private Rigidbody2D rg2d;
    private PolygonCollider2D pc2d;
    private Animator anim;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rg2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
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
        anim.SetInteger("status", status);
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
        isKilling = false;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { };
        pc2d.isTrigger = false;        
    }

    private void turnKilling()
    {
        isKilling = true;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { };
    }

    private void turnBouncy()
    {
        isKilling = false;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { bounciness = 2 };
        pc2d.isTrigger = false;
    }

    private void turnIcy()
    {
        isKilling = false;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { friction = 0 };
        pc2d.isTrigger = false;
    }

    private void turnPhantom()
    {
        isKilling = false;
        rg2d.sharedMaterial = new PhysicsMaterial2D() { };
        pc2d.isTrigger = true;
    }
}
