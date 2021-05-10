using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using TMPro;
using EZCameraShake;
                                                                                                  
public class PlayerController : AbstractPlayer
{
    public Animator anim;
    Rigidbody rb;
    public LayerMask clickable;
    public NavMeshAgent myAgent;
    public bool running;
    [Header("Player Stats")]
    [HideInInspector] public string healthKey = "healthkey";
    public int maxHealth;
    public float footstepDelay;

    [Header("Farming")]
    public GameObject[] trees;
    public Tree closestTree;
    private WaitForSeconds farmingDelay;
    public float farmDelay;
    public float farmDelayMax;
    public bool canFarm;
    public bool treeContact;

    [Header("Building")]
    public GameObject[] buildings;
    public Building closestBuilding;
    private WaitForSeconds buildingDelay;
    public float buildDelay;
    public float buildDelayMax;
    public bool canBuild;
    public bool buildingContact;

    [Header("Shooting")]
    public GameObject bullet;
    public float shootDelay;
    public float shootDelayMax;
    public Transform[] firePoints;
    public Transform shellPoint;
    public GameObject bulletShellPrefab;
    public bool canShoot;
    public GameObject[] enemies;
    public GameObject closestEnemy;
    public bool enemyContact;
    public GameObject muzzleFlash;

    [Header("POWER UPS!")]
    [Header("Shield")]
    public GameObject shieldPrefab;
    public GameObject shieldPickup;
    public bool hasShield;
    public GameObject playerShield;
    public int shieldLevel;
    [Header("Triple Shoot")]
    public bool hasTripleShoot;
    public float tripleShootTimer;
    public float tripleShootTimerMax;

    [Header("Missile")]
    public GameObject missile;
    private bool shootMissile;

    [Header("Wave Fx")]
    public Transform waveFx;
    public GameObject waveBlastFx;
    private bool isWaveUsed;
    private float waveScale;
    public float maxWaveScale = 4f;


    public List<GameObject> tiles;
    public GameObject button2;
    public GameObject radialProgress;
    public GameObject tree;
    private bool build;

    public void Build(int i)
    {
        if (i!= 2)
        {
            tiles[i].SetActive(true);
        }
        build = true;
        radialProgress.SetActive(true);
        StartCoroutine(BuildRoutine());
    }

    public void FinishBuild(int i)
    {
        build = false;
        //anim.SetBool("Idle1", true);
        radialProgress.SetActive(false);
        radialProgress.GetComponent<RadialProgress>().done = false;
        if (i == 2)
        {
            tree.GetComponent<Tile>().BuildFinished();
            tree.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
        tiles[i].GetComponent<Tile>().BuildFinished();
    }

    IEnumerator BuildRoutine()
    {
        if (!build)
        {
            yield break;
        }
        while (build)
        {
            yield return new WaitForSeconds(.5f);
            anim.SetTrigger("Attack3");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        myAgent = GetComponent<NavMeshAgent>();

        ///waveScale = 1;
        closestEnemy = null;
        enemyContact = false;
        canShoot = false;

        farmDelay = farmDelayMax;
        farmingDelay = new WaitForSeconds(0.3f);
        ///tripleShootTimer = tripleShootTimerMax;
        GetPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.startGame) return;
        if (isDead) return;

        if (canFarm) AutoFarm();
        if (closestTree == null) StopFarming();

        if (canShoot) Attack();
        if (closestEnemy == null) StopShooting();

        if (Input.GetKeyDown(KeyCode.A))
        {
            button2.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            Build(2);
        }



        /// TO-DO Later
        //if (playerShield != null)
        //    playerShield.transform.Rotate((Vector3.up * 200 * Time.deltaTime));

        //if (hasTripleShoot)
        //{
        //    tripleShootTimer -= Time.deltaTime;
        //    if (tripleShootTimer <= 0)
        //    {
        //        hasTripleShoot = false;
        //        tripleShootTimer = tripleShootTimerMax;
        //    }              
        //}

        //if (shootMissile)
        //{
        //    tripleShootTimer -= Time.deltaTime;
        //    if (tripleShootTimer <= 0)
        //    {
        //        shootMissile = false;
        //        tripleShootTimer = tripleShootTimerMax;
        //    }
        //}
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("enemyProjectile"))
    //    {
    //        TakeDamage(2);

    //        Destroy(other.gameObject);
    //    }

    //    if (other.gameObject.CompareTag("enemySniperProjectile"))
    //    {
    //        TakeDamage(10);

    //        Destroy(other.gameObject);

    //        //Camera.main.transform.DOShakeRotation(0.45f, 1, 10, 90);
    //    }

    //    if (other.gameObject.CompareTag("playerShieldPickup") && !hasShield)
    //    {
    //        hasShield = true;
    //        GameObject shieldIns = Instantiate(shieldPrefab, transform.position, shieldPrefab.transform.rotation);
    //        shieldIns.transform.parent = transform;
    //        playerShield = shieldIns;
    //        Destroy(other.gameObject);
    //        SoundManager.Instance.PlaySFX(SoundManager.Instance.tripleFirePickSFX);
    //    }

    //    if (other.gameObject.CompareTag("TripleShootPickup") && !hasTripleShoot)
    //    {
    //        hasTripleShoot = true;
    //        Destroy(other.gameObject);
    //        SoundManager.Instance.PlaySFX(SoundManager.Instance.tripleFirePickSFX);
    //    }

    //    if (other.gameObject.CompareTag("playerHealthPickup"))
    //    {
    //        if(health < maxHealth - 30)
    //        {
    //            health += 30;
    //            PlayerPrefs.SetInt(healthKey, health);
    //            UIManager.instance.healthBar.fillAmount = (float)health / (float)maxHealth;
    //            Destroy(other.gameObject);
    //            SoundManager.Instance.PlaySFX(SoundManager.Instance.healthPickSFX);
    //        }
    //        else
    //        {
    //            health = maxHealth;
    //            PlayerPrefs.SetInt(healthKey, health);
    //            UIManager.instance.healthBar.fillAmount = (float)health / (float)maxHealth;
    //            Destroy(other.gameObject);
    //            SoundManager.Instance.PlaySFX(SoundManager.Instance.healthPickSFX);
    //        }
    //    }

    //    if (other.gameObject.CompareTag("coin"))
    //    {
    //        GameManager.instance.GiveCoin(10);
    //        Destroy(other.gameObject);
    //        SoundManager.Instance.PlaySFX(SoundManager.Instance.coinPickSFX);
    //    }

    //    if (other.gameObject.CompareTag("levelendportal"))
    //    {
    //        GameManager.instance.LevelCompleteUponEnteringPortal();
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("bot"))
        {
            closestEnemy = GetClosestEnemy();
            canShoot = true;
            enemyContact = true;
            transform.DOLookAt(closestEnemy.transform.position, 0.25f);
        }

        if (other.gameObject.CompareTag("Tree"))
        {
            closestTree = other.GetComponent<Tree>();

            if (closestTree && closestTree.IsFarmable)
            {
                canFarm = true;
                treeContact = true;             
                transform.DOLookAt(new Vector3(closestTree.transform.position.x, transform.position.y, closestTree.transform.position.z), .25f);
            }
        }

        if (other.gameObject.CompareTag("Building"))
        {
            closestBuilding = other.GetComponent<Building>();

            if (closestBuilding && closestBuilding.resourceCost > 0)
            {
                if (!canBuild)
                {
                    canBuild = true;
                    closestBuilding.Build();
                }
                canFarm = true;
                treeContact = true;
                Debug.Log("Building");
                
                transform.DOLookAt(new Vector3(closestBuilding.transform.position.x, transform.position.y, closestBuilding.transform.position.z), .25f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("bot"))
        {
            StopShooting();
        }

        if (other.gameObject.CompareTag("Tree"))
        {
            StopFarming();
        }

        if (other.gameObject.CompareTag("Building"))
        {
            StopBuilding();
        }
    }

    public void StopShooting()
    {
        enemyContact = false;
        canShoot = false;
        muzzleFlash.SetActive(false);
        anim.SetBool("Attack", false);
    }

    public void StopFarming()
    {
        farmDelay = farmDelayMax;
        treeContact = false;
        canFarm = false;
        anim.SetBool("Farm", false);
    }

    public void StopBuilding()
    {
        buildDelay = buildDelayMax;
        buildingContact = false;
        canBuild = false;
        anim.SetBool("Build", false);
    }

    public override void Attack()
    {
        AutoShoot();
    }

    private void AutoShoot()
    {
        shootDelay += Time.deltaTime;
        if (shootDelay >= shootDelayMax)
        {
            anim.SetBool("Attack",true);
            muzzleFlash.SetActive(true);
            GameObject bulletIns = Instantiate(bullet, firePoints[0].position, firePoints[0].rotation);
            //bulletIns.GetComponent<Missile>().target = closestEnemy.transform;
            Destroy(bulletIns, 3f);

            GameObject bshellIns = Instantiate(bulletShellPrefab, shellPoint.position, shellPoint.rotation);
            //bshellIns.GetComponent<Rigidbody>().AddExplosionForce(100f,Vector3.right * 50,50,50,ForceMode.Impulse);
            Destroy(bshellIns, 1.5f);
            shootDelay = 0;

            SoundManager.Instance.PlaySFX(SoundManager.Instance.GetRandomShootSFX());

            //triple shoot power up
            if (hasTripleShoot)
            {
                GameObject bulletDRIns = Instantiate(bullet, firePoints[1].position, firePoints[1].rotation);
                Destroy(bulletDRIns, 3f);
                GameObject bulletDLIns = Instantiate(bullet, firePoints[2].position, firePoints[2].rotation);
                Destroy(bulletDLIns, 3f);
            }
    
        }
    }

    private void AutoFarm()
    {
        farmDelay += Time.deltaTime;
        if (farmDelay >= farmDelayMax)
        {
            farmDelay = 0;
            anim.SetBool("Farm", true);
            StartCoroutine(Farm());
        }
    }

    private void AutoBuild()
    {
        farmDelay += Time.deltaTime;
        if (farmDelay >= farmDelayMax)
        {
            farmDelay = 0;
            anim.SetBool("Farm", true);
            StartCoroutine(Farm());
        }
    }

    private IEnumerator Farm()
    {
        yield return farmingDelay;
        if (canFarm == false) yield break;
        int acquiredResource = closestTree.Farm();
        if (acquiredResource > 0)
        {
            resource += acquiredResource;
        }
        CameraShaker.Instance.ShakeOnce(2f, 1.5f, .1f, .15f);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.GetRandomFarmSFX());
    }

    public void SimulateFootsteps()
    {
        footstepDelay += Time.deltaTime;
        if (footstepDelay >= 0.3f)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.footstepSFX);
            footstepDelay = 0;
        }
    }

    public void ViewTapEffect()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(myRay, out hitInfo, 100, clickable))
        {
            GameObject tap = Instantiate(GameManager.instance.tapFX, new Vector3(hitInfo.point.x, 1.35f, hitInfo.point.z), Quaternion.identity);
            Destroy(tap, 1f);
        }
    }

    private GameObject GetClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("bot");
        float closestDistance = Mathf.Infinity;
        GameObject enemy = null;

        foreach (GameObject go in enemies)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                enemy = go;
            }
        }
        return enemy;
    }

    private GameObject GetClosestTree()
    {
        trees = GameObject.FindGameObjectsWithTag("Tree");
        float closestDistance = Mathf.Infinity;
        GameObject tree = null;

        foreach (GameObject go in trees)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                tree = go;
            }
        }
        return tree;
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        PlayerPrefs.SetInt(healthKey,health);
        UIManager.instance.healthBar.fillAmount = (float)health / (float)maxHealth;
        if (health <= 0)
        {
            Die();
        }
        if (waveScale < maxWaveScale)
        {
            //++waveScale;
            waveScale += .5f;
            //waveFx.transform.localScale = Vector3.one * waveScale;
            waveFx.DOScale(Vector3.one * waveScale, .25f);
            if (waveScale >= maxWaveScale)
            {
                waveBlastFx.SetActive(true);
                waveScale = 1;
                waveFx.DOScale(Vector3.one, .25f);
            }
        }
        UIManager.instance.HitFlash();
        CameraShaker.Instance.ShakeOnce(5f, 2f, .15f, .25f); 
        GameObject dmgPop = Instantiate(GameManager.instance.dmgTextPopupPrefab, transform.position, Quaternion.identity);
        dmgPop.transform.GetComponent<TextMeshPro>().text = "-" + damage * 10;
        dmgPop.transform.GetComponent<TextMeshPro>().DOFade(50, .3f);
        dmgPop.transform.DOMoveY(dmgPop.transform.position.y + 2, 0.3f);
        Destroy(dmgPop, 0.3f);
    }

    public override void Die()
    {
        isDead = true;
        enabled = false;
        UIManager.instance.gamePanel.SetActive(false);
        UIManager.instance.gameOverPanel.SetActive(true);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.GetRandomKillSFX());
    }

    public void GetPlayerHealth()
    {
        health = PlayerPrefs.GetInt(healthKey, maxHealth);
        UIManager.instance.healthBar.fillAmount = (float)health / (float)maxHealth;
    }
}
