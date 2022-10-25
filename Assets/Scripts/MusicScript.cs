using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource ambience;

    public AudioSource music;

    public AudioClip menuSong;

    public AudioClip song2;

    // Start is called before the first frame update
    void Start()
    {
        //Play();

        music.volume = 0.1f;
        music.clip = menuSong;
        music.Play();


        ambience.volume = 0.1f;
        ambience.Play();


    }

    private void Update()
    {
        
    }

    void StartGame()
    {
        if (music.clip == menuSong)
            music.clip = song2;
        else
            music.clip = menuSong;
        music.Play();
        //source.Play();

        //source.clip = clip1;
    }

    public void UpdateMusic(GameManager.gameState state) // this is the state to change to
    {
        switch (state)
        {
            case GameManager.gameState.menu:
                // play the menu music
                // if the ambience is not already playing, play that
                break;

            case GameManager.gameState.falling:
                // play the falling music
                // cut the ambience
                break;

            case GameManager.gameState.loot_room:
                // play just the ambience noise in the loot room
                // while in the loot room, play the cue when the player grabs the relic
                break;

            case GameManager.gameState.die:
                // play the death cue once
                // also play the ambience
                break;
        }
    }
}
