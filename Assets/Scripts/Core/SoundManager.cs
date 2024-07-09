using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioSource m_AudioSource;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public static void PlayAudioClip(AudioClip clip)
    {
        m_AudioSource.volume = 1f;
        m_AudioSource.pitch = 1f;
        m_AudioSource.PlayOneShot(clip);
    }

    public static void PlayAudioClipPitch(AudioClip clip, float pitch)
    {
        m_AudioSource.volume = 1f;
        m_AudioSource.pitch = pitch;
        m_AudioSource.PlayOneShot(clip);
    }

    public static void PlayAudioClipVolume(AudioClip clip, float volume)
    {
        m_AudioSource.volume = volume;
        m_AudioSource.pitch = 1f;
        m_AudioSource.PlayOneShot(clip);
    }
}
