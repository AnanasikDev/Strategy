using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Waves, money, lose and win logic

    public Transform TownHall;

    public static GameManager singleton { get; private set; }
    private void Awake()
    {
        singleton = this;
    }
}
