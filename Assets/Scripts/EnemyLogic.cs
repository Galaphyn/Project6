using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private GameObject player;
    private GameObject parentSpawner;
    private IndependantSpawner spawnerIndependant;
    private GlobalSpawner spawnerGlobal;
    private Vector2 target;
    public int hp = 3;

    public float chaseSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector2(player.transform.position.x, transform.position.y); //Only get the x so they dont start hovering
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * chaseSpeed);
    }

    //Sets parent spawner so that it can spawn a replacement upon their death
    public void setSpawner(GameObject spawner)
    {
        parentSpawner = spawner;
        spawnerIndependant = parentSpawner.GetComponent<IndependantSpawner>();
        spawnerGlobal = spawner.GetComponent<GlobalSpawner>();
    }

    //For calculating varying levels of damage based on player stats
    public void calculateDamage(int dmg)
    {
        Debug.Log("Hit!");
        hp -= dmg;
        if (hp <= 0)
        {
            if (spawnerGlobal != null)
            {
                Destroy(gameObject);
                spawnerGlobal.destroyEnemy();
            }
            else if (spawnerIndependant != null)
            {
                Destroy(gameObject);
                spawnerIndependant.destroyEnemy();
            }
            else
            {
                Debug.Log("No parent spawner found!");
            }
        }
    }
}
