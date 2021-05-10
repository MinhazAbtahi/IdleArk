using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGlow : MonoBehaviour
{
    private float lightDelay2357 = 0.15f;
    private float lightDelay12319 = 0.05f;
    private float lightDelay2826 = 0.15f;
    private float lightDelay12826 = 0.15f;
    private float lightDelay12987 = 0.15f;
    private float lightDelay12990 = 0.15f;
    private float lightDelay12317 = 0.15f;
    private float lightDelay11 = 0.15f;

    public GameObject lightMaterial;

    // Start is called before the first frame update
    void Start()
    {
        string name = gameObject.name;
        if (name.StartsWith("2357"))
        {
            StartCoroutine(Glow2357());
        }
        if (name.StartsWith("12319"))
        {
            StartCoroutine(Glow12319());
        }
        if (name.StartsWith("2826"))
        {
            StartCoroutine(Glow2826());
        }
        if (name.StartsWith("12826"))
        {
            StartCoroutine(Glow12826());
        }
        if (name.StartsWith("12987"))
        {
            StartCoroutine(Glow12987());
        }
        if (name.StartsWith("12990"))
        {
            StartCoroutine(Glow12990());
        }
        if (name.StartsWith("12317"))
        {
            StartCoroutine(Glow12317());
        }
        if (name.StartsWith("11"))
        {
            StartCoroutine(Glow11());
        }
    }

    IEnumerator Glow2357()
    {
        while (true)
        {
            //lightMaterial.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            for (float i = 0f; i <= 8f; i++)
            {
                yield return new WaitForSeconds(lightDelay2357);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0f, 0.1277957f, 0.1857496f) * i);
            }
            for (float i = 8f; i >= 0f; i--)
            {
                yield return new WaitForSeconds(lightDelay2357);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0f, 0.1277957f, 0.1857496f) * i);
            }
        }
    }
    
    IEnumerator Glow12319()
    {
        while (true)
        {
            for (float i = 0f; i <= 1f; i++)
            {
                yield return new WaitForSeconds(lightDelay12319);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(1f, 1f, 1f) * i);
            }
            for (float i = 1f; i >= 0f; i--)
            {
                yield return new WaitForSeconds(lightDelay12319);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(1f, 1f, 1f) * i);
            }
        }
    }

    IEnumerator Glow2826()
    {
        while (true)
        {
            for (float i = 0f; i <= 1f; i++)
            {
                yield return new WaitForSeconds(lightDelay2826);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(1f, 1f, 1f) * i);
            }
            for (float i = 1f; i >= 0f; i--)
            {
                yield return new WaitForSeconds(lightDelay2826);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(1f, 1f, 1f) * i);
            }
        }
    }


    IEnumerator Glow12826()
    {
        while (true)
        {
            for (float i = 0f; i <= 6f; i++)
            {
                yield return new WaitForSeconds(lightDelay12826);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.015625f, 0.015625f, 0.015625f) * i);
            }
            for (float i = 6f; i >= 0f; i--)
            {
                yield return new WaitForSeconds(lightDelay12826);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.015625f, 0.015625f, 0.015625f) * i);
            }
        }
    }

    IEnumerator Glow12987()
    {
        while (true)
        {
            for (float i = 0f; i <= 3f; i++)
            {
                yield return new WaitForSeconds(lightDelay12987);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.09362745f, 0.09362745f, 0.09362745f) * i);
            }
            for (float i = 3f; i >= 0f; i--)
            {
                yield return new WaitForSeconds(lightDelay12987);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.09362745f, 0.09362745f, 0.09362745f) * i);
            }
        }
    }

    IEnumerator Glow12990()
    {
        while (true)
        {
            for (float i = 0f; i <= 4f; i++)
            {
                yield return new WaitForSeconds(lightDelay12990);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.0627451f, 0.0627451f, 0.0627451f) * i);
            }
            for (float i = 4f; i >= 0f; i--)
            {
                yield return new WaitForSeconds(lightDelay12990);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.0627451f, 0.0627451f, 0.0627451f) * i);
            }
        }
    }

    IEnumerator Glow12317()
    {
        while (true)
        {
            for (float i = 0f; i <= 2f; i++)
            {
                yield return new WaitForSeconds(lightDelay12317);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.1872549f, 0.1872549f, 0.1872549f) * i);
            }
            for (float i = 2f; i >= 0f; i--)
            {
                yield return new WaitForSeconds(lightDelay12317);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.1872549f, 0.1872549f, 0.1872549f) * i);
            }
        }
    }

    IEnumerator Glow11()
    {
        while (true)
        {
            for (float i = 0f; i <= 4f; i++)
            {
                yield return new WaitForSeconds(lightDelay11);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(1f, 0.1519028f, 0f) * i);
            }
            for (float i = 4f; i >= 0f; i--)
            {
                yield return new WaitForSeconds(lightDelay11);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(1f, 0.1519028f, 0f) * i);
            }
        }
    }
}




