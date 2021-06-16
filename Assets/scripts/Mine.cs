using UnityEngine;
using System.Linq;
public class Mine : MonoBehaviour
{
    [SerializeField] float freq;
    [SerializeField] int mass;
    private void Start()
    {
        freq = 1 / freq;
        InvokeRepeating("mine", freq, freq);
    }
    public void Init()
    {
        Slot slot = SlotGenerator.singleton.slots.OrderBy(s => (s.transform.position - transform.position).sqrMagnitude).First();
        if (slot.Empty)
        {
            transform.position = slot.transform.position;
            slot.Create(gameObject);
        }
    }
    void mine()
    {
        GameManager.singleton.money += mass;
    }
}
