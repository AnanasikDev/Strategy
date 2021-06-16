using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    Enemy enemyScript;
    private void Start()
    {
        enemyScript = transform.parent.GetComponent<Enemy>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 6 && !enemyScript.destroying) // Collision with building
        {
            enemyScript.Stop();
            enemyScript.DestroyBuilding(other.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6) // Collision with building
        {
            enemyScript.Go();
        }
    }
}
