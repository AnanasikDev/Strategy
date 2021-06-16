using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Waves, money, lose and win logic

    public Transform TownHall;
    [SerializeField] GameObject loseScreen;
    public static GameManager singleton { get; private set; }
    private void Awake()
    {
        singleton = this;
    }
    public void Lose()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }
}
