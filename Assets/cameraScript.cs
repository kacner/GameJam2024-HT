using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private Transform MainPOV;
    [SerializeField] private Transform ForestPOV;
    [SerializeField] private bool isMainPov = true;
    public PlayerMovement playerMovement;
    public float followSpeed = 5f;
    void Start()
    {
        transform.position = MainPOV.position;
    }
    public void Switch()
    {
        if (!isMainPov)
        {
            StopAllCoroutines();
            StartCoroutine(SwitchTo(MainPOV));
            isMainPov = true;
        }
        else if (isMainPov)
        {
            StopAllCoroutines();
            StartCoroutine(SwitchTo(ForestPOV));
            isMainPov = false;
        }
    }

    IEnumerator SwitchTo(Transform MoveToTransform)
    {
        float duration = 10f;
        float time = 0f;
        
        while (time < duration)
        {
            transform.position = Vector3.Lerp(transform.position, MoveToTransform.position, time / duration);
            time += Time.fixedDeltaTime;
            print(time);
            yield return new WaitForEndOfFrame();
        }

        transform.position = MoveToTransform.position;
    }

    private void Update()
    {
        if (playerMovement.ShouldCameraFollow)
        {
            //transform.position = new Vector3(transform.position.x, playerMovement.transform.position.y, -10);
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, playerMovement.transform.position.y, followSpeed * Time.deltaTime), -10);
        }
    }
}
