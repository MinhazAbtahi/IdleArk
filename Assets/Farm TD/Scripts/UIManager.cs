using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
  
    public GameObject gamePanel;
    public GameObject gameOverPanel;
    public Button nextBtn;
    
    [Header("Start Panel")]
    public GameObject startPanel;
    public Button playBtn;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtWorld;
    public TextMeshProUGUI txtCoin;
    public GameObject tutorial;

    [Header("Game Panel")]
    public Image healthBar;
    public Image levelProgBar;
    public Image hitImage;
    private Color hitColor;
    private Color flashColor;
    private bool isHit;
    private WaitForSeconds hitCoooldownDelay = new WaitForSeconds(.25f);
    [HideInInspector] public bool howtoPlayTapped;

    private void Awake()
    {
        instance = this;
        playBtn.onClick.AddListener(() => PlayBtnCallback());
        nextBtn.onClick.AddListener(() => NextBtnCallback());
    }
    
    // Start is called before the first frame update
    void Start()
    {
        txtLevel.text = "Level- " + (GameManager.instance.levelID).ToString();
        txtWorld.text = "World- " + (GameManager.instance.worldID).ToString();
        txtCoin.text = GameManager.instance.totalCoin.ToString();
        hitColor = new Color(hitImage.color.r, hitImage.color.g, hitImage.color.b, .45f);
        flashColor = Color.white;
        levelProgBar.fillAmount = (float)GameManager.instance.botsKilled / (float)GameManager.instance.botsToKill;
    }

    public void PlayBtnCallback()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        GameManager.instance.startGame = true;
        //GameManager.instance.portalFX.transform.DOScale();
    }

    public void HitFlash()
    {
        if (!isHit)
        {
            hitImage.DOColor(hitColor, .1f).OnComplete(() =>
            {
                hitImage.DOFade(0f, .05f).OnComplete(() =>
                {
                    isHit = false;
                });
            });
        }
    }

    //private IEnumerator HitCoolDownRoutine()
    //{
    //    yield return
    //}

    public void NextBtnCallback()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
