using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using TMPro;
using EZCameraShake;

public enum botClass
{
    follower,
    shooter,
    shooter4Dir,
    sniper,
    cloner,
    bomber
}
public class BotController : MonoBehaviour
{
    public botClass bc;
    Rigidbody rb;
    public int[] damage;
    public GameObject[] bulletTypes;
    NavMeshAgent agent;
    public Transform target;
    public float lookRadius = 25f;
    public float shootRadius;
    public int health;
    public bool canAttack;
    public bool attacked;
    public bool dead;

    [Header("shooter")]
    public Transform firePoint;
    public float shootDelay;
    public float shootDelayMax;
    [Header("sniper")]
    public GameObject ray;
    [Header("cloner")]
    public int cloneLevel;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        target = GameManager.instance.player.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!GameManager.instance.startGame) return;
        if (dead) return;
        if (GameManager.instance.playerCon.isDead) return;
        if (GameManager.instance.victory) return;
        //if (!GameManager.instance.gameStart) return;

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            if (bc == botClass.follower || bc == botClass.cloner)
            {
                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance + 2f)
                {
                    canAttack = true;

                    if (canAttack && !attacked)
                    {
                        StartCoroutine(HitPlayerRoutine());
                    }
                }
                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }

        }
        if (bc == botClass.shooter)
        {
            if (distance <= shootRadius)
            {
                transform.DOLookAt(target.position, 0.25f);
                ShootPlayer();
            }
        }
        if (bc == botClass.sniper)
        {
            if (distance <= shootRadius)
            {
                transform.DOLookAt(target.position, 0.25f);
                SnipePlayer();
            }
            else
            {
                ray.SetActive(false);
            }
        }
        if (bc == botClass.bomber)
        {
            if (distance <= shootRadius)
            {
                transform.DOLookAt(target.position, 0.25f);
                BombPlayer();
            }
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }

    IEnumerator HitPlayerRoutine()
    {

        canAttack = false;
        attacked = true;

        GameManager.instance.playerCon.health-= damage[0];
        PlayerPrefs.SetInt(GameManager.instance.playerCon.healthKey, GameManager.instance.playerCon.health);
        UIManager.instance.healthBar.fillAmount = (float)GameManager.instance.playerCon.health / (float)GameManager.instance.playerCon.maxHealth;

        //GetComponentInChildren<Animator>().SetTrigger("isEating");
        //UIManager.instance.hurtFrame.SetActive(true);
        //SoundManager.Instance.PlaySFX(SoundManager.Instance.hitSFX);

        yield return new WaitForSeconds(0.35f);

        //UIManager.instance.hurtFrame.SetActive(false);//fails to turn off sometimes 

        yield return new WaitForSeconds(2f);

        //UIManager.instance.hurtFrame.SetActive(false);
        canAttack = true;
        attacked = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            int dmg = 1;
            health-= dmg;
            GameObject dmgPop = Instantiate(GameManager.instance.dmgTextPopupPrefab, transform.position, Quaternion.identity);
            dmgPop.transform.GetComponent<TextMeshPro>().text = "-" + dmg * 10;
            dmgPop.transform.GetComponent<TextMeshPro>().DOFade(50, .3f);
            dmgPop.transform.DOMoveY(dmgPop.transform.position.y + 1, 0.3f);
            Destroy(dmgPop, 0.3f);

            Destroy(other.gameObject);
            GameObject pop = Instantiate(GameManager.instance.hitFX, transform.position, Quaternion.identity);
            Destroy(pop, 1f);

            transform.DOShakeScale(0.2f, 0.15f);

            if (health <= 0)
            {
                Death();
            }

        }
    }

    public void ShootPlayer()
    {
        shootDelay += Time.deltaTime;
        if (shootDelay >= shootDelayMax)
        {
            GameObject bulletIns = Instantiate(bulletTypes[0], firePoint.position, firePoint.rotation);
            Destroy(bulletIns, 7f);
            shootDelay = 0;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.enemyShootSFX);
        }
    }
    public void SnipePlayer()
    {
        
        shootDelay += Time.deltaTime;

        if (shootDelay >= 0.5f && shootDelay <= 1.5f)
        {
            ray.SetActive(true);
        }
        if (shootDelay >= shootDelayMax)
        {
            GameObject bulletIns = Instantiate(bulletTypes[0], firePoint.position, firePoint.rotation);
            bulletIns.GetComponent<BulletController>().speed = 25;
            Destroy(bulletIns, 7f);
            shootDelay = 0;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.sniperShotSFX);

            ray.SetActive(false);
        }
     
    }

    public void BombPlayer()
    {
        shootDelay += Time.deltaTime;
        if (shootDelay >= shootDelayMax)
        {
            GameObject bulletIns = Instantiate(bulletTypes[0], firePoint.position, firePoint.rotation);
            bulletIns.GetComponent<Missile>().target = GameManager.instance.player.transform;
            //Destroy(bulletIns, 3f);
            shootDelay = 0;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.enemyShootSFX);
        }
    }

    public void GiveDamage()
    {

    }
    public void Death()
    {
        dead = true;
        gameObject.tag = "botDead";
        gameObject.layer = 11;
        //GetComponentInChildren<Animator>().SetTrigger("isDied");
        enabled = false;
        agent.enabled = false;
        GetComponent<Collider>().enabled = false;
        rb.useGravity = false;
        GameManager.instance.player.GetComponent<PlayerController>().canShoot = false;
        Destroy(gameObject);
        GameObject hit = Instantiate(GameManager.instance.popFX, transform.position, Quaternion.identity);
        hit.GetComponent<ParticleSystem>().startColor=gameObject.GetComponent<MeshRenderer>().material.color;
        GameManager.instance.player.GetComponent<PlayerController>().enemyContact = false;
        Destroy(hit, 1f);
        GameObject coinIns = Instantiate(GameManager.instance.coinPrefab, transform.position, GameManager.instance.coinPrefab.transform.rotation);

        //Camera.main.transform.DOShakeRotation(0.35f,1,10,90);
        CameraShaker.Instance.ShakeOnce(4.5f, 2f, .15f, .15f);

        SoundManager.Instance.PlaySFX(SoundManager.Instance.GetRandomKillSFX());

        if (ray != null) ray.SetActive(false);

        if (bc == botClass.cloner && cloneLevel==0)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.enemyCloneSFX);

            for (int i = 0; i < 2; i++)
            {
                GameObject clone = Instantiate(BotSpawner.instance.enemy_cloner, transform.position, BotSpawner.instance.enemy_cloner.transform.rotation);
                clone.transform.localScale = new Vector3(1.5f, 2, 1.5f);
                clone.GetComponent<MeshRenderer>().material = BotSpawner.instance.clonerMat;
                clone.GetComponent<BotController>().cloneLevel = 1;
                clone.GetComponent<BotController>().health /= 2;
            }
        }

        GameManager.instance.CheckLevelProgression();

    }
}
