using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    // Waves, money, lose and win logic

    public Transform TownHall;
    [SerializeField] GameObject loseScreen;

    int _kills;
    public int kills 
    {
        get { return _kills; }
        set
        {
            _kills = value;
            killsText.text = "kills: " + _kills.ToString();

            level = _kills / 100;
        }
    }
    [SerializeField] TextMeshProUGUI killsText;
    public int money;
    [SerializeField] TextMeshProUGUI moneyText;
    int _level = 1;
    public int level
    {
        get { return _level; }
        set
        {
            if (_level == value) return;
            _level = value;
            levelText.text = "level: " + _level.ToString();

            if (_level % 10 == 0) // ����������� � �������, � ���-�� �� ���
            {
                Spawner.singleton.frequency *= 1.5f;
                Spawner.singleton.mass *= 2;
            }
            else if (_level % 2 == 0) // ����������� �������
            {
                Spawner.singleton.frequency *= 1.25f;
            }
            else // ����������� ���-�� �� ���
            {
                Spawner.singleton.mass = (short)Mathf.RoundToInt(Spawner.singleton.mass * 1.5f);
            }
        }
    }
    [SerializeField] TextMeshProUGUI levelText;
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
