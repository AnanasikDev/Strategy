using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] GameObject CurrentBuilding;
    Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Transform obj = Instantiate(CurrentBuilding, transform).transform;
            obj.position = mousePos; //mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1))
        }
    }
    public void ChangeCurrentBuilding(GameObject value)
    {
        CurrentBuilding = value;
    }
}
