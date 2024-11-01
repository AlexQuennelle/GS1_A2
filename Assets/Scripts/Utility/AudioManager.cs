using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Singleton;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource exploreSound;
    public AudioClip exploreClip1;
    public AudioClip exploreClip2;
    public AudioSource combat_Small;
    public AudioSource combat_Boss; 
    public AudioSource attackSound;
    public AudioSource healSound;
    public AudioSource hitEnemy;
    public AudioSource killEnemy;

    private bool pause = false;

    private bool isPlayingFirstClip = true;

    private void Start()
    {
        exploreSound.clip = exploreClip1;
        exploreSound.Play();
        exploreSound.loop = false;
    }

    private void Update()
    {
        if (pause)
            return;
        if (!exploreSound.isPlaying)
        {
            exploreSound.clip = isPlayingFirstClip ? exploreClip2 : exploreClip1;
            exploreSound.Play();
            isPlayingFirstClip = !isPlayingFirstClip;
        }
    }

    public void PlayExploreSound()
    {
        if (!exploreSound.isPlaying)
        {
            exploreSound.Play();
        }
    }

    public void PauseExploreSound()
    {
        if (exploreSound.isPlaying)
        {
            pause = true;
            exploreSound.Pause();
        }
    }

    public void PlayCombatSmallSound() => combat_Small?.Play();
    public void PauseCombatSmallSound() => combat_Small?.Pause();

    public void PlayCombatBossSound() => combat_Boss?.Play();
    public void PauseCombatBossSound() => combat_Boss?.Pause();

    public void PlayHitSound() => attackSound?.Play();

    public void PlayHealSound() => healSound?.Play();

    public void PlayhitEnemy() => hitEnemy?.Play();

    public void PlaykillEnemy() => killEnemy?.Play();
}
