using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class Transaction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float a1;
    public float a2;
    public float a3;
    public float a4;
    public string format;

    [SerializeField] GameObject activeTransaction;
    [SerializeField] TextMeshProUGUI info;
    public string GetInfo()
    {
        return format.Replace("a1", a1.ToString())
                     .Replace("a2", a2.ToString())
                     .Replace("a3", a3.ToString())
                     .Replace("a4", a4.ToString());
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        activeTransaction.transform.position = transform.position;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        info.text = GetInfo();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        info.text = "";
    }
}
