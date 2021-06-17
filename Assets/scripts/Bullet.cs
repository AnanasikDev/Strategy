using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public bool destroy;
    private void Start()
    {
        Destroy(gameObject, 5);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            other.GetComponent<Enemy>().GetDamage(damage);
            if (destroy) Destroy(gameObject);
        }
    }
}
