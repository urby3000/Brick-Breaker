using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

using UnityEngine.UI;
public class RadioControll : MonoBehaviour
{


    public AudioMixerSnapshot radiolossantos;
    public Texture rls;
    public AudioMixerSnapshot radiox;
    public Texture rx;
    public AudioMixerSnapshot bounce;
    public Texture rb;
    public AudioMixerSnapshot csr;
    public Texture rcsr;
    public AudioMixerSnapshot kjahwest;
    public Texture rkjw;
    public AudioMixerSnapshot krose;
    public Texture rkr;
    public AudioMixerSnapshot mastersounds;
    public Texture rms;
    public AudioMixerSnapshot playbackfm;
    public Texture rpbfm;
    public AudioMixerSnapshot sfur;
    public Texture rsfur;
    public AudioMixerSnapshot kdst;
    public Texture rkdst;
    public RawImage logo;

    float transitionTime;

    // Use this for initialization
    void Start()
    {
        logo.texture = rls;
        transitionTime = 60 / 128 * 32;
        foreach (AudioSource a in GameObject.Find("MusicPlayer").GetComponents<AudioSource>())
        {
            if (PlayerPrefs.GetFloat("RadioTime") != 0) {
                a.time = PlayerPrefs.GetFloat("RadioTime");
            }
            
        }

        int SelRadio = PlayerPrefs.GetInt("SelectedRadio");
        if (SelRadio == 1)// radio los santos
        {
            logo.texture = rls;
            radiolossantos.TransitionTo(0);
        }
        else if (SelRadio == 2)// radio x
        {
            logo.texture = rx;
            radiox.TransitionTo(0);
        }
        else if (SelRadio == 3)// bounce
        {
            logo.texture = rb;
            bounce.TransitionTo(0);
        }
        else if (SelRadio == 4)// csr
        {
            logo.texture = rcsr;
            csr.TransitionTo(0);
        }
        else if (SelRadio == 5)// kjahwest
        {
            logo.texture = rkjw;
            kjahwest.TransitionTo(0);
        }
        else if (SelRadio == 6)// krose
        {
            logo.texture = rkr;
            krose.TransitionTo(0);
        }
        else if (SelRadio == 7)// mastersounds
        {
            logo.texture = rms;
            mastersounds.TransitionTo(0);
        }
        else if (SelRadio == 8)// playbackfm
        {
            logo.texture = rpbfm;
            playbackfm.TransitionTo(0);
        }
        else if (SelRadio == 9)// sfur
        {
            logo.texture = rsfur;
            sfur.TransitionTo(0);
        }
        else if (SelRadio == 0)// kdst
        {
            logo.texture = rkdst;
            kdst.TransitionTo(0);
        }
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            logo.enabled = false;
            foreach (AudioSource a in GameObject.Find("MusicPlayer").GetComponents<AudioSource>())
            {
                a.mute = true;
            }
        }
        else
        {

        }
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            AudioSource a = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
            PlayerPrefs.SetFloat("RadioTime", a.time);
            logo.enabled = true;
            if (Input.GetKeyDown("1"))// radio los santos
            {
                logo.texture = rls;
                radiolossantos.TransitionTo(transitionTime);
                PlayerPrefs.SetInt("SelectedRadio", 1);


            }
            else if (Input.GetKeyDown("2"))// radio x
            {
                logo.texture = rx;
                radiox.TransitionTo(transitionTime);
                PlayerPrefs.SetInt("SelectedRadio", 2);
            }
            else if (Input.GetKeyDown("3"))// bounce
            {
                logo.texture = rb;
                bounce.TransitionTo(transitionTime);

                PlayerPrefs.SetInt("SelectedRadio", 3);
            }
            else if (Input.GetKeyDown("4"))// csr
            {
                logo.texture = rcsr;
                csr.TransitionTo(transitionTime);
                PlayerPrefs.SetInt("SelectedRadio", 4);
            }
            else if (Input.GetKeyDown("5"))// kjahwest
            {
                logo.texture = rkjw;
                kjahwest.TransitionTo(transitionTime);
                PlayerPrefs.SetInt("SelectedRadio", 5);
            }
            else if (Input.GetKeyDown("6"))// krose
            {
                logo.texture = rkr;
                krose.TransitionTo(transitionTime);
                PlayerPrefs.SetInt("SelectedRadio", 6);
            }
            else if (Input.GetKeyDown("7"))// mastersounds
            {
                logo.texture = rms;
                mastersounds.TransitionTo(transitionTime);
                PlayerPrefs.SetInt("SelectedRadio", 7);
            }
            else if (Input.GetKeyDown("8"))// playbackfm
            {
                logo.texture = rpbfm;
                playbackfm.TransitionTo(transitionTime);
                PlayerPrefs.SetInt("SelectedRadio", 8);
            }
            else if (Input.GetKeyDown("9"))// sfur
            {
                logo.texture = rsfur;
                sfur.TransitionTo(transitionTime);
                PlayerPrefs.SetInt("SelectedRadio", 9);
            }
            else if (Input.GetKeyDown("0"))// kdst
            {
                logo.texture = rkdst;
                kdst.TransitionTo(transitionTime);
                PlayerPrefs.SetInt("SelectedRadio", 0);
            }
        }
        else
        {
            logo.enabled = false;
        }
    }



}