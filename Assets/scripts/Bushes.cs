using UnityEngine;

public class Bushes : MonoBehaviour
{
    // Canvas script;

    [SerializeField] GameObject Bush;
    [SerializeField] Transform max;
    [SerializeField] Transform min;
    [SerializeField] Transform Handler;
    void Generate()
    {
        int amount = Random.Range(0, 14);
        for (short _ = 0; _ < amount; _++)
        {
            Transform bush = Instantiate(Bush, Handler).transform;
            bush.localPosition = new Vector2(
                Random.Range(min.localPosition.x, max.localPosition.x), 
                Random.Range(min.localPosition.y, max.localPosition.y));
        }
    }
    private void Start()
    {
        Generate();
    }
}
