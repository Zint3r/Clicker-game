using UnityEngine;
using SimpleJSON;
using System.IO;
public class PlayerStatsScript : MonoBehaviour
{
    private string path = null;
    [SerializeField] private int strength = 1;
    [SerializeField] private int intellect = 1;
    [SerializeField] private int stamina = 1;
    [SerializeField] private int lucky = 1;
    [SerializeField] private int freeStatsPoints = 5;
    private int playerLavel = 1;
    private int maxPlayerHp;
    private int currentPlayerHp;
    private int maxPlayerMp;
    private int currentPlayerMp;
    private int armor;
    private int minDamage;
    private int maxDamage;
    private int weaponDamage;
    private int criticalChance;
    private int expToLevelUp = 200;
    private int currentExp = 0;
    private int goldCount;
    private int locationLevel = 1;
    public int Strength { get => strength; }
    public int Intellect { get => intellect; }
    public int StartStamina { get => stamina; }
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
    public int WeaponDamage { get => weaponDamage; set => weaponDamage = value; }
    public int Armor { get => armor; set => armor = value; }
    public int GoldCount { get => goldCount; set => goldCount = value; }
    public int LocationLevel { get => locationLevel; }

    private bool currentHit = false;
    private void Awake()
    {
        path = Application.dataPath + "/Save.json";
        Debug.Log(path);
        CalculateAllStats();
        currentPlayerHp = maxPlayerHp;
        currentPlayerMp = maxPlayerMp / 2;
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
    private void CalculateHp()
    {
        maxPlayerHp = 30 + stamina * 15 + playerLavel * 10;
    }
    private void CalculateMp()
    {
        maxPlayerMp = 10 + intellect * 5 + playerLavel * 2;
    }
    private void CalculateDamage()
    {
        minDamage = 5 + weaponDamage + Strength * 2;
        maxDamage = 10 + weaponDamage + Strength * 2;
    }
    private void CalculateCriticalChance()
    {
        criticalChance = 5 + lucky * 3;
    }
    private void CalculateAllStats()
    {
        CalculateHp();
        CalculateMp();
        CalculateDamage();
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
                strength += StatsCheckAndUp(strength);
                CalculateDamage();
                break;
            case 2:
                intellect += StatsCheckAndUp(intellect);
                CalculateMp();
                break;
            case 3:
                stamina += StatsCheckAndUp(stamina);
                CalculateHp();
                break;
            case 4:
                lucky += StatsCheckAndUp(lucky);
                CalculateCriticalChance();
                break;            
        }
    }
    private int StatsCheckAndUp(int stat)
    {
        if (freeStatsPoints > 0 && stat < 10)
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
        playerJson.Add("Level", playerLavel);
        playerJson.Add("Strength", strength);
        playerJson.Add("Intellect", intellect);
        playerJson.Add("Stamina", stamina);
        playerJson.Add("Lucky", lucky);
        playerJson.Add("FreeStatsPoints", freeStatsPoints);
        playerJson.Add("Gold", goldCount);
        playerJson.Add("CurrentExp", currentExp);
        playerJson.Add("ExpToUp", expToLevelUp);
        playerJson.Add("LocationLevel", locationLevel);
        File.WriteAllText(path, playerJson.ToString());
    }
    public void LoadPlayerStats()
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
        CalculateAllStats();
    }
}