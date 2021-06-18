using UnityEngine;
using System.Linq;
using TMPro;
public class Build : MonoBehaviour
{
    // Canvas script;

    public Building CurrentBuilding;
    [SerializeField] TextMeshProUGUI[] costTexts;

    public Building[] buildings;

    [SerializeField] Transform BuildingsHandler;
    Camera mainCamera;
    public bool buildable = true;
    [SerializeField] Transform selectedSlot;

    int mineCost = 75;
    public static Build singleton { get; private set; }
    private void Start()
    {
        singleton = this;
        mainCamera = Camera.main;
    }
    private void Update()
    {
        selectedSlot.transform.position = new Vector2(Mathf.RoundToInt(Input.mousePosition.x) / SlotGenerator.singleton.slotSize * SlotGenerator.singleton.slotSize,
                                                      Mathf.RoundToInt(Input.mousePosition.y) / SlotGenerator.singleton.slotSize * SlotGenerator.singleton.slotSize);
        if (CurrentBuilding != null)
        {
            if (Input.GetMouseButton(0) && buildable && Time.timeScale > 0)
        {
            Mine mine;
            bool m = CurrentBuilding.TryGetComponent<Mine>(out mine);
            if (CurrentBuilding.cost <= GameManager.singleton.money ||
               (m && mineCost <= GameManager.singleton.money))
            {
                Vector3 mousePos = Input.mousePosition;
                Slot slot = SlotGenerator.singleton.slots.OrderBy(s => (s.transform.position - mousePos).sqrMagnitude).First();
                if (slot.Empty)
                {
                    Transform obj = Instantiate(CurrentBuilding, BuildingsHandler).transform;
                    obj.position = slot.transform.position;
                    slot.Create(obj.gameObject);
                    buildings[GetFreeId()] = obj.GetComponent<Building>();
                    GameManager.singleton.money -= CurrentBuilding.cost;
                    if (m)
                    {
                        mineCost = Mathf.RoundToInt(mineCost * 1.75f);
                        costTexts[CurrentBuilding.id].text = mineCost.ToString();
                    }
                }

            }
        }
        }
        if (Input.GetMouseButtonDown(1) && Time.timeScale > 0)
        {
            Vector3 mousePos = Input.mousePosition;
            Slot slot = SlotGenerator.singleton.slots.OrderBy(s => (s.transform.position - mousePos).sqrMagnitude).First();
            if (!slot.Empty)
            {
                slot.Destroy();
            }
        }
    }
    int GetFreeId()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i] == null) return i;
        }
        return -1;
    }
    public int GetBuildedId()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i] != null) return i;
        }
        return -1;
    }
}