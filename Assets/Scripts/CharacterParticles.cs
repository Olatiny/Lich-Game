using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticles : MonoBehaviour
{
    [SerializeField][Tooltip("the particle system to play when the character gets levitated")]private ParticleSystem levitateParticles;
    [SerializeField][Tooltip("the particle system to play when the world gets rotated")]private ParticleSystem rotateParticles;
    [SerializeField][Tooltip("the particle system to play when the character gets hurt")]private ParticleSystem hurtParticles;
    [SerializeField][Tooltip("the particle system to play when the character gets healed")]private ParticleSystem healParticles;
    [SerializeField][Tooltip("the particle system to play when the character gets a relic")]private ParticleSystem relicParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFloating(){
        levitateParticles.Play();
    }

    public void StartRotating(){
        rotateParticles.Play();
    }

    public void StartFalling(){
        levitateParticles.Stop();
        levitateParticles.Clear();
        rotateParticles.Stop();
        rotateParticles.Clear();
    }

    public void Hurt(){
        hurtParticles.Play();
    }

    public void GainedHealth(){
        healParticles.Play();
        Debug.Log("health parts");
    }

    public void GainedRelic(){
        relicParticles.Play();
    }

    public void Reset(){
        levitateParticles.Stop();
        relicParticles.Stop();
        hurtParticles.Stop();
        healParticles.Stop();
        rotateParticles.Stop();
        rotateParticles.Clear();
        levitateParticles.Clear();
    }
}
