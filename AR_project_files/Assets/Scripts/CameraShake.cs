using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 cameraPos;
    float shakeDuration;
    public float shakeIntensity;

    void Start()
    {
        cameraPos = transform.position;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition += Random.insideUnitSphere * shakeIntensity;
            StartCoroutine("StopShaking");
        }


    }

    public void ShakeTrigger()
    {
        shakeDuration = 1;
    }

    private IEnumerator StopShaking()
    {
        yield return new WaitForSeconds(0.3f);
        shakeDuration = 0;
        transform.position = cameraPos;
    }
}
