using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Canvas script;

    public Enemy[] enemies;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float startDelay;
    public float frequency; // frequency раз в секундку тик
    public short mass; // Мобов за тик
    [SerializeField] Transform EnemiesHandler;
    public static Spawner singleton { get; private set; }
    private void Start()
    {
        enemies = new Enemy[60];
        singleton = this;
        frequency = 1 / frequency;
        InvokeRepeating("Spawn", startDelay, frequency);
    }
    short GetEnemyId()
    {
        short i = (short)Random.Range(0, 22);
        //i = (short)(i == 21 ? 2 : i > 15 ? i = 1 : 0);
        if (i == 21) return 2; // Large
        if (i > 15) return 1;  // Medium;
        else return 0;         // Small
    }
    void Spawn()
    {
        for (short _ = 0; _ < mass; _++)
        {
            short index = GetFreeSlot();
            if (index == -1) return;
            Enemy e = Instantiate(enemyPrefabs[GetEnemyId()], EnemiesHandler).GetComponent<Enemy>();
            float r = Random.Range(950, 1100);
            e.transform.localPosition = new Vector2(Mathf.Sin(Random.Range(-500, 500)) * r, Mathf.Cos(Random.Range(-500, 500)) * r);
            enemies[index] = e;
            e.id = index;
        }
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
