using UnityEngine;
using TMPro;
public class Building : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] float maxHp;
    public short id;
    public int cost;
    public bool alive { get { return hp > 0; } }
    [SerializeField] Transform hpBar;
    
    public bool GetDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            //Destroy(gameObject, 0.1f);
            gameObject.SetActive(false);
            return true;
        }
        hpBar.localPosition = new Vector2(-(100 - hp/maxHp*100), 0);
        return false;
    }
    private void OnDisable()
    {
        //Destroy(gameObject, 2f);
    }
}
