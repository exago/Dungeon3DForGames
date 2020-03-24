using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioSource _instanceAudioSource = null;

    private static AudioManager _instance = null;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null && !_applicationIsQuiting)
            {
                GameObject gameObjectOfInstance = GameObject.FindGameObjectWithTag("AudioManager");

                if (gameObjectOfInstance == null)
                {
                    GameObject newObject = new GameObject("Singleton_AudioManager");
                    _instance = newObject.AddComponent<AudioManager>();
                    _instanceAudioSource = newObject.AddComponent<AudioSource>();
                    _instanceAudioSource.volume = GameSettings.Volume;
                    newObject.tag = "AudioManager";
                    Instantiate(newObject);
                }
                return _instance;
            }
            return _instance;
        }
    }

    private static bool _applicationIsQuiting = false;
    public void OnApplicationQuit()
    {
        _applicationIsQuiting = true;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
        {
            _instanceAudioSource = this.GetComponent<AudioSource>();
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    public void PlayAudioClip(AudioClip audioClip)
    {
        if (audioClip == null)
            Debug.Log("No audioClip");
        else
            _instanceAudioSource.PlayOneShot(audioClip);
    }
}
