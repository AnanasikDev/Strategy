using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Waves, money, lose and win logic

    public Transform TownHall;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject ExitScreen;

    [SerializeField] Sprite pauseImg;
    [SerializeField] Sprite continueImg;
    [SerializeField] Image Img;

    [SerializeField] TextMeshProUGUI killsText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI damageText;

    int _kills;
    public int kills 
    {
        get { return _kills; }
        set
        {
            _kills = value;
            killsText.text = "kills: " + _kills.ToString();

            if (_kills % 100 == 0 && _kills != 0) level++;

            if (kills % 5 == 0) money++;
        }
    }
    int _money = 100;
    public int money
    {
        get { return _money; }
        set
        {
            if (_money != value)
            {
                _money = value;
                moneyText.text = "coins: " + _money.ToString();
            }
        }
    }
    int _level = 1;
    public int level
    {
        get { return _level; }
        set
        {
            if (_level == value || _level == 0) return;

            _level = value;
            levelText.text = "level: " + _level.ToString();

            if (_level % 10 == 0) // Увеличиваем и частоту, и кол-во за раз
            {
                Spawner.singleton.frequency *= 1.5f;
                Spawner.singleton.mass *= 2;
            }
            if (_level % 5 == 0)
            {
                Spawner.singleton.hpMult *= 1.5f;
                Spawner.singleton.dmgMult *= 1.2f;
            }
            else if (_level % 2 == 0) // Увеличиваем частоту
            {
                Spawner.singleton.frequency *= 1.25f;
            }
            else // Увеличиваем кол-во за раз
            {
                Spawner.singleton.mass = (short)Mathf.RoundToInt(Spawner.singleton.mass * 1.15f);
            }
        }
    }
    float _damage = 0;
    public float damage
    {
        get { return _damage; }
        set
        {
            if (_damage == value) return;

            _damage = value;
            damageText.text = "damage: " + Mathf.RoundToInt(_damage).ToString();
        }
    }
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
    public void ConfirmExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
    public void DenyExit()
    {
        ExitScreen.SetActive(false);
        Continue();
    }
    public void Exit()
    {
        ExitScreen.SetActive(true);
        Pause();
    }
    bool paused = false;
    void Pause()
    {
        Time.timeScale = 0;
        Img.sprite = continueImg;
    }
    void Continue()
    {
        Time.timeScale = 1;
        Img.sprite = pauseImg;
    }
    public void ClickPauseBtn()
    {
        paused = !paused;
        if (paused) Pause();
        else Continue();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ClickPauseBtn();
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q)) Exit();
        if (Input.GetKeyDown(KeyCode.F1)) Application.runInBackground = !Application.runInBackground;
    }
}
