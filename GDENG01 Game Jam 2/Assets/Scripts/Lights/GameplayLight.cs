using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLight : MonoBehaviour
{
    [SerializeField] Light tableLight;
    private LightData currentLight;
    private List<LightData> customers = new List<LightData>();
    private LightData tableLightData = new LightData();
    private System.Random r;

    public float fadeDuration = 5f;

    private void Awake()
    {
        r = new System.Random();
        EventBroadcaster.Instance.AddObserver("Customer", this.StartGameLights);
        EventBroadcaster.Instance.AddObserver("ResetLight", this.ResetLight);
    }

    private void Start()
    {

        tableLightData.light = tableLight;
        tableLightData.maxIntensity = tableLight.intensity;
        tableLight.intensity = 0f;

        foreach (Transform child in transform)
        {
            Light light = child.GetComponent<Light>();
            if (light != null)
            {
                LightData lightData = new LightData();
                lightData.light = light;
                lightData.maxIntensity = light.intensity;
                customers.Add(lightData);

                light.intensity = 0f;
            }
        }
    }

    void StartGameLights()
    {

        if (customers.Count == 0) return;

        int customerIndex = r.Next(0, customers.Count);
        currentLight = customers[customerIndex];
        StartCoroutine(FadeIn(currentLight));
    }

    void ResetLight()
    {
        currentLight.light.intensity = 0f;
        tableLight.intensity = 0f;
    }

    IEnumerator FadeIn(LightData data)
    {
        float timer = 0f;
        data.light.intensity = 0f;

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            data.light.intensity = Mathf.Lerp(0f, data.maxIntensity, t);
            tableLight.intensity = Mathf.Lerp(0f, data.maxIntensity, t);
            timer += Time.deltaTime;
            yield return null;
        }

        data.light.intensity = data.maxIntensity;
        tableLight.intensity = tableLightData.maxIntensity;

        EventBroadcaster.Instance.PostEvent("dialouge"); //goes to dialogue and gameplay        
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("Customer");
    }
}