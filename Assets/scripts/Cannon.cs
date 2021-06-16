using UnityEngine;
using System.Linq;
public class Cannon : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] float distance;
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform handler;
    Building building;
    private void Start()
    {
        distance *= 1000;
        building = GetComponent<Building>();
        InvokeRepeating("Aim", 0.2f, 0.2f);
    }
    void Aim()
    {
        if (!Spawner.singleton.ContainEnemy() || !building.alive) return;

        enemy = Spawner.singleton.enemies

            .Where(e => e != null)
            .Where(e => (e.transform.position - transform.position).sqrMagnitude < distance)
            .OrderBy(e => (e.transform.position - transform.position).sqrMagnitude).FirstOrDefault();

        if (enemy == null || !enemy.gameObject.activeSelf) return;

        handler.transform.LookAt(enemy.transform);

        Shoot();
    }
    void Shoot()
    {
        Instantiate(Bullet, transform.position, handler.transform.rotation, GameManager.singleton.transform);
    }
}
