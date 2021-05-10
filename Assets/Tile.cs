using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EZCameraShake;

public class Tile : MonoBehaviour
{
    public Material tileMaterial;
    private MeshRenderer meshRenderer;
    public ParticleSystem fx;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildFinished()
    {
        if (tileMaterial)
        {
            meshRenderer.material = tileMaterial;
        }
        fx.transform.position = transform.position;
        fx.Play();
        CameraShaker.Instance.ShakeOnce(2f, 1.5f, .1f, .15f);
    }
}
