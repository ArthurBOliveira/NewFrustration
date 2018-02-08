using UnityEngine;

public class LevelController : MonoBehaviour
{
    public string name;
    public bool isActive;
    
    public GameObject initPos;
    public GameObject player;
    public GameObject camera;
    public GameObject nextLevel;

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
        Instantiate(player, initPos.transform.position, Quaternion.identity);
    }

    public void NextLevel()
    {
        isActive = false;
        camera.SetActive(false);
        nextLevel.GetComponent<LevelController>().StartLevel();
    }

    public void RespawnPlayer()
    {
        Instantiate(player, initPos.transform.position, Quaternion.identity);
    }
}
