using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ScionEngine;
using Sol;

public class AutoIntensity : MonoBehaviour
{
    public ScionPostProcess scion;
	public Gradient nightDayColor;

	public float maxIntensity = 3f;
	public float minIntensity = 0f;
	public float minPoint = -0.2f;

	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbientPoint = -0.2f;


	public Gradient nightDayFogColor;
	public AnimationCurve fogDensityCurve;

    public AnimationCurve maxExposure;
    public AnimationCurve minExposure;

	public float fogScale = 1f;

	public float dayAtmosphereThickness = 0.4f;
	public float nightAtmosphereThickness = 0.87f;

	public Vector3 dayRotateSpeed;
	public Vector3 nightRotateSpeed;
    public bool go = false;

    public bool colorOnly = false;

    public AudioClip morningSong;
    public AudioClip nightSong;

    private bool isDay = true;
    private bool wasDay = false;
	public float elapsedTime;
	public float flashlightActivateTime;
	public float elapsedTimeSpeed;

    public List<ParticleSystem> affectedParticles = new List<ParticleSystem>();

	float skySpeed = 1;

    public float currentTime = 0f;

    public float CurrentTime
    {
        get { return currentTime; }
    }


	Light mainLight;
	Skybox sky;
	Material skyMat;

	void Start () 
	{
	
		mainLight = GetComponent<Light>();
		skyMat = RenderSettings.skybox;

	}

	void Update() {
		if (isDay) {
			if (elapsedTime < flashlightActivateTime) {
				//elapsedTime += Time.deltaTime * skySpeed;
			} else {
				//UIManager.GetMenu<MessageMenu> ().Open ("F to toggle flashlight.", 3, 5f);
				elapsedTime = 0.0f;
			}
		}
	}

	void FixedUpdate () 
	{
	    if(go)
        {
            if(colorOnly)
            {
                float tRange = 1 - minPoint;
                float dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
                dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
                mainLight.color = nightDayColor.Evaluate(dot);
            }
            else
            {
                float tRange = 1 - minPoint;
                float dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
                float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

                mainLight.intensity = i;

                tRange = 1 - minAmbientPoint;
                dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
                i = ((maxAmbient - minAmbient) * dot) + minAmbient;
                RenderSettings.ambientIntensity = i;

                mainLight.color = nightDayColor.Evaluate(dot);
                RenderSettings.ambientLight = mainLight.color;

                Color fogColor = nightDayFogColor.Evaluate(dot);
                RenderSettings.fogColor = fogColor;
                RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

                foreach(ParticleSystem particles in affectedParticles)
                {
                    Color particleColor = new Color(fogColor.r, fogColor.g, fogColor.b, particles.startColor.a);
                    particles.startColor = particleColor;
                }

                // scion.minMaxExposure = new Vector2(minExposure.Evaluate(dot), maxExposure.Evaluate(dot));

                i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
                skyMat.SetFloat("_AtmosphereThickness", i);

                if (dot > 0)
                {
                    isDay = true;
                    if (isDay != wasDay && morningSong != null) GameManager.Get<SoundManager>().Play(morningSong);
                    transform.Rotate(dayRotateSpeed * Time.deltaTime * skySpeed);
                }
                else
                {
                    isDay = false;
                    if (isDay != wasDay)
                    {
                        if(nightSong != null) GameManager.Get<SoundManager>().Play(nightSong);
                    }
                    transform.Rotate(nightRotateSpeed * Time.deltaTime * skySpeed);
                }

                currentTime += Time.deltaTime;
                wasDay = isDay;
            }
        }
	}
}
