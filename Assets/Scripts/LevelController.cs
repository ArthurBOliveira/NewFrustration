using UnityEngine;

public class LevelController : MonoBehaviour
{
    public string name;
    public bool isActive;
    
    public GameObject initPos;
    public GameObject player;
    public GameObject camera;
    public GameObject nextLevel;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject jumpButton;

    private bool once;

    private void Start()
    {
        once = true;
    }

    private void Update()
    {
        if (once && isActive) StartLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var col = collision.GetComponent<Collider2D>();
        if (col.tag == "Player")
        {
            Destroy(collision.gameObject);
            NextLevel();
        }
    }

    public void StartLevel()
    {
        isActive = true;
        once = false;
        camera.SetActive(true);
        GameObject p = Instantiate(player, initPos.transform.position, Quaternion.identity);
        SetPlayerOnCamera(p);
    }

    public void NextLevel()
    {
        isActive = false;
        camera.SetActive(false);
        nextLevel.GetComponent<LevelController>().StartLevel();
    }

    public void RespawnPlayer()
    {
        GameObject p = Instantiate(player, initPos.transform.position, Quaternion.identity);
        SetPlayerOnCamera(p);
    }

    public void SetPlayerOnCamera(GameObject player)
    {
        camera.GetComponent<CameraFollow>().player = player;
    }
}
