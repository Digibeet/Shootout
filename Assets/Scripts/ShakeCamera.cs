using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public IEnumerator Shake(float time)
    {
        float elapsed = 0.0f;

        Vector3 originalCamPos = this.transform.position;

        this.transform.position = new Vector3(originalCamPos.x + 0.2f, originalCamPos.y + 0.2f, originalCamPos.z);
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;

            
            float percentComplete = elapsed / time;

            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= 0.2f * damper;
            y *= 0.2f * damper;

            this.transform.position = new Vector3(x, y, originalCamPos.z);
            
            yield return null;
        }

        this.transform.position = originalCamPos;
    }
}
