using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class Transaction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float damage;
    public float hp;
    public float radius;

    [SerializeField] TextMeshProUGUI info;
    public string GetInfo()
    {
        return $"Урон: {damage}<br>Стойкость: {hp}<br>Радиус действия: {radius}";
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
