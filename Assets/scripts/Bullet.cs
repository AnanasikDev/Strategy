using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;
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
        }
    }
}
