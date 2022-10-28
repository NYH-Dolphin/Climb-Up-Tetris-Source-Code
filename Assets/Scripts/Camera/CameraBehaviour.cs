using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraBehaviour : MonoBehaviour
{


    public static CameraBehaviour Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void OnShakeCamera(float duration, float magnitudeX, float magnitudeY)
    {
        StartCoroutine(Shake(duration, magnitudeX, magnitudeY));
    }

    private IEnumerator Shake(float duration, float magnitudeX, float magnitudeY)
    {
        Vector3 originPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitudeX;
            float y = Random.Range(-1f, 1f) * magnitudeY;

            transform.position = new Vector3(x, y, -10);
            elapsed += Time.deltaTime;
            yield return 0;
        }

        transform.position = originPosition;
    }
}