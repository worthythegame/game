using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	// Start is called before the first frame update
	void Start()
	{
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "Menu")
		{
			this.Play("MainTheme");
		}
		if (scene.name == "Level1")
		{
			this.Play("forestamb");
			this.Play("1worthy");
		}
		if (scene.name == "Level2")
		{
			this.Play("2worthy");
		}
        if (scene.name == "Level3")
        {
            this.Play("3worthy");
        }
        if (scene.name == "Level4")
        {
            this.Play("3worthy");
        }
        if (scene.name == "Level4")
        {
            this.Play("4worthy");
        }
        if (scene.name == "Level5")
        {
            this.Play("5worthy");
        }
        if (scene.name == "Level6")
        {
            this.Play("6worthy");
        }
        if (scene.name == "Level7")
        {
            this.Play("7worthy");
        }
    }


	void Awake()
	{

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void Stop(string sound)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Stop();
    }

	public void Pause(string sound)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Pause();
	}

	public void UnPause(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.UnPause();
	}

	public void Destroy()
    {
		Destroy(gameObject);
    }
}
