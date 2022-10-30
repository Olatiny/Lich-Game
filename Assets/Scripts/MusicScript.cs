using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource ambience; // has its clip stored inside
    public AudioSource music;
    public AudioSource sfx;

    public AudioClip menuSong;
    public AudioClip fallingSong;

    public AudioClip deathCue; // all sfx will be played as oneshots through sfx
    [SerializeField]
    public List<AudioClip> healthCollect;

    // Start is called before the first frame update
    void Start()
    {
        ambience.volume = 0.02f; // ambience will fade in on the first call to menuMusic
        ambience.Play(); // this makes it so the ambience can loop
        
        
        // altered MenuMusic() for the first time
        // Maybe add volume slider later
        
        // fade the ambience up VERY SLOW
        StartCoroutine(FadeAudioSource.StartFade(ambience, 3.5f, 0.15f)); // does nothing if it is already high

        music.volume = 0.2f;
        music.clip = menuSong;
        music.Play();
    }


    private void Update()
    {
        
    }


    public void UpdateMusic(GameManager.gameState state) // this is the state to change to
    {
        switch (state)
        {
            case GameManager.gameState.menu:
                // play the menu music, fade in the ambience
                MenuMusic();
                break;

            case GameManager.gameState.falling:
                // play the falling music, lower the ambience
                FallingMusic();
                break;

            case GameManager.gameState.loot_room:
                // play just the ambience noise in the loot room
                // while in the loot room, play the cue when the player grabs the relic
                break;

            case GameManager.gameState.die:
                // play the death cue once, fade up the ambience
                DieMusic();
                break;
        }
    }

    public void MenuMusic()
    {
        // fade the ambience up
        StartCoroutine(FadeAudioSource.StartFade(ambience, 2f, 0.17f)); // does nothing if it is already high

        music.clip = menuSong;
        music.Play();
    }

    public void FallingMusic()
    {
        // lower the ambience slightly for the gameplay music
        StartCoroutine(FadeAudioSource.StartFade(ambience, 2f, 0.07f));

        music.clip = fallingSong;
        music.volume = 0.3f;
        music.Play();


    }

    public void DieMusic()
    {
        music.Stop(); // no music when die

        // fade the ambience back up
        StartCoroutine(FadeAudioSource.StartFade(ambience, 2f, 0.15f));
        
        // AND play the death cue
        sfx.PlayOneShot(deathCue, 0.2f);
    }

    public void HealthSFX()
    {
        sfx.PlayOneShot(healthCollect[UnityEngine.Random.Range(0, healthCollect.Count)]);
    }
}



// code borrowed from online, fades audio out
// use:
// StartCoroutine(FadeAudioSource.StartFade(AudioSource audioSource, float duration, float targetVolume));
public static class FadeAudioSource
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}