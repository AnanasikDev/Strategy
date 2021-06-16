using UnityEngine;
using System.Linq;
using TMPro;
public class Build : MonoBehaviour
{
    // Canvas script;

    public Building CurrentBuilding;
    [SerializeField] TextMeshProUGUI[] costTexts;

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
                GameManager.singleton.money -= CurrentBuilding.cost;
                Mine mine;
                if (CurrentBuilding.TryGetComponent<Mine>(out mine))
                {
                    CurrentBuilding.cost = Mathf.RoundToInt(Mathf.Pow(CurrentBuilding.cost, 1.25f) / 1.5f);
                    costTexts[CurrentBuilding.id].text = CurrentBuilding.cost.ToString();
                }
            }
        }
    }
}