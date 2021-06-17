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
    public static Build singleton { get; private set; }
    private void Start()
    {
        singleton = this;
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (CurrentBuilding != null)
        if (Input.GetMouseButtonDown(0) && buildable && Time.timeScale > 0 && CurrentBuilding.cost <= GameManager.singleton.money)
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
                Mine mine;
                if (CurrentBuilding.TryGetComponent<Mine>(out mine))
                {
                    CurrentBuilding.cost = Mathf.RoundToInt(CurrentBuilding.cost * 1.75f);
                    costTexts[CurrentBuilding.id].text = CurrentBuilding.cost.ToString();
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && Time.timeScale > 0)
        {
            Vector3 mousePos = Input.mousePosition;
            Slot slot = SlotGenerator.singleton.slots.OrderBy(s => (s.transform.position - mousePos).sqrMagnitude).First();
            if (!slot.Empty)
            {
                Destroy(slot.handle);
                slot.handle = null;
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