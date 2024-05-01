using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectManager : MonoBehaviour
{
    public GameObject[] effects;
    private VisualEffect[] vfxs;
    private int currentEffect = 0;
    private float time = 0;

    private void Start()
    {
        vfxs = new VisualEffect[effects.Length];
        for (int i = 0; i < effects.Length; ++i)
        {
            effects[i].SetActive(false);
            vfxs[i] = effects[i].GetComponent<VisualEffect>();
        }

        effects[currentEffect].SetActive(true);
    }

    private void FixedUpdate()
    {
        if (time > 180)
        {
            if (currentEffect + 1 < effects.Length)
            {
                vfxs[currentEffect].Stop();
                effects[currentEffect].SetActive(false);
                ++currentEffect;
                effects[currentEffect].SetActive(true);
                vfxs[currentEffect].Play();
                time = 0;
            }
        }
        else 
        {
            time++;
            Debug.Log(time);
        }

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    if (currentEffect + 1 < effects.Length)
        //    {
        //        vfxs[currentEffect].Stop();
        //        effects[currentEffect].SetActive(false);
        //        ++currentEffect;
        //        effects[currentEffect].SetActive(true);
        //        vfxs[currentEffect].Play();
        //    }
        //}
    }
}
