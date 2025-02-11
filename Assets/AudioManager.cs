using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("----------------Audio Source ----------------")]
    [SerializeField] AudioSource m_Source;
    [SerializeField] AudioSource sFxSource;

    [Header("----------------Audio Clip ----------------")]

    public AudioClip background;
    public AudioClip death;
    public AudioClip gunshot;
    public AudioClip sound_run;

    public AudioClip ui_click;
    public AudioClip break_die;





    void Start()
    {
        m_Source.clip = background;
        m_Source.Play();
    }

    public void playSFX(AudioClip clip)
    {
        sFxSource.PlayOneShot(clip);
    }
}
