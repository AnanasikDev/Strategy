using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Canvas script;

    public Enemy[] enemies;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float startDelay;
    public float frequency; // frequency раз в секундку тик
    public short mass; // Мобов за тик
    [SerializeField] Transform EnemiesHandler;

    public float hpMult; // Коэффициент хп
    public float dmgMult; // Коэффициент дамага

    Canvas canvas;
    float minDistance;
    public static Spawner singleton { get; private set; }
    private void Start()
    {
        enemies = new Enemy[150];
        singleton = this;
        frequency = 1 / frequency;

        canvas = GetComponent<Canvas>();
        minDistance = Vector2.Distance(
            new Vector2(0, 0),
            new Vector2(Screen.width / canvas.scaleFactor / 2, Screen.height / canvas.scaleFactor / 2));

        InvokeRepeating("Spawn", startDelay, frequency);
    }
    void Spawn()
    {
        for (short _ = 0; _ < mass; _++)
        {
            short index = GetFreeSlot();
            if (index == -1) return;
            Enemy e = Instantiate(enemyPrefab, EnemiesHandler).GetComponent<Enemy>();
            float r = Random.Range(minDistance + 10, minDistance + 150);
            e.transform.localPosition = new Vector2(Mathf.Sin(Random.Range(-500, 500)) * r, Mathf.Cos(Random.Range(-500, 500)) * r);
            enemies[index] = e;
            e.id = index;

            e.maxHp *= hpMult;
            e.hp *= hpMult;

            e.damage *= dmgMult;

            // Size
            /*float size = Random.Range(0.4f, 2f);
            e.transform.localScale *= size;
            e.speed *= 1 / size;
            e.damage *= size;
            e.hp *= size;
            e.kickDelay *= size;*/

            float size = Random.Range(0.4f, 0.7f);
            if (size > 0.65f && GameManager.singleton.level > 2) size *= Random.Range(0.9f, 2.1f);

            e.transform.localScale *= size * 1.1f;
            e.speed *= 1 / size * 0.8f;
            e.damage *= size * 0.85f;
            e.hp *= size * 0.6f;
            e.kickDelay *= size * 1.2f;
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
