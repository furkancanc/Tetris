using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public bool musicEnabled = true;
    public bool fxEnabled = true;

    [Range(0, 1)]
    public float musicVolume = 1.0f;

    [Range(0, 1)]
    public float fxVolume = 1.0f;

    public AudioClip clearRowSound;
    public AudioClip moveSound;
    public AudioClip dropSound;
    public AudioClip gameOverSound;
    public AudioClip errorSound;

    public AudioSource musicSource;

    public AudioClip[] musicClips;

    AudioClip randomMusicClip;

    public AudioClip[] vocalClips;
    public AudioClip gameOverVocalClip;
    private void Start()
    {
        randomMusicClip = GetRandomClip(musicClips);
        PlayBackgroundMusic(randomMusicClip);
    }

    public AudioClip GetRandomClip(AudioClip[] clips)
    {
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        return randomClip;
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        // Return if music is disabled or if musicSource is null or is musicClip is null
        if (!musicEnabled || !musicClip || !musicSource)
        {
            return;
        }

        // if music is playing, stop it
        musicSource.Stop();

        musicSource.clip = musicClip;

        // set the music volume
        musicSource.volume = musicVolume;

        // music repeats forever
        musicSource.loop = true;

        // start playing
        musicSource.Play();
    }

    void UpdateMusic()
    {
        if (musicSource.isPlaying != musicEnabled)
        {
            if (musicEnabled)
            {
                randomMusicClip = GetRandomClip(musicClips);
                PlayBackgroundMusic(randomMusicClip);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }

    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        UpdateMusic();
    }

    public void ToggleFX()
    {
        fxEnabled = !fxEnabled;
    }
}
