using UnityEngine;

public class TownHall : MonoBehaviour
{
    private void OnDisable()
    {
        GameManager.singleton.Lose();
    }
}
