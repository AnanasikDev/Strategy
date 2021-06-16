using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    bool stopped = false;
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] Transform Direction;
    [SerializeField] float hp;
    [SerializeField] float kickDelay;
    WaitForSeconds wait;
    public bool destroying = false;
    private void Start()
    {
        // Getting direction to move
        Direction.transform.LookAt(GameManager.singleton.TownHall);

        wait = new WaitForSeconds(kickDelay);
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
                yield break;
            }
            yield return new WaitForSeconds(1);
            yield return DamageBuilding(building);
        }
        else destroying = false;
    }
    public void GetDamage()
    {

    }
    public void Stop()
    {
        stopped = true;
    }
    public void Go()
    {
        stopped = false;
    }
}
