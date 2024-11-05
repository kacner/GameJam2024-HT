using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTile : MonoBehaviour
{
    [SerializeField] private int Health = 3;

    public void TakeDMG(int damange)
    {
        if ((Health - damange) <= 0)
        {
            die();
        }
        else
        {
            Health -= damange;
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
