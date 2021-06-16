using UnityEngine;
using System.Linq;
public class Build : MonoBehaviour
{
    // Canvas script;

    public Building CurrentBuilding;

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
            GameManager.singleton.money -= CurrentBuilding.cost;
            Vector3 mousePos = Input.mousePosition;
            Slot slot = SlotGenerator.singleton.slots.OrderBy(s => (s.transform.position - mousePos).sqrMagnitude).First();
            if (slot.Empty)
            {
                Transform obj = Instantiate(CurrentBuilding, BuildingsHandler).transform;
                obj.position = slot.transform.position;
                slot.Create(obj.gameObject);
            }
        }
    }
}