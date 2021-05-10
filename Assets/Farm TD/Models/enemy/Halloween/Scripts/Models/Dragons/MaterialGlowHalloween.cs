using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGlowHalloween : MonoBehaviour
{
    private float lightDelay2375 = 0.15f;
    ////private float lightDelay3907 = 0.15f;
    private float lightDelay3905 = 0.15f;
    private float lightDelay3802 = 0.5f;

    public GameObject lightMaterial;

    // Start is called before the first frame update
    void Start()
    {
        string name = gameObject.name;
        if (name.StartsWith("2375"))
        {
            StartCoroutine(Glow2375());
        }
        //if (name.StartsWith("3907"))
        //{
        //    StartCoroutine(Glow3907());
        //}
        if (name.StartsWith("3905"))
        {
            StartCoroutine(Glow3905());
        }
        if (name.StartsWith("3802"))
        {
            StartCoroutine(Glow3802());
        }       
    }

    IEnumerator Glow2375()
    {
        while (true)
        {
            for (float i = 0f; i <= 8f; i++)
            {
                yield return new WaitForSeconds(lightDelay2375);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.0720752f, 0.0720752f, 0f) * i);
            }
            for (float i = 8f; i >= 0f; i--)
            {
                yield return new WaitForSeconds(lightDelay2375);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.0720752f, 0.0720752f, 0f) * i);
            }
        }
    }

    //IEnumerator Glow3907()
    //{
    //    while (true)
    //    {
    //        for (float i = 0f; i <= 6f; i++)
    //        {
    //            yield return new WaitForSeconds(lightDelay3907);
    //            lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0f, 0.2641509f, 0f) * i);
    //        }
    //        for (float i = 6f; i >= 0f; i--)
    //        {
    //            yield return new WaitForSeconds(lightDelay3907);
    //            lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0f, 0.2641509f, 0f) * i);
    //        }
    //    }
    //}

    IEnumerator Glow3905()
    {
        while (true)
        {
            for (float i = -4f; i <= 10f; i++)
            {
                yield return new WaitForSeconds(lightDelay3905);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.08193418f, 0.08193418f, 0.08193418f) * i);
            }
            for (float i = 10f; i >= -4f; i--)
            {
                yield return new WaitForSeconds(lightDelay3905);
                lightMaterial.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0.08193418f, 0.08193418f, 0.08193418f) * i);
            }
        }
    }

    IEnumerator Glow3802()
    {
        while (true)
        {
            lightMaterial.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            yield return new WaitForSeconds(lightDelay3802);
            lightMaterial.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(lightDelay3802);
        }
    }    
    // test 
}




