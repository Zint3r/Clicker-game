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
    [SerializeField] private int[] upgradeArmorBonus = null;
    [SerializeField] private int[] upgradeWeaponBonus = null;
    private int currentUpgradeLvl = 0;
    private PlayerStatsScript playerStats = null;
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStatsScript>();
        ViewUpgradeParametrs();
    }
    public void SetUpgradeLvl(int lvl)
    {
        currentUpgradeLvl = lvl;
    }
    public void ViewUpgradeParametrs()
    {
        itemImage.sprite = upgradeImages[currentUpgradeLvl];
        bonusText.text = upgradeBonusTexts[currentUpgradeLvl];
        priceText.text = upgradePrices[currentUpgradeLvl].ToString();
        if (currentUpgradeLvl > 0)
        {
            lvlText.enabled = true;
            lvlText.text = currentUpgradeLvl.ToString();
        }
        else
        {
            lvlText.enabled = false;
        }        
    }
    public void Upgrades()
    {
        if (playerStats.GoldCount >= upgradePrices[currentUpgradeLvl] && currentUpgradeLvl < upgradeBonusTexts.Length)
        {
            playerStats.GoldCount -= upgradePrices[currentUpgradeLvl];
            currentUpgradeLvl++;
            playerStats.CalculateArmor(upgradeArmorBonus[currentUpgradeLvl] - upgradeArmorBonus[currentUpgradeLvl - 1]);
            playerStats.CalculateWeaponDamage(upgradeWeaponBonus[currentUpgradeLvl] - upgradeWeaponBonus[currentUpgradeLvl - 1]);
            ViewUpgradeParametrs();            
        }
    }
    public void UpgradeHelm()
    {
        playerStats.HelmItenLvl = currentUpgradeLvl;
        playerStats.CalculateItemsBonus();
    }
    public void UpgradeBodyArmor()
    {
        playerStats.BodyArmorItemLvl = currentUpgradeLvl;
        playerStats.CalculateItemsBonus();
    }
    public void UpgradeShield()
    {
        playerStats.ShieldItemLvl = currentUpgradeLvl;
        playerStats.CalculateItemsBonus();
    }
    public void UpgradeWeapon()
    {
        playerStats.WeaponItemLvl = currentUpgradeLvl;
        playerStats.CalculateItemsBonus();
    }
}