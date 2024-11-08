using System.Collections;
using UnityEngine;

public class pickerUpper : MonoBehaviour
{
    public inventory Playerinventory;
    public Color color;
    public ParticleSystem[] partisstclesys;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pickup")
        {
            Playerinventory.pearlsAmount++;
            StartCoroutine(Thing(collision.gameObject));
            collision.GetComponent<Pickup>().Dettach();
            collision.GetComponent<SpriteRenderer>().sortingOrder = 12;
        }
    }

    IEnumerator Thing(GameObject item)
    {
        item.GetComponent<CircleCollider2D>().enabled = false;
        float time = 0;
        float duration = 2f;
        SpriteRenderer sprieenderer = item.GetComponent<SpriteRenderer>();

        while (time < duration)
        {
            sprieenderer.color = Color.Lerp(item.GetComponent<SpriteRenderer>().color, color, time / duration);

            item.transform.position = Vector2.Lerp(item.transform.position, transform.position, time / duration);

            item.transform.localScale = Vector3.Lerp(item.transform.localScale, new Vector3(0.3f, 0.3f, 0.3f), time / duration);

            time += Time.fixedDeltaTime;
            yield return null;
        }

        item.transform.position = transform.position;
        item.GetComponent<SpriteRenderer>().color = color;
        Destroy(item.gameObject);

        StartParticle();
    }

    void StartParticle()
    {
        foreach (ParticleSystem item in partisstclesys)
        {
            item.Play();
        }
    }
}
