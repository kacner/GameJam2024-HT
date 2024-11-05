using System.Collections;
using UnityEngine;

public class pickerUpper : MonoBehaviour
{
    public inventory Playerinventory;
    public Color color;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pickup")
        {
            Playerinventory.pearlsAmount++;
            StartCoroutine(Thing(collision.gameObject));
            collision.GetComponent<Pickup>().Dettach();
        }
    }

    IEnumerator Thing(GameObject item)
    {
        item.GetComponent<BoxCollider2D>().enabled = false;
        float time = 0;
        float duration = 2f;

        while (time < duration)
        {
            item.transform.position = Vector2.Lerp(item.transform.position, transform.position, time / duration);

            time += Time.fixedDeltaTime;
            yield return null;
        }

        item.transform.position = transform.position;
        StartCoroutine(FadeToblack(item));
    }
    IEnumerator FadeToblack(GameObject item)
    {
        float time = 0f;
        float duration = 0.5f;

        SpriteRenderer sprieenderer = item.GetComponent<SpriteRenderer>();

        while (time < duration)
        {
            sprieenderer.color = Color.Lerp(item.GetComponent<SpriteRenderer>().color, color, time / duration);


            time += Time.fixedDeltaTime;
            yield return null;
        }
        
        item.GetComponent<SpriteRenderer>().color = color;
        
        //Destroy(item.gameObject);
    }
}
