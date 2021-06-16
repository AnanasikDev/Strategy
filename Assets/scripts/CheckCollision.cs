using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    Enemy enemyScript;
    private void Start()
    {
        enemyScript = transform.parent.GetComponent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) // Collision with building
        {
            enemyScript.Stop();
            enemyScript.Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6) // Collision with building
        {
            enemyScript.Go();
        }
    }
}
