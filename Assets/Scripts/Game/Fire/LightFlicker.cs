using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
    [SerializeField]
    private float minIntensity = 6f;
    private float maxIntensity = 8f;

    private Light lightSource;

    private void Start()
    {
        lightSource = GetComponent<Light>();
    }

    void Update()
    {
        lightSource.intensity = Random.Range(minIntensity, maxIntensity);
    }
}
