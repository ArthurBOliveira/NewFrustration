using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public bool left;
    public float speed;
    public float spawnTime;
    public float despawnTime;
    public float size;

    public GameObject platform;
    public GameObject end;

    public bool isActive;

    private void Start()
    {
        isActive = false;
        StartCoroutine(BeginSpawn());
    }

    private void Update()
    {
        isActive = end.GetComponent<LevelController>().isActive;
    }

    private IEnumerator BeginSpawn()
    {
        while (true)
        {
            if (isActive)
            {
                Spawn();
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void Spawn()
    {
        Debug.Log("Spawnou!");
        GameObject plat = Instantiate(platform, transform.position, Quaternion.identity);

        plat.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        plat.transform.localScale = new Vector3(size, plat.transform.localScale.y);

        if (left)
            plat.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        else
            plat.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

        Destroy(plat, despawnTime);
    }
}
