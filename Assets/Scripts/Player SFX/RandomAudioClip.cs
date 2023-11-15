using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que seleciona um clipe aleatorio de audio,
/// para ser usado com a funcao PlaySound() em um Animator Event
/// </summary>

public class RandomAudioClip : MonoBehaviour
{
    public List<AudioClip> audioClips;
    
    private System.Random _rand;
    private List<GameObject> _soundObjects;
    private List<AudioSource> _audioSources;


    private void Awake()
    {
        _rand = new System.Random();
        _soundObjects = new List<GameObject>(audioClips.Count);
        _audioSources = new List<AudioSource>(audioClips.Count);

        for (int i = 0; i < audioClips.Count; i++)
        {
            var _gameObject = new GameObject();
            _soundObjects.Add(Instantiate(_gameObject));
            _soundObjects[i].AddComponent<AudioSource>();
            _soundObjects[i].GetComponent<AudioSource>().clip = audioClips[i];
            _audioSources.Add(_soundObjects[i].GetComponent<AudioSource>());
        }
    }

    public void PlaySound()
    {
        _audioSources[_rand.Next(audioClips.Count)].Play();
    }
}
