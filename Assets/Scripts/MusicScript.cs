using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    /** MUSIC SCRIPT "API"
     * UpdateMusic(gameState);  updates the audio based on gamestate change
     * PauseAdjust();           ducks the music down when paused        (ONLY USE WHILE IN FALLING GAMESTATE)
     * UnpauseAdjust();         brings the music back up on unpause     (ONLY USE WHILE IN FALLING GAMESTATE)
     * HealthSFX();             plays health sound effect
     * DamageSFX();             ...figure it out
     * EnemyThrowSFX();         ...
     * ButtonSFX();             ...
     * RelicCollectSFX();       ...
     * 
     * FallRumble();            plays ground tremble in cutscene then... 
     * FallScream();            plays cartoon ahhhhhh once the falling starts
     */

    public AudioSource ambience; // has its clip stored inside
    public AudioSource music;
    public AudioSource sfx;

    public AudioClip menuSong;
    public float menuVolume = 0.17f;

    public AudioClip fallingSong;
    public float spookzoomVolume = 0.17f;

    public AudioClip deathCue; // all sfx will be played as oneshots through sfx

    public AudioClip relicCollect;

    public List<AudioClip> healthCollect;
    public List<AudioClip> damageNoise;
    public List<AudioClip> enemyThrow;
    public List<AudioClip> buttonClick;

    public AudioClip deathSFX;

    public List<AudioClip> fallScream;
    public AudioClip groundRumble; // maybe make this a list too

    // Start is called before the first frame update
    void Start()
    {
        ambience.volume = 0.02f; // ambience will fade in on the first call to menuMusic
        ambience.Play(); // this makes it so the ambience can loop
        
        
        // altered MenuMusic() for the first time
        // Maybe add volume slider later
        
        // fade the ambience up VERY SLOW
        StartCoroutine(FadeAudioSource.StartFade(ambience, 3.5f, 0.15f)); // does nothing if it is already high

        music.volume = menuVolume;
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

    public void PauseAdjust()
    {
        //StartCoroutine(FadeAudioSource.StartFade(music, .1f, menuVolume / 2));
        music.volume = menuVolume / 2;
    }

    public void UnpauseAdjust()
    {
        //StartCoroutine(FadeAudioSource.StartFade(music, .1f, menuVolume));
        music.volume = menuVolume;
    }

    public void MenuMusic()
    {
        // fade the ambience up
        StartCoroutine(FadeAudioSource.StartFade(ambience, 2f, 0.17f)); // does nothing if it is already high

        music.clip = menuSong;
        music.Play();
    }

    public void FadeMenuMusic()
    {
        // fades the music out
        StartCoroutine(FadeAudioSource.StartFade(music, 2f, 0f));
    }

    public void FallingMusic()
    {
        // lower the ambience slightly for the gameplay music
        StartCoroutine(FadeAudioSource.StartFade(ambience, 2f, 0.07f));


        music.clip = fallingSong;
        music.volume = spookzoomVolume;
        music.Play();

    }

    public void DieMusic()
    {
        music.Stop(); // no music when die

        // fade the ambience back up
        StartCoroutine(FadeAudioSource.StartFade(ambience, 2f, 0.15f));
        
        // AND play the death cue
        sfx.PlayOneShot(deathCue, 0.2f);
        sfx.PlayOneShot(deathSFX, 0.1f);
    }


    public void HealthSFX()
    {
        sfx.PlayOneShot(healthCollect[UnityEngine.Random.Range(0, healthCollect.Count)]);
    }
    public void DamageSFX()
    {
        sfx.PlayOneShot(damageNoise[UnityEngine.Random.Range(0, damageNoise.Count)]);
    }
    public void EnemyThrowSFX()
    {
        sfx.PlayOneShot(enemyThrow[UnityEngine.Random.Range(0, enemyThrow.Count)]);
    }
    public void ButtonSFX()
    {
        sfx.PlayOneShot(buttonClick[UnityEngine.Random.Range(0, buttonClick.Count)]);
    }
    public void RelicCollectSFX()
    {
        // maybe fade music out and then back in much more slowly
        sfx.PlayOneShot(relicCollect, 0.3f); // can add volume thing later
    }

    public void FallRumble()
    {
        // play ground tremble
        sfx.PlayOneShot(groundRumble, 0.75f);
    }

    public void FallScream()
    {
        sfx.PlayOneShot(fallScream[UnityEngine.Random.Range(0, fallScream.Count)], 0.2f);
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