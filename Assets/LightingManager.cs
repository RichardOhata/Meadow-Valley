using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField] Material[] SkyBoxes;

    private void Start()
    {
        
    }
    private void Update()
    {
        ChangeSkyBox();
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24 / 10;
            UpdateLighting(TimeOfDay / 24f);
        } else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }
    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);    

        if (DirectionalLight != null) { 
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        { 
            DirectionalLight = RenderSettings.sun;
        } else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    private void ChangeSkyBox()
    {
       if (TimeOfDay >= 6 && TimeOfDay <= 8)
        {
            RenderSettings.skybox = SkyBoxes[0]; 
        }
       else if (TimeOfDay > 8 && TimeOfDay <= 19)
        {
            RenderSettings.skybox = SkyBoxes[1];
        }
       else if (TimeOfDay > 19 && TimeOfDay <= 21)
        {
            RenderSettings.skybox = SkyBoxes[2];
        }
       else if (TimeOfDay > 21 || TimeOfDay < 6)
        {
            RenderSettings.skybox = SkyBoxes[3];
        }
    }
}