using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private bool colliding1 = false;
    public SpriteRenderer spriterenderer;
    [SerializeField] private Sprite DoorOpen;
    private Sprite DoorClouse;

    private void Start()
    {
        DoorClouse = spriterenderer.sprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliding1 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliding1 = false;
        }
    }
    private void Update()
    {
        if (colliding1)
        {
            spriterenderer.sprite = DoorOpen;
        }
        else
        {
            spriterenderer.sprite = DoorClouse;
        }
    }
}
