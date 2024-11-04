using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private Transform MainPOV;
    [SerializeField] private Transform ForestPOV;
    [SerializeField] private bool isMainPov = true;
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


}
