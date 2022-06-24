using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private List<AudioClip> audioClips;

    [SerializeField]
    [Min(0)]
    private float pitchOffset = 0.1f, volumeOffset = 0.05f;

    private float basePitch, baseVolume;

    private void Start()
    {
        if (source == null)
            return;

        basePitch = source.pitch;
        baseVolume = source.volume;
    }

    public void PlaySFX()
    {
        if (source == null || audioClips.Count == 0)
            return;

        source.pitch = basePitch + Random.Range(-pitchOffset, pitchOffset);
        source.volume = baseVolume + Random.Range(-volumeOffset, volumeOffset);
        int randomClipIndex = Random.Range(0, audioClips.Count);
        source.PlayOneShot(audioClips[randomClipIndex]);
    }
}
