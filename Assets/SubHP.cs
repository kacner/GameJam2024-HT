using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubHP : MonoBehaviour
{
    public Image Slider;
    [SerializeField] private float HP = 20;
    [SerializeField] private float CurrentHP = 5;
    // Update is called once per frame

    private void Start()
    {
        updateHealthBar();
    }

    public void TakeDamage(float Damage)
    {
        if ((CurrentHP - Damage) <= 0)
        {
            die();
        }
        else
        {
            CurrentHP--;
        }


        Slider.fillAmount = CurrentHP / HP;

        if (Slider.fillAmount == 0.05f)
        {
            Slider.fillAmount = 0;
            die();
        }
    }

    void die()
    {
        print("SUB DIDES");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            updateHealthBar();
        }
    }

    void updateHealthBar()
    {
        TakeDamage(0);
    }
}
