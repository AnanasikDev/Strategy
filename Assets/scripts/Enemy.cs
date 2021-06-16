using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    bool stopped = false;
    [SerializeField] float speed;
    [SerializeField] Transform Direction;
    private void Start()
    {
        // Getting direction to move
        Direction.transform.LookAt(GameManager.singleton.TownHall);
    }
    private void Update()
    {
        if (!stopped)
        {
            transform.Translate(Direction.forward * speed);
        }
    }
    public void DestroyBuilding(GameObject buildingObject)
    {
        IEnumerator c = DamageBuilding(buildingObject);
        StartCoroutine(c);

    }
    IEnumerator DamageBuilding(GameObject buildingObject)
    {
        yield return new WaitForSeconds(1);
        Destroy(buildingObject); // needa destroy from slot
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
