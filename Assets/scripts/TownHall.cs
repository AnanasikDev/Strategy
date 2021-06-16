using UnityEngine;
using System.Linq;
public class TownHall : MonoBehaviour
{
    public static TownHall singleton { get; private set; }
    void Awake() => singleton = this;
    public void Init()
    {
        Slot slot = SlotGenerator.singleton.slots.OrderBy(s => (s.transform.position - transform.position).sqrMagnitude).First();
        if (slot.Empty)
        {
            transform.position = slot.transform.position;
            slot.Create(gameObject);
        }
    }
    private void OnDisable()
    {
        GameManager.singleton.Lose();
    }
}
