using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
    public float minIntensity = 0.25f;
    public float maxIntensity = 0.5f;

    float random;

    void Start()
    {

    }

    void Update()
    {
        GetComponent<Light>().intensity = Random.Range(6.0f, 8.0f);
    }
}
