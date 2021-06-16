using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Canvas script;

    public Enemy[] enemies;
    [SerializeField] GameObject enemyPrefab;
    public static Spawner singleton { get; private set; }
    private void Start()
    {
        enemies = new Enemy[20];
        singleton = this;
        InvokeRepeating("Spawn", 1, 2);
    }
    void Spawn()
    {
        short index = GetFreeSlot();
        if (index == -1) return;
        Enemy e = Instantiate(enemyPrefab, new Vector2(Random.Range(-500, 500), Random.Range(-500, 500)), Quaternion.identity, transform).GetComponent<Enemy>();
        enemies[index] = e;
    }
    short GetFreeSlot()
    {
        for (short i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null) return i;
        }
        return -1;
    }
    public bool ContainEnemy()
    {
        for (short i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null) return true;
        }
        return false;
    }
}
