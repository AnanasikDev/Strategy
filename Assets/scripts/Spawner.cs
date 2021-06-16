using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Canvas script;

    public Enemy[] enemies;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] public float frequency; // frequency ��� � �������� ���
    [SerializeField] public short mass; // ����� �� ���
    public static Spawner singleton { get; private set; }
    private void Start()
    {
        enemies = new Enemy[60];
        singleton = this;
        frequency = 1 / frequency;
        InvokeRepeating("Spawn", frequency, frequency);
    }
    void Spawn()
    {
        for (short _ = 0; _ < mass; _++)
        {
            short index = GetFreeSlot();
            if (index == -1) return;
            Enemy e = Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
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
