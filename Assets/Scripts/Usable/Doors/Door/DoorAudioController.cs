using UnityEngine;
using System.Collections;
using System;
 
public class DoorAudioController : MonoBehaviour{
    private DoorDebugManager DoorDebugManager;

    public float AudioDelay;
    public AudioClip[] AudioClips;

    private bool IsAudioDelayed, CanAudioPlay = true;

    private void Awake(){DoorDebugManager = GetComponent<DoorDebugManager>();}
    private void Update(){DoorDebugManager.IsAudioDelayed = IsAudioDelayed;}

    public void OnDoorTransitionAudioLocked(object source, AudioSource AudioSource)
    {
         AudioPlay(AudioSource, AudioClips[2], true, AudioDelay);
    }
    public void OnDoorTransitionAudioStart(object source, AudioSource AudioSource)
    {
         AudioPlay(AudioSource, AudioClips[0]);
    }

    public void OnDoorTransitionAudioEnd(object source, AudioSource AudioSource)
    {
         AudioPlay(AudioSource, AudioClips[1]);
    }

    private void AudioPlay(AudioSource AudioSource, AudioClip AudioClip, bool IsDelayed = false, float Delay = 0)
    {
            IsAudioDelayed = IsDelayed;
  
            if (CanAudioPlay == true)
            {
                CanAudioPlay = false;
                StartCoroutine(WaitForSound(AudioSource, AudioClip, Delay));
            }             
    }

    private IEnumerator WaitForSound(AudioSource AudioSource, AudioClip AudioClip, float Delay)
    {
        AudioSource.clip = AudioClip;

        if (IsAudioDelayed == false)
        {         
            AudioSource.Play();
        }
        else
        {
            AudioSource.Play();
            yield return new WaitForSeconds(AudioClip.length + Delay);
            IsAudioDelayed = false;
        }
 
        CanAudioPlay = true;
    }
}
