using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private Dictionary<string, AudioClip> sfx = new Dictionary<string, AudioClip>();
    private AudioSource _audioSource;

    private void Awake()
    {
        if(instance == null) instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        sfx.Add("PistolAttack", Resources.Load<AudioClip>("Sound/Effect/PistolAttack"));
        sfx.Add("RifleAttack", Resources.Load<AudioClip>("Sound/Effect/RifleAttack"));
        sfx.Add("ShotgunAttack", Resources.Load<AudioClip>("Sound/Effect/ShotgunAttack"));
        sfx.Add("Swap", Resources.Load<AudioClip>("Sound/Effect/Swap"));
    }

    public void PlaySFX(string audioName)
    {
        _audioSource.PlayOneShot(sfx[audioName]);
    }
}
