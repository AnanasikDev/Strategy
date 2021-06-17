using UnityEngine;
using System.Collections;
using System.Linq;
public class Tesla : MonoBehaviour
{
    [SerializeField] Transform zipper; // Молния
    [SerializeField] float damage;
    WaitForSeconds waitForZipper;
    [SerializeField] float distance;
    [SerializeField] float Freq;
    [SerializeField] float waitForZip;
    Enemy enemy;
    private void Start()
    {
        waitForZipper = new WaitForSeconds(waitForZip);
        distance *= 1000;
        Freq = 1 / Freq;
        InvokeRepeating("Damage", Freq/2, Freq);
    }
    public void Damage()
    {
        if (!Spawner.singleton.ContainEnemy()) return;

        enemy = Spawner.singleton.enemies
            
            .Where(e => e != null)
            .Where(e => (e.transform.position - transform.position).sqrMagnitude < distance)
            .OrderBy(e => (e.transform.position - transform.position).sqrMagnitude).FirstOrDefault();

        if (enemy == null || !enemy.gameObject.activeSelf)
        {
            zipper.gameObject.SetActive(false);
            return;
        }

        zipper.LookAt(enemy.transform);
        SetZipper(transform.position, enemy.transform.position);
        //zipper.transform.localEulerAngles = new Vector3(0, 0, zipper.transform.localEulerAngles.z);
        zipper.gameObject.SetActive(true);
        enemy.GetDamage(damage);
        if (!gameObject.activeSelf) return;
        StartCoroutine("ZipperDisable");
    }
    void SetZipper(Vector2 startPos, Vector2 endPos)
    {
        float scale = Vector2.Distance(startPos, endPos);
        zipper.transform.localScale = new Vector3(1, 1, scale/460); // 262
    }
    IEnumerator ZipperDisable()
    {
        if (!gameObject.activeSelf) yield break;
        yield return waitForZipper;
        zipper.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (enemy != null && enemy.gameObject.activeSelf && zipper.gameObject.activeSelf) 
            SetZipper(transform.position, enemy.transform.position);
    }
}
