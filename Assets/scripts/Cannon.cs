using UnityEngine;
using System.Linq;
public class Cannon : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] float distance;
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform handler;
    [SerializeField] float freq;
    [SerializeField] float damage;
    [SerializeField] float bulletSpeed;
    Building building;
    bool enemyNear = false;
    private void Start()
    {
        distance *= 1000;
        building = GetComponent<Building>();
        freq = 1 / freq;

        InvokeRepeating("Aim", 0, 0.1f);
        InvokeRepeating("Shoot", 0, freq);
    }
    void Aim()
    {
        if (!Spawner.singleton.ContainEnemy() || !building.alive)
        {
            enemyNear = false;
            return;
        }

        enemy = Spawner.singleton.enemies

            .Where(e => e != null)
            .Where(e => (e.transform.position - transform.position).sqrMagnitude < distance)
            .OrderBy(e => (e.transform.position - transform.position).sqrMagnitude).FirstOrDefault();

        if (enemy == null || !enemy.gameObject.activeSelf)
        {
            enemyNear = false;
            return;
        }

        handler.transform.LookAt(enemy.transform);
        enemyNear = true;
    }
    void Shoot()
    {
        if (enemyNear)
        {
            Bullet b = Instantiate(Bullet, transform.position, handler.transform.rotation, GameManager.singleton.transform).GetComponent<Bullet>();
            b.damage = damage;
            b.speed = bulletSpeed;
            b.destroy = true;
        }
            
    }
}
