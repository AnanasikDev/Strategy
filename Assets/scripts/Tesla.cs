using UnityEngine;
using System.Collections;
using System.Linq;
public class Tesla : MonoBehaviour
{
    [SerializeField] Transform zipper; // Молния
    [SerializeField] float damage;
    WaitForSeconds waitForZipper;
    [SerializeField] float distance;
    private void Start()
    {
        waitForZipper = new WaitForSeconds(0.25f);
        distance *= 1000;
        InvokeRepeating("Damage", 0.5f, 0.5f);
    }
    public void Damage()
    {
        if (!Spawner.singleton.ContainEnemy()) return;

        Enemy enemy = Spawner.singleton.enemies
            
            .Where(e => e != null)
            .Where(e => (e.transform.position - transform.position).sqrMagnitude < distance)
            .OrderBy(e => (e.transform.position - transform.position).sqrMagnitude).FirstOrDefault();

        print(enemy);
        if (enemy == null || !enemy.gameObject.activeSelf)
        {
            zipper.gameObject.SetActive(false);
            return;
        }

        zipper.LookAt(enemy.transform);
        //zipper.transform.localEulerAngles = new Vector3(0, 0, zipper.transform.localEulerAngles.z);
        zipper.gameObject.SetActive(true);
        enemy.GetDamage(damage);
        if (!gameObject.activeSelf) return;
        StartCoroutine("ZipperDisable");
    }
    IEnumerator ZipperDisable()
    {
        if (!gameObject.activeSelf) yield break;
        yield return waitForZipper;
        zipper.gameObject.SetActive(false);
    }
}
