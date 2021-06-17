using UnityEngine;
using UnityEngine.EventSystems;
public class Shop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator anim;
    bool opened = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("opened", false);
    }
    void Open()
    {
        anim.SetBool("opened", true);
    }
    void Close()
    {
        anim.SetBool("opened", false);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Build.singleton.buildable = false;
        Open();
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Build.singleton.buildable = true;
        Close();
    }
    public void SetBuilding(Building building)
    {
        Build.singleton.CurrentBuilding = building;
    }
    public void ResetBuilding()
    {
        Build.singleton.CurrentBuilding = null;
    }
}
