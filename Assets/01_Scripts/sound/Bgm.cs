using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    private static Bgm instance;
    public static Bgm Instance => instance;

    public AudioSource _audioSource01;
    public AudioSource _audioSource02;
    public AudioSource _audioSource03;

    public Bgm()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }
    public void Start()
    {
        _audioSource01.Play();
    }

    //Bgm 변경 함수
    public void ChangeBgm(int number)
    {
        if(number == 1)
        {
            _audioSource01.Stop();

            if (!_audioSource02.isPlaying)
                _audioSource02.Play();
        }
        if (number == 2)
        {
            _audioSource02.Stop();

            if (!_audioSource03.isPlaying)
                _audioSource03.Play();
        }           
    }

    public void VolumnDown(int number)
    {
        if (number == 1)
        {
            if (_audioSource01.volume >= 0)
                _audioSource01.volume -= 0.01f;

            if (_audioSource01.volume < 0)
                _audioSource01.volume = 0.0f;
        }

        if (number == 2)
        {
            if (_audioSource02.volume >= 0)
                _audioSource02.volume -= 0.01f;

            if (_audioSource02.volume < 0)
                _audioSource02.volume = 0.0f;
        } 
        
        if (number == 3)
        {
            if (_audioSource03.volume >= 0)
                _audioSource03.volume -= 0.01f;

            if (_audioSource03.volume < 0)
                _audioSource03.volume = 0.0f;
        }
    }
}
