using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public void TriggerLightScreenShake()
    {
        StartCoroutine(LightScreenShake());
    }

    public void TriggerHeavyScreenShake()
    {
        StartCoroutine(HeavyScreenShake());
    }

    IEnumerator LightScreenShake()
    {
        Vector3 camPos = Camera.main.transform.position;
        for (int i = 0; i < 3; i++)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Random.insideUnitCircle.x * 0.05f, Camera.main.transform.position.y + Random.insideUnitCircle.y * 0.05f, camPos.z);
            yield return new WaitForSeconds(0.01f);
            Camera.main.transform.position = camPos;
        }
    }

    IEnumerator HeavyScreenShake()
    {
        Vector3 camPos = Camera.main.transform.position;
        for (int i = 0; i < 5; i++)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Random.insideUnitCircle.x * 0.15f, Camera.main.transform.position.y + Random.insideUnitCircle.y * 0.15f, camPos.z);
            yield return new WaitForSeconds(0.02f);
            Camera.main.transform.position = camPos;
        }
    }

}
