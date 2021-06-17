using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem singleton { get; private set; }

    AudioSource source;

    void Awake() => singleton = this;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
