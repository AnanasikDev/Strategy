using UnityEngine;
public class MegaCannon : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform handler;
    [SerializeField] float freq;
    [SerializeField] float damage;
    [SerializeField] float bulletSpeed;
    private void Start()
    {
        freq = 1 / freq;

        InvokeRepeating("Aim", 0, freq);
        InvokeRepeating("Shoot", 0.5f, freq);
    }
    void Aim()
    {
        handler.transform.localEulerAngles = new Vector3(Random.Range(0, 360), 90, 0);
    }
    void Shoot()
    {
        Bullet b = Instantiate(Bullet, transform.position, handler.transform.rotation, GameManager.singleton.transform).GetComponent<Bullet>();
        b.damage = damage;
        b.speed = bulletSpeed;
        b.destroy = false;
    }
}
