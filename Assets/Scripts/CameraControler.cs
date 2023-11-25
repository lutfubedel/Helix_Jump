using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Transform ball;
    Vector3 offset;
    public float smoothSpeed;

    bool shake_control;


    private void Start()
    {
        offset = transform.position - ball.position;
    }

    private void Update()
    {
        Vector3 newPos = Vector3.Lerp(transform.position,offset+ball.position,smoothSpeed);
        transform.position = newPos;
    }


    public void CameraShakeFonks()
    {
        if (shake_control == false)
        {
            StartCoroutine(Shake(0.22f, 0.4f));
            shake_control = true;
        }
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed<duration)
        {
            float x = Random.Range(-2f, 2f) * magnitude;
            float y = Random.Range(-2f, 2f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
    
}
