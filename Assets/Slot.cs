using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject handle; // This object is attached to this slot
    public bool Empty { get { return handle == null; } }
    public bool Create(GameObject newHandle)
    {
        if (handle == null)
        {
            handle = newHandle;
            return true;
        }
        return false;
    }
    public bool Destroy()
    {
        if (handle != null)
        {
            Destroy(handle);
            return true;
        }
        return false;
    }
}