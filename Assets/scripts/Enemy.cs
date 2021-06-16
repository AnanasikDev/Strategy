using UnityEngine;
using System.Collections;
using System.Linq;
public class Enemy : MonoBehaviour
{
    // Moving
    bool stopped = false;
    [SerializeField] float speed;

    // Kicking
    [SerializeField] float damage;
    [SerializeField] float kickDelay;

    [SerializeField] Transform Direction;

    // Hp
    [SerializeField] float hp;
    [SerializeField] float maxHp;
    [SerializeField] Transform hpBar;

    WaitForSeconds wait;
    public short id;
    public bool destroying = false;
    private void Start()
    {
        wait = new WaitForSeconds(kickDelay);
        InvokeRepeating("GetDirInfo", 0, 0.5f);
    }
    void GetDirInfo()
    {
        Transform nearestBuilding;

        if (Build.singleton.GetBuildedId() != -1)
        {
            nearestBuilding = Build.singleton.buildings
                .Where(b => b != null)
                .OrderBy(b => (transform.position - b.transform.position).sqrMagnitude).First().transform;
        }

        else nearestBuilding = GameManager.singleton.TownHall;

        Direction.transform.LookAt(nearestBuilding);
    }
    private void Update()
    {
        if (!stopped)
        {
            transform.Translate(Direction.forward * speed * Time.timeScale);
        }
    }
    public void DestroyBuilding(GameObject buildingObject)
    {
        destroying = true;
        Building building = buildingObject.GetComponent<Building>();
        IEnumerator c = DamageBuilding(building);
        StartCoroutine(c);
    }
    IEnumerator DamageBuilding(Building building)
    {
        if(hp > 0 && building.alive)
        {
            if (building.GetDamage(damage))
            {
                destroying = false;
                GetDirInfo();
                yield break;
            }
            yield return new WaitForSeconds(1);
            yield return DamageBuilding(building);
        }
        else destroying = false;
    }
    public bool GetDamage(float damage)
    {
        hp -= damage;
        GameManager.singleton.damage += damage;
        if (hp <= 0)
        {
            //Destroy(gameObject, 0.1f);
            gameObject.SetActive(false);
            return true;
        }
        hpBar.localPosition = new Vector2(-(100 - hp / maxHp * 100), 0);
        return false;
    }
    public void Stop()
    {
        stopped = true;
    }
    public void Go()
    {
        stopped = false;
    }
    private void OnDisable()
    {
        GameManager.singleton.kills++;
        Spawner.singleton.enemies[id] = null;
        Destroy(gameObject, 1);
    }
}
