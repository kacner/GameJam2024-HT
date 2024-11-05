using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ForestTile : MonoBehaviour
{
    [SerializeField] private int Health = 3;
    [SerializeField] private GameObject droppedItem; 

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
       GameObject DroppedItem = Instantiate(droppedItem, transform.position, Quaternion.identity);
        //DroppedItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Destroy(this.gameObject);
        
    }
}
