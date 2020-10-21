﻿using UnityEngine;
using UnityEngine.UI;
public class InventoryUiScript : MonoBehaviour
{
    private PlayerStatsScript playerStats = null;
    [Header("Base stats")]
    [SerializeField] private Text levelText = null;
    [SerializeField] private Text healthText = null;
    [SerializeField] private Text manaText = null;
    [SerializeField] private Text damageText = null;
    [SerializeField] private Text spellText = null;
    [SerializeField] private Text critChanceText = null;
    [SerializeField] private Text expText = null;
    [SerializeField] private Text goldCountText = null;
    [Header("Stats point")]
    [SerializeField] private Text strengthText = null;
    [SerializeField] private Text intellectText = null;
    [SerializeField] private Text staminaText = null;
    [SerializeField] private Text luckyText = null;
    [SerializeField] private Text pointsText = null;
    void Start()
    {
        playerStats = GetComponent<PlayerStatsScript>();
        ChangeAllText();
        ChangeGoldText();
    }
    public void ChangeGoldText()
    {
        goldCountText.text = playerStats.GoldCount.ToString();
    }
    public void ChangeLevelText()
    {
        levelText.text = " Level: " + playerStats.PlayerLavel.ToString();
    }
    public void ChangeHealthText()
    {
        healthText.text = " Health: " + playerStats.MaxPlayerHp.ToString();
    }
    public void ChangeManaText()
    {
        manaText.text = " Mana: " + playerStats.MaxPlayerMp.ToString();
    }
    public void ChangeDamageText()
    {
        damageText.text = " Damage: " + playerStats.MinDamage.ToString() + " - " + playerStats.MaxDamage.ToString();
    }
    public void ChangeSpellText()
    {
        spellText.text = " Spell: " + playerStats.SpellDamage.ToString();
    }
    public void ChangeCritChanceText()
    {
        critChanceText.text = " Crit chance: " + playerStats.CriticalChance.ToString() + " %";
    }
    public void ChangeExpText()
    {
        expText.text = " Exp: " + playerStats.CurrentExp.ToString() + " / " + playerStats.ExpToLevelUp.ToString();
    }
    public void ChangeStrengthText()
    {
        strengthText.text = playerStats.Strength.ToString();
    }
    public void ChangeIntellectText()
    {
        intellectText.text = playerStats.Intellect.ToString();
    }
    public void ChangeStaminaText()
    {
        staminaText.text = playerStats.Stamina.ToString();
    }
    public void ChangeLuckyText()
    {
        luckyText.text = playerStats.Lucky.ToString();
    }
    public void ChangeStatsPointsText()
    {
        pointsText.text = playerStats.FreeStatsPoints.ToString();
    }
    public void ChangeAllText()
    {
        ChangeLevelText();
        ChangeHealthText();
        ChangeManaText();
        ChangeDamageText();
        ChangeSpellText();
        ChangeCritChanceText();
        ChangeExpText();
        ChangeStrengthText();
        ChangeIntellectText();
        ChangeStaminaText();
        ChangeLuckyText();
        ChangeStatsPointsText();
    }
}