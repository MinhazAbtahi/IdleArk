using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool startGame;
    public bool victory;

    public GameObject player;
    public PlayerController playerCon;
    [Header("Currency")]
    public static string totalcoinKey = "totalcoinKey";
    public int totalCoin;
    public GameObject coinPrefab;

    [Header("FX")]
    public GameObject tapFX;
    public GameObject popFX;
    public GameObject hitFX;
    public GameObject portalPrefab;
    public GameObject dmgTextPopupPrefab;
    [Header("Level Info")]
    public static string worldKey = "worldKey";
    public int worldID;
    public static string levelKey = "levelKey";
    public int levelID;
    public int botsKilled;
    public int botsToKill;
    [Header("World Info")]
    public GameObject ground;
    public Material[] groundMats;

    public static string gamelevelKey = "gamelevelKey";
    public int gameLevel;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
            playerCon = player.GetComponent<PlayerController>();

        levelID = PlayerPrefs.GetInt(levelKey,1);
        gameLevel = PlayerPrefs.GetInt(gamelevelKey, 1);
        worldID = PlayerPrefs.GetInt(worldKey,1);
        totalCoin = PlayerPrefs.GetInt(totalcoinKey, 0);
        SetCurrentWorld();

    }

    private void Update()
    {
        if (true)
        {

        }
    }

    public void CheckLevelProgression()
    {
        if (!victory)
        {
            botsKilled++;
            UIManager.instance.levelProgBar.fillAmount = (float)botsKilled / (float)botsToKill;
            if (botsKilled >= botsToKill)
            {
                Debug.Log("level complete");
                victory = true;
                GameObject portalIns = Instantiate(portalPrefab, new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z + 3) , portalPrefab.transform.rotation);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.portalSFX);
            }
        }
    }
    public void LevelCompleteUponEnteringPortal()
    {
        ShowInterstitialAd();
        //FPG.FirebaseManager.GetInstance().LogAnalyticsEvent("LevelComplete_" + gameLevel);

        gameLevel++;
        PlayerPrefs.SetInt(gamelevelKey, gameLevel);
        levelID++;
        PlayerPrefs.SetInt(levelKey,levelID);

        
        if(levelID > 3)//world progression
        {
            worldID++;
            PlayerPrefs.SetInt(worldKey, worldID);
            PlayerPrefs.SetInt(levelKey, 1);
            playerCon.health = playerCon.maxHealth;
            PlayerPrefs.SetInt(playerCon.healthKey, playerCon.health);
        }

    }

    public void SetCurrentWorld()
    {
        //// level design
        //if (worldID == 1)
        //{
        //    ground.GetComponent<MeshRenderer>().material = groundMats[0];
        //}
        //else if (worldID %2 == 0)
        //{
        //    ground.GetComponent<MeshRenderer>().material = groundMats[1];
        //}
        //else if (worldID %3 == 0)
        //{
        //    ground.GetComponent<MeshRenderer>().material = groundMats[2];
        //}
        //else
        //{
        //    ground.GetComponent<MeshRenderer>().material = groundMats[0];
        //}
        ////enemy count
        //if (worldID <= 3)
        //{
        //    botsToKill = 5;
        //}
        //else if (worldID > 3 && worldID < 7)
        //{
        //    botsToKill = 7;
        //}
        //else if (worldID >= 7)
        //{
        //    botsToKill = 10;
        //}
    }

    public void GiveCoin(int i)
    {
        totalCoin += i;
        PlayerPrefs.SetInt(totalcoinKey, totalCoin);
    }

    private static int adShowLevelInterval = 0;
    public void ShowInterstitialAd()
    {
        adShowLevelInterval++;

        //if (adShowLevelInterval >= 3 && FPG.MobileAdsManager.GetInstance().IsInterstitialAdAvialable())
        //{
        //    FPG.MobileAdsManager.GetInstance().ShowInterstitial(OnIntersititialAdClosed);
        //    adShowLevelInterval = 0;

        //}
        //else
        //{
        //    OnIntersititialAdClosed();
        //}
    }

    public void OnIntersititialAdClosed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
