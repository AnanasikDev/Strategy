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
    void mine()
    {
        GameManager.singleton.money += mass;
    }
}
