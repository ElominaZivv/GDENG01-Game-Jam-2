using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LightFlicker : MonoBehaviour
{

    private List<LightData> childLights = new List<LightData>();
    private System.Random r;

    public float minMultiplier = 0.6f;
    public float flickerSpeed = 0.1f;
    public float duration = 3f;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver("ResetLight", this.ResetLight);
        EventBroadcaster.Instance.AddObserver("StartFlicker", this.StartFlicker);
    }
    void Start()
    {
        gameObject.SetActive(true);
        r = new System.Random();

        foreach (Transform child in transform)
        {
            Light light = child.GetComponent<Light>();
            if (light != null)
            {
                LightData lightData = new LightData();
                lightData.light = light;
                lightData.maxIntensity = light.intensity;
                childLights.Add(lightData);
            }
        }
    }

    void StartFlicker()
    {
        StartCoroutine(Flicker());
    }
    IEnumerator Flicker()
    {
        float timer = 0f;

        while (timer < duration)
        {
            foreach (LightData data in childLights)
            {
                if (data.light != null)
                {
                    float min = data.maxIntensity * minMultiplier;
                    float intensity = (float)(r.NextDouble() * (data.maxIntensity - min) + min);
                    data.light.intensity = intensity;
                }
            }

            float waitTime = flickerSpeed;
            yield return new WaitForSeconds(waitTime);
            timer += waitTime;
        }

        gameObject.SetActive(false);
        EventBroadcaster.Instance.PostEvent("Customer");
        EventBroadcaster.Instance.PostEvent("spread");
    }

    void ResetLight()
    {
        gameObject.SetActive(true);
        foreach (LightData data in childLights)
        {
            if (data.light != null)
            {
                data.light.intensity = data.maxIntensity;
            }
        }
        //StartCoroutine(Flicker()); //Get rid once gameplay is added
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("StartFlicker");
        EventBroadcaster.Instance.RemoveObserver("ResetLight");
    }
}