using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tree : MonoBehaviour, IFarmable
{
    public bool IsFarmable { get; set; }
    public int health;
    public int resource;
    private int lostResource;
    private GameObject bigTreePart;
    private GameObject smallTreePart;
    public GameObject[] trunks; // 0 - length => Gameobjects upper to lower
    private int trunkIndex;
    private ParticleSystem hitFx;

    // Start is called before the first frame update
    void Start()
    {
        IsFarmable = true;
        bigTreePart = transform.GetChild(0).gameObject;
        smallTreePart = transform.GetChild(1).gameObject;
        hitFx = GameObject.FindGameObjectWithTag("TreeHitFx").GetComponent<ParticleSystem>();
        lostResource = resource / health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Farm()
    {
        if (IsFarmable)
        {
            if (hitFx)
            {
                hitFx.transform.position = new Vector3(transform.position.x, 1.65f, transform.position.z);
                hitFx.Play();
            }

            if (trunkIndex == health - 1)
            {
                IsFarmable = false;

                transform.DOLocalMoveY(transform.localPosition.y - 5f, .05f);
                bigTreePart.transform.DOScale(Vector3.zero, .1f).OnComplete(()=> 
                {
                    
                    smallTreePart.transform.parent = null;
                    smallTreePart.transform.DOMoveY(1.3f, .1f);
                    Destroy(this.gameObject, .25f);
                });
                return 0;
            }

            trunks[trunkIndex].gameObject.SetActive(false);
            transform.DOLocalMoveY(transform.localPosition.y - 1.5f, .15f).SetEase(Ease.InBack);
            ++trunkIndex;
            resource = resource - lostResource;
            return lostResource;
        }

        return 0;
    }
}
