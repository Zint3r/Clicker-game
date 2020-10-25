using UnityEngine;
using SimpleJSON;
using System.IO;
public class PlayerStatsScript : MonoBehaviour
{
    private string path = null;
    private int locationLevel = 1;
    //Base stats
    private int strength = 1;
    private int intellect = 1;
    private int stamina = 1;
    private int lucky = 1;
    private int freeStatsPoints = 5;
    private int playerLavel = 1;
    //Second stats
    private int maxPlayerHp;
    private int currentPlayerHp;
    private int maxPlayerMp;
    private int currentPlayerMp;    
    private int minDamage;
    private int maxDamage;    
    private int buffDamage;
    private int spellDamage;
    private int criticalChance;
    private int expToLevelUp = 200;
    private int currentExp = 0;    
    private bool currentHit = false;
    //Inventory
    private int goldCount;
    private int armor;
    private int weaponDamage;
    private int helmItenLvl;
    private int bodyArmorItemLvl;
    private int shieldItemLvl;
    private int weaponItemLvl;
    private int strengthBonus;
    private int intellectBonus;
    private int staminaBonus;
    private int luckyBonus;
    //---------------------------------------
    public int Strength { get => strength; }
    public int Intellect { get => intellect; }
    public int Stamina { get => stamina; }
    public int Lucky { get => lucky; }
    public int FreeStatsPoints { get => freeStatsPoints; set => freeStatsPoints = value; }
    public int ExpToLevelUp { get => expToLevelUp; }
    public int CurrentExp { get => currentExp; }
    public int MaxPlayerHp { get => maxPlayerHp; }
    public int CurrentPlayerHp { get => currentPlayerHp; }
    public int MaxPlayerMp { get => maxPlayerMp; }
    public int CurrentPlayerMp { get => currentPlayerMp; }
    public int CriticalChance { get => criticalChance; }
    public bool CurrentHit { get => currentHit; }
    public int WeaponDamage { get => weaponDamage; }
    public int Armor { get => armor; }
    public int GoldCount { get => goldCount; set => goldCount = value; }
    public int LocationLevel { get => locationLevel; }
    public int SpellDamage { get => spellDamage; set => spellDamage = value; }
    public int BuffDamage { get => buffDamage; set => buffDamage = value; }
    public int PlayerLavel { get => playerLavel; }
    public int MinDamage { get => minDamage; set => minDamage = value; }
    public int MaxDamage { get => maxDamage; set => maxDamage = value; }
    public int HelmItenLvl { get => helmItenLvl; set => helmItenLvl = value; }
    public int BodyArmorItemLvl { get => bodyArmorItemLvl; set => bodyArmorItemLvl = value; }
    public int ShieldItemLvl { get => shieldItemLvl; set => shieldItemLvl = value; }
    public int WeaponItemLvl { get => weaponItemLvl; set => weaponItemLvl = value; }

    private void Awake()
    {
        path = Application.dataPath + "/Save.json";
        LoadPlayerStats();
        locationLevel = ScenesParametrs.currentSceneLevel;
        currentPlayerHp = maxPlayerHp;
        currentPlayerMp = maxPlayerMp / 2;
    }
    public void UpLocationLevel()
    {
        ScenesParametrs.NextSceneLevel();
    }
    public int ApplyItemBonus(int itemLvl)
    {
        if (itemLvl < 3 && itemLvl > 0)
        {
            return 1;            
        }
        else if (itemLvl >= 3 && itemLvl > 0)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
    public int PlayerMiddleDamage()
    {
        currentHit = CheckCriticalStrike();
        if (currentHit == true)
        {
            return Random.Range(minDamage, maxDamage) * 2;
        }
        else
        {
            return Random.Range(minDamage, maxDamage);
        }        
    }
    public int PlayerSpellDamage()
    {
        currentHit = CheckCriticalStrike();
        if (currentHit == true)
        {
            return spellDamage * 2;
        }
        else
        {
            return spellDamage;
        }
    }
    public void RegenHp(int hp)
    {
        if (currentPlayerHp + hp < maxPlayerHp)
        {
            currentPlayerHp += hp;
        }
        else
        {
            currentPlayerHp = maxPlayerHp;
        }
    }
    public void RegenMp(int mp)
    {
        if (currentPlayerMp + mp < maxPlayerMp)
        {
            currentPlayerMp += mp;
        }
        else
        {
            currentPlayerMp = maxPlayerMp;
        }
    }
    public bool ManaCost(int cost)
    {
        if (currentPlayerMp >= cost)
        {
            currentPlayerMp -= cost;
            return true;
        }
        else 
        { 
            return false;
        }
    }
    public void ReceivingExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= expToLevelUp)
        {
            PlayerLevelUp();
        }
    }
    public void ReceivingDamage(int damage)
    {
        if (armor < damage)
        {
            currentPlayerHp -= damage - armor;
            if (currentPlayerHp <= 0)
            {
                currentPlayerHp = 0;                
            }
        }
        else
        {
            currentPlayerHp--;
            if (currentPlayerHp <= 0)
            {
                currentPlayerHp = 0;                
            }
        }        
    }
    public void ReceivingGold(int gold)
    {
        goldCount += gold;
    }
    public int BuffPower()
    {
        return PlayerLavel + intellect;
    }
    public void BuffDamageCalculate(int damage)
    {
        buffDamage = damage;
        CalculateDamage();
    }
    public void BuffArmorCalculate(int addArmor)
    {
        armor += addArmor;
    }
    public void DownBuffArmor()
    {
        armor -= BuffPower();
    }
    public void CalculateArmor(int addArmor)
    {
        armor += addArmor;
    }
    public void CalculateWeaponDamage(int addDamage)
    {
        weaponDamage += addDamage;
    }
    public void CalculateItemsBonus()
    {
        strengthBonus = ApplyItemBonus(weaponItemLvl);
        intellectBonus = ApplyItemBonus(helmItenLvl);
        staminaBonus = ApplyItemBonus(bodyArmorItemLvl);
        luckyBonus = ApplyItemBonus(shieldItemLvl);
    }
    private void CalculateHp()
    {
        maxPlayerHp = 30 + (stamina + staminaBonus) * 15 + PlayerLavel * 10;
    }
    private void CalculateMp()
    {
        maxPlayerMp = 10 + (intellect + intellectBonus) * 5 + PlayerLavel * 2;
    }
    private void CalculateDamage()
    {
        minDamage = 5 + weaponDamage + buffDamage + (strength + strengthBonus) * 2;
        maxDamage = 10 + weaponDamage + buffDamage + (strength + strengthBonus) * 2;
    }
    private void CalculateSpellDamage()
    {
        spellDamage = 5 + (intellect + intellectBonus) * 4;
    }
    private void CalculateCriticalChance()
    {
        criticalChance = 5 + (lucky + luckyBonus) * 3;
    }
    private void CalculateAllStats()
    {
        CalculateItemsBonus();
        CalculateHp();
        CalculateMp();
        CalculateDamage();
        CalculateSpellDamage();
        CalculateCriticalChance();                
    }
    private bool CheckCriticalStrike()
    {
        int crit = Random.Range(1, 100);
        if (crit <= criticalChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void PlayerLevelUp()
    {
        playerLavel++;
        CalculateHp();
        CalculateMp();
        freeStatsPoints += 2;
        expToLevelUp = (int)((expToLevelUp + expToLevelUp / 2) * 1.3);
        currentExp = 0;
    }
    public void StatUp(int number)
    {
        switch (number)
        {
            case 1:
                strength += StatsCheckAndUp(strength, strengthBonus);
                CalculateDamage();
                break;
            case 2:
                intellect += StatsCheckAndUp(intellect, intellectBonus);
                CalculateMp();
                break;
            case 3:
                stamina += StatsCheckAndUp(stamina, staminaBonus);
                CalculateHp();
                break;
            case 4:
                lucky += StatsCheckAndUp(lucky, luckyBonus);
                CalculateCriticalChance();
                break;            
        }
    }
    private int StatsCheckAndUp(int stat, int bonus)
    {
        if (freeStatsPoints > 0 && stat < 10 + bonus)
        {            
            freeStatsPoints--;
            return 1;
        }
        else
        {            
            return 0;
        }
    }
    public void NextLocationLevel()
    {
        locationLevel++;
    }
    public void SetLocationLevel(int newLevel)
    {
        locationLevel = newLevel;
    }
    public void SavePlayerStats()
    {
        JSONObject playerJson = new JSONObject();
        playerJson.Add("Level", PlayerLavel);
        playerJson.Add("Strength", strength);
        playerJson.Add("Intellect", intellect);
        playerJson.Add("Stamina", stamina);
        playerJson.Add("Lucky", lucky);
        playerJson.Add("FreeStatsPoints", freeStatsPoints);
        playerJson.Add("Gold", goldCount);
        playerJson.Add("CurrentExp", currentExp);
        playerJson.Add("ExpToUp", expToLevelUp);
        playerJson.Add("LocationLevel", locationLevel);
        playerJson.Add("HelmItenLvl", helmItenLvl);
        playerJson.Add("BodyArmorItemLvl", bodyArmorItemLvl);
        playerJson.Add("ShieldItemLvl", shieldItemLvl);
        playerJson.Add("WeaponItemLvl", weaponItemLvl);
        File.WriteAllText(path, playerJson.ToString());
    }
    public void LoadPlayerStats()
    {
        if (File.Exists(path) == true)
        {
            string jsonString = File.ReadAllText(path);
            JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
            playerLavel = playerJson["Level"];
            strength = playerJson["Strength"];
            intellect = playerJson["Intellect"];
            stamina = playerJson["Stamina"];
            lucky = playerJson["Lucky"];
            freeStatsPoints = playerJson["FreeStatsPoints"];
            goldCount = playerJson["Gold"];
            currentExp = playerJson["CurrentExp"];
            expToLevelUp = playerJson["ExpToUp"];
            locationLevel = playerJson["LocationLevel"];
            helmItenLvl = playerJson["HelmItenLvl"];
            bodyArmorItemLvl = playerJson["BodyArmorItemLvl"];
            shieldItemLvl = playerJson["ShieldItemLvl"];
            weaponItemLvl = playerJson["WeaponItemLvl"];
            CalculateAllStats();
        }
        else
        {
            CalculateAllStats();
            SavePlayerStats();            
        }
    }
}