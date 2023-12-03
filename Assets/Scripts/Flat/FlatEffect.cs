using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlatEffect : MonoBehaviour
{
    ParticleSystem particle;
    ParticleSystem.MainModule mainModule;
    ParticleSystem.Particle[] particles;

    Vector3 endPos;
    Vector3[] controlPos1;
    Vector3 controlPos2;

    Light2D endPosLight;

    float duration = 0.5f;
    float timer = 0f;

    bool IsTransparent = false;
    Color transparent;
    private void Awake()
    {
        particle= GetComponent<ParticleSystem>();
        endPosLight = GetComponentInChildren<Light2D>();
        endPosLight.intensity = 0;
        endPosLight.gameObject.SetActive(false);
        mainModule = particle.main;
        transparent = new Color(0, 0, 0, 0);
        particles = new ParticleSystem.Particle[mainModule.maxParticles];
        controlPos1 = new Vector3[mainModule.maxParticles];
    }
    public void ColorChange(Color _color)
    {
        mainModule.startColor = _color;
    }
    public void SetEndpos(Vector3 _endpos)
    {
        endPos = _endpos;
        endPosLight.transform.position = endPos;
    }
    public void PlayEffect()
    {
        IsTransparent = false;
        particle.Play();
        timer = 0f;
        SetRandomPos();
    }
    void SetRandomPos()
    {
        float deg = Random.Range(0f, 360f);
        controlPos2 = new Vector3(endPos.x + (10 * Mathf.Sin(deg)), endPos.y + (10 * Mathf.Cos(deg)), 0);
        for (int i = 0; i < particles.Length; i++)
        {
            controlPos1[i] = particles[i].position + particles[i].velocity*0.5f;
        }
    }
    private void LateUpdate()
    {
        if (!IsTransparent)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                IsTransparent = true;
                endPosLight.gameObject.SetActive(true);
                ColorChange(transparent);
            }
            particle.GetParticles(particles);
            float t = timer / duration;
            if (t > 0.2f)
            {
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].position = CalculateBezierPoint(t, transform.position, controlPos1[i], controlPos2, endPos);
                }

                particle.SetParticles(particles, particles.Length);
            }
        }
    }
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3f * uu * t * p1;
        p += 3f * u * tt * p2;
        p += ttt * p3;
        return p;
    }
}
