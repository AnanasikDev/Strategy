using UnityEngine;

public class SlotGenerator : MonoBehaviour
{
    // Canvas script;

    Vector2 fieldSize = new Vector2(1000, 500);
    public Slot[] slots;
    short slotSize = 20;
    [SerializeField] GameObject slotPrefab;

    public static SlotGenerator singleton { get; private set; }
    private void Start()
    {
        singleton = this;
        slots = new Slot[(int)(fieldSize.x * fieldSize.y / slotSize / slotSize)];
        short index = 0;
        for (short i = 0; i < fieldSize.x / slotSize; i++)
        {
            for (short j = 0; j < fieldSize.y / slotSize; j++)
            {
                Slot slot = Instantiate(slotPrefab, new Vector2(i*slotSize, j*slotSize), Quaternion.identity, transform).GetComponent<Slot>();
                slots[index] = slot;
                index++;
            }
        }
    }
}
