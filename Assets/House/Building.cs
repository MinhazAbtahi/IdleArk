using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Building : MonoBehaviour
{
    public GameObject[] buildings;
    public int resourceCost;
    private int buildingIndex;
    private int buildingCount;
    private int lostResource;
    public TextMeshProUGUI resourceText;
    public ParticleSystem buildingFx;
    public ParticleSystem buildCompleteFx;

    // Start is called before the first frame update
    void Start()
    {
        resourceText.text = resourceCost.ToString();
        buildingCount = buildings.Length;
        lostResource = resourceCost / buildingCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Build()
    {
        StartCoroutine(BuildRoutine());
    }

    private IEnumerator BuildRoutine()
    {
        for (int i = 0; i < buildingCount; i++)
        {
            yield return new WaitForSeconds(1f);
            resourceCost -= lostResource;
            resourceText.text = resourceCost.ToString();
            buildings[i].SetActive(true);
            SoundManager.Instance.PlayBuildingSFX();
            buildingFx.Play();
            if (i == buildingCount - 1)
            {
                EZCameraShake.CameraShaker.Instance.ShakeOnce(1.5f, 1.5f, .1f, .15f);
                SoundManager.Instance.PlayBuildingCompleteSFX();
                buildCompleteFx.Play();
                resourceText.rectTransform.parent.DOScale(Vector3.zero, .25f);
            }
        }
    }
}
