using UnityEngine;
using System.Linq;
public class Build : MonoBehaviour
{
    // Canvas script;

    [SerializeField] GameObject CurrentBuilding;
    Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0)
        {
            Vector3 mousePos = Input.mousePosition;
            Slot slot = SlotGenerator.singleton.slots.OrderBy(s => (s.transform.position - mousePos).sqrMagnitude).First();
            if (slot.Empty)
            {
                Transform obj = Instantiate(CurrentBuilding, transform).transform;
                obj.position = slot.transform.position;
                slot.Create(obj.gameObject);
            }
                //new Vector3(Mathf.RoundToInt(mousePos.x / 50) * 50, Mathf.RoundToInt(mousePos.y / 100) * 100, 0);
            //new Vector2(Mathf.RoundToInt(mousePos.x / 50) * 50, Mathf.RoundToInt(mousePos.y / 100) * 100);
        }
    }
    public void ChangeCurrentBuilding(GameObject value)
    {
        CurrentBuilding = value;
    }
}
