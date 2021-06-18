using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Enemy : MonoBehaviour
{
    // Moving
    bool stopped = false;
    public float speed;

    // Kicking
    public float damage;
    public float kickDelay;

    [SerializeField] Transform Direction;

    // Hp
    public float hp;
    public float maxHp;
    [SerializeField] Transform hpBar;
    [SerializeField] AudioClip gettingdamageSound;

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
            /*nearestBuilding = Build.singleton.buildings
                .Where(b => b != null)
                .Where(b => b.gameObject.layer != 8)
                .Sort(b => (transform.position - b.transform.position).sqrMagnitude).First().transform;
*/
            Building[] buildings = Build.singleton.buildings
                .Where(b => b != null)
                .Where(b => b.gameObject.layer != 8).ToArray();

            if (buildings.FirstEmpty() == -1) nearestBuilding = TownHall.singleton.transform;
            else nearestBuilding = buildings.OrderBy(b => (transform.position - b.transform.position).sqrMagnitude).First().transform;
        }

        else nearestBuilding = TownHall.singleton.transform;

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
        if (hp <= 0) return;
        destroying = true;
        Building building = buildingObject.GetComponent<Building>();
        IEnumerator c = DamageBuilding(building);
        StartCoroutine(c);
    }
    IEnumerator DamageBuilding(Building building)
    {
        if(hp > 0 && building.alive)
        {
            building.Damage(this);
            if (building.GetDamage(damage))
            {
                destroying = false;
                GetDirInfo();
                yield break;
            }
            yield return wait;
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
            AudioSystem.singleton.PlaySound(gettingdamageSound);
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
public static class Ext
{
    public static int FirstEmpty(this object[] collection, object format = null)
    {
        for (int i = 0; i < collection.Length; i++)
            if (collection[i] == format)
                return i;
        return -1;
    }
}