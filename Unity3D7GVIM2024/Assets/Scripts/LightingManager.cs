using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;
    [SerializeField, Range(0, 24)] private float timeOfDay;

    // Update is called once per frame
    void Update()
    {
        //timeOfDay += Time.deltaTime;
        //timeOfDay %= 24;
        UpdateLighting(timeOfDay / 24f);
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        directionalLight.color = preset.directionalColor.Evaluate(timePercent);
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(
            timePercent * 360f - 90f, 170f, 0));

    }
}
