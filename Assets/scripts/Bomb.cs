using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float frequency;

    Building building;
    WaitForSeconds wait;
    Enemy enemy;
    private void Start()
    {
        frequency = 1 / frequency;
        wait = new WaitForSeconds(frequency);
        building = GetComponent<Building>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7) // enemy
        {
            enemy = other.GetComponent<Enemy>();
            if (gameObject.activeSelf) StartCoroutine("Damage");
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 7) // enemy
        {
            enemy = other.GetComponent<Enemy>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 7) // enemy
        {
            enemy = null;
        }
    }
    IEnumerator Damage()
    {
        if (enemy != null && gameObject.activeSelf)
        {
            enemy.GetDamage(damage);
            if (building.GetDamage(damage))
            {
                yield break;
            }
            yield return wait;  
            yield return Damage();
        }
    }
}
