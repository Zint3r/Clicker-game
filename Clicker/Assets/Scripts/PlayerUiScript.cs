using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUiScript : MonoBehaviour
{
    private LinksScript links = null;
    [SerializeField] private Image attackCooldownImg = null;
    [SerializeField] private Image defenseImg = null;    
    [SerializeField] private PlayerClickController playerClickController = null;    
    [SerializeField] private GameObject playerDamageText = null;
    [SerializeField] private Text currentExpText = null;
    [SerializeField] private Slider sliderExp = null;
    [SerializeField] private Text currentHpText = null;
    [SerializeField] private Slider sliderHp = null;
    [SerializeField] private Text currentMpText = null;
    [SerializeField] private Slider sliderMp = null;
    [SerializeField] private GameObject endGamePanel = null;
    [SerializeField] private Text goldCountText = null;
    private PlayerControllerScript playerController = null;
    private GameMainScript gameMain = null;
    private float maxDuration = 0;
    private float currentDuration = 0;
    private bool blockPosible = true;
    public bool BlockPosible { get => blockPosible; set => blockPosible = value; }
    private void Start()
    {
        links = GetComponent<LinksScript>();
        playerController = links.PlayerController;
        gameMain = links.GameMain;
        ChangeHpUi(links.PlayerStats.CurrentPlayerHp, links.PlayerStats.MaxPlayerHp);
        ChangeMpUi(links.PlayerStats.CurrentPlayerMp, links.PlayerStats.MaxPlayerMp);
        ChangeExpUi(links.PlayerStats.CurrentExp, links.PlayerStats.ExpToLevelUp);
    }
    private void Update()
    {
        AttackCooldown();
        DefenseCooldown();
    }
    private void OnEnable()
    {
        playerClickController.OnClick += OnClickDefenseButton;
    }
    private void OnDisable()
    {
        playerClickController.OnClick -= OnClickDefenseButton;
    }
    public void EndGame()
    {
        endGamePanel.SetActive(true);
        goldCountText.text = links.PlayerStats.GoldCount.ToString();
    }
    public void ChangeHpUi(int currentHp, int maxHp)
    {
        currentHpText.text = currentHp.ToString() + "/" + maxHp.ToString();
        sliderHp.value = (float)currentHp / (float)maxHp;
    }
    public void ChangeMpUi(int currentMp, int maxMp)
    {
        currentMpText.text = currentMp.ToString() + "/" + maxMp.ToString();
        sliderMp.value = (float)currentMp / (float)maxMp;
    }
    public void ChangeExpUi(int currentExp, int expToLevelUp)
    {
        currentExpText.text = currentExp.ToString() + "/" + expToLevelUp.ToString();
        sliderExp.value = (float)currentExp / (float)expToLevelUp;
    }
    public void DamageTextAnimation(int damage)
    {
        GameObject newCanvas = Instantiate(playerDamageText);       
        Text textDamage = newCanvas.gameObject.GetComponentInChildren<Text>();
        textDamage.text = damage.ToString();                
        if (links.PlayerStats.CurrentHit == true)
        {
            Image critImg = newCanvas.GetComponentInChildren<Image>();
            critImg.enabled = true;
            StartCoroutine(DamageTextCoroutine(newCanvas, textDamage, critImg));
        }
        else
        {
            StartCoroutine(DamageTextCoroutine(newCanvas, textDamage));
        }
    }
    public void AttackCooldownTime(float cooldown)
    {
        maxDuration = cooldown;
        currentDuration = cooldown;
        attackCooldownImg.gameObject.SetActive(true);        
    }
    private void AttackCooldown()
    {
        if (currentDuration > 0)
        {
            currentDuration -= Time.deltaTime;
            attackCooldownImg.fillAmount += Time.deltaTime / maxDuration;
            if (currentDuration <= 0)
            {
                attackCooldownImg.gameObject.SetActive(false);
                attackCooldownImg.fillAmount = 0;
                playerController.Cooldown = false;
            }
        }
    }
    private void DefenseCooldown()
    {
        if (gameMain.CurrentEnemy.IsEnemyAttack == true && blockPosible == true)
        {
            defenseImg.enabled = true;
        }
        else if (gameMain.CurrentEnemy.IsEnemyAttack == false)
        {            
            defenseImg.enabled = false;
            blockPosible = true;
        }
    }
    private IEnumerator DamageTextCoroutine(GameObject obj, Text text)
    {
        text.rectTransform.position += new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0);
        while (true)
        {
            for (int i = 0; i <= 20; i++)
            {
                if (text != null)
                {
                    text.rectTransform.position += new Vector3(0, 5, 0);
                    text.fontSize++;                    
                }
                if (i == 20)
                {
                    if (text != null)
                    {
                        Destroy(obj);
                    }
                    yield break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    private IEnumerator DamageTextCoroutine(GameObject obj, Text text, Image img)
    {
        img.rectTransform.position += new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0);
        while (true)
        {
            for (int i = 0; i <= 20; i++)
            {
                if (text != null)
                {                    
                    text.fontSize++;
                    img.rectTransform.position += new Vector3(0, 5, 0);
                }                
                if (i == 20)
                {
                    if (text != null)
                    {
                        Destroy(obj);
                    }
                    yield break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    private void OnClickDefenseButton()
    {
        defenseImg.enabled = false;
    }
}