using UnityEngine;
using UnityEngine.UI;
public class UpgradeSlotScript : MonoBehaviour
{
    [Header("Links to elements")]
    [SerializeField] private Image itemImage = null;
    [SerializeField] private Text lvlText = null;
    [SerializeField] private Text bonusText = null;
    [SerializeField] private Text priceText = null;
    [Header("Upgrades")]
    [SerializeField] private Sprite[] upgradeImages = null;
    [TextArea(2, 5), SerializeField] private string[] upgradeBonusTexts = null;    
    [SerializeField] private int[] upgradePrices = null;
    private int currentUpgradeLvl = 0;
    private PlayerStatsScript playerStats = null;
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStatsScript>();
        ViewUpgradeParametrs();
    }
    public void ViewUpgradeParametrs()
    {
        itemImage.sprite = upgradeImages[currentUpgradeLvl];
        lvlText.text = (currentUpgradeLvl + 1).ToString();
        bonusText.text = upgradeBonusTexts[currentUpgradeLvl];
        priceText.text = upgradePrices[currentUpgradeLvl].ToString();
    }
    public void Upgrades()
    {
        if (playerStats.GoldCount >= upgradePrices[currentUpgradeLvl])
        {
            playerStats.GoldCount -= upgradePrices[currentUpgradeLvl];
            currentUpgradeLvl++;
            ViewUpgradeParametrs();            
        }
    }
}