using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.Security;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class ActivateTurret : MonoBehaviour
{
    public PlayerMovement playermovement;
    public BoatControll boatcontroll;
    [SerializeField] private bool colliding = false;
    [SerializeField] private bool active = false;
    public cameraScript CameraScript;
    public GameObject interactDisplay;

    private void Start()
    {
        interactDisplay.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliding = false;
        }
    }

    private void Update()
    {
        if (colliding)
            interactDisplay.SetActive(true);
        else
            interactDisplay.SetActive(false);


        if (Input.GetKeyDown(KeyCode.E) && colliding && !active)
        {
            active = true;
            activate();
        }
        else if (Input.GetKeyDown(KeyCode.E) && colliding && active)
        {
            active = false;
            deactivate();
        }
    }

    void activate()
    {
        playermovement.CanMove = false;
        boatcontroll.isActive = true;
        playermovement.moveDirection = Vector2.zero;

        CameraScript.ChangeToTurretPOV();

        StartCoroutine(FlashDisplay());
    }

    void deactivate()
    {
        playermovement.CanMove = true;
        boatcontroll.isActive = false;

        CameraScript.ChangeAwayFromTurretPov();

        StartCoroutine(FlashDisplay());
    }

    IEnumerator FlashDisplay()
    {
        float duration = 0.5f;
        float timer = 0;

        Vector3 originalScale = interactDisplay.transform.localScale;
        Vector3 targetScale = new Vector3(2.5f, 2.5f, 2.5f);

        while (timer < duration)
        {
            interactDisplay.transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / duration);
            timer += Time.fixedDeltaTime;
            yield return null;
        }

        interactDisplay.transform.localScale = targetScale;

        timer = 0;

        while (timer < duration)
        {
            interactDisplay.transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / duration);
            timer += Time.fixedDeltaTime;
            yield return null;
        }

        interactDisplay.transform.localScale = originalScale;
    }
}