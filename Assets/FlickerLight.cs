using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{

	public float variation = 0;

	private Light _light;

	private float baseLine;
	// Use this for initialization
	void Start ()
	{
		_light = GetComponent<Light>();
		baseLine = _light.intensity;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float targetIntensity = Random.Range(baseLine - variation, baseLine + variation);
		_light.intensity = Mathf.Lerp(_light.intensity, targetIntensity, Time.deltaTime);
	}
}
