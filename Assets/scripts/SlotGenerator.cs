using UnityEngine;

public class SlotGenerator : MonoBehaviour
{
    // Canvas script;

    Vector2 fieldSize;
    public Slot[] slots;
    public int slotsAmount;
    [SerializeField] short slotSize;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform slotsHandler;
    Canvas canvas;
    public static SlotGenerator singleton { get; private set; }
    private void Start()
    {
        canvas = GetComponent<Canvas>();
        singleton = this;

        fieldSize = new Vector2Int(
            (int)(Screen.width / canvas.scaleFactor) / slotSize * slotSize,
            (int)(Screen.height / canvas.scaleFactor) / slotSize * slotSize);
        //fieldSize = new Vector2(1200, 600);
        slotsAmount = Mathf.RoundToInt(fieldSize.x * fieldSize.y / slotSize / slotSize);
        slots = new Slot[slotsAmount];
        Build.singleton.buildings = new Building[slotsAmount];
        short index = 0;
        for (short i = 0; i < fieldSize.x / slotSize; i++)
        {
            for (short j = 0; j < fieldSize.y / slotSize; j++)
            {
                Slot slot = Instantiate(slotPrefab, new Vector2(i*slotSize, j*slotSize), Quaternion.identity, slotsHandler).GetComponent<Slot>();
                slots[index] = slot;
                index++;
            }
        }
        TownHall.singleton.Init();
    }
}
