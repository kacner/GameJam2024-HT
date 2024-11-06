using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowTrigger : MonoBehaviour
{
    public ShadowCaster2D shadowcaster;
    [SerializeField] private bool Colliding = false;
    [SerializeField] private GameObject Particlessystemets;

    private void Update()
    {
        if (Colliding)
        {
            Particlessystemets.SetActive(false);
        }
        else
        {
            Particlessystemets.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shadowcaster.enabled = false;
        Colliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shadowcaster.enabled = true;
        Colliding = false;
    }

    public void forceDisable()
    {
        Colliding = true;
        shadowcaster.enabled = false;
    }
}
