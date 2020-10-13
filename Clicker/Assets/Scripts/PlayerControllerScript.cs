using System.Collections;
using UnityEngine;
public class PlayerControllerScript : MonoBehaviour
{
    private LinksScript links = null;
    [SerializeField] private PlayerClickController playerAttackButton = null;
    [SerializeField] private PlayerClickController playerDefensButton = null;
    [SerializeField] private PlayerClickController playerSpellButton = null;
    [SerializeField] private PlayerClickController playerBuffsButton = null;
    [SerializeField] private BackGroundScript backGround = null;
    private Camera cam = null;
    private PlayerUiScript playerUi = null;
    private GameMainScript gameMain = null;
    private EnemySpawnScript enemySpawn = null;    
    private float moveTimer = 0;
    private bool cooldown = false;
    private Vector3 nextPosition;
    private int continuitySpell = 0;
    private float currentContinuitySpellTimer = 0;
    private float currentBuffTimer = 0;
    private bool buffUp = false;
    public bool Cooldown { set => cooldown = value; }
    private void Start()
    {
        cam = Camera.main;
        links = GetComponent<LinksScript>();
        playerUi = links.PlayerUi;
        gameMain = links.GameMain;
        enemySpawn = links.EnemySpawn;
    }
    private void Update()
    {
        ContinuitySpellTimer();
    }
    private void OnEnable()
    {
        playerAttackButton.OnClick += PlayerAttack;
        playerDefensButton.OnClick += PlayerDefense;
        playerSpellButton.OnClick += PlayerSpell;
        playerBuffsButton.OnClick += PlayerBuff;
    }
    private void OnDisable()
    {
        playerAttackButton.OnClick -= PlayerAttack;
        playerDefensButton.OnClick -= PlayerDefense;
        playerSpellButton.OnClick -= PlayerSpell;
        playerBuffsButton.OnClick -= PlayerBuff;
    }    
    public void PlayerAttack()
    {
        if (cooldown == false && gameMain.CurrentEnemy != null && !gameMain.CurrentEnemy.EnemeDead())
        {
            cooldown = true;
            playerUi.AttackCooldownTime(gameMain.GetCooldownAttack());
            PlayerHit(1);
        }        
    }
    public void PlayerDefense()
    {
        if (gameMain.CurrentEnemy.IsEnemyAttack == true && playerUi.BlockPosible == true && gameMain.CurrentEnemy != null && !gameMain.CurrentEnemy.EnemeDead())
        {
            playerUi.BlockPosible = false;
            PlayerHit(2);
        }
    }
    public void PlayerSpell()
    {
        if (gameMain.CurrentEnemy != null && !gameMain.CurrentEnemy.EnemeDead() && links.PlayerStats.ManaCost(5))
        {
            if (continuitySpell < 3)
            {
                continuitySpell++;
            }            
            int damage = links.PlayerStats.PlayerSpellDamage() * continuitySpell;
            gameMain.CurrentEnemy.EnemyReceiveDamage(damage);
            playerUi.DamageTextAnimation(damage);
            KillEnemy();
            playerUi.ChangeMpUi(links.PlayerStats.CurrentPlayerMp, links.PlayerStats.MaxPlayerMp);
            playerUi.SpellMultyOn(continuitySpell);
        }
    }
    public void PlayerBuff()
    {
        if (links.PlayerStats.ManaCost(10) && buffUp == false)
        {
            buffUp = true;
            playerUi.ChangeMpUi(links.PlayerStats.CurrentPlayerMp, links.PlayerStats.MaxPlayerMp);
            int buffPower = links.PlayerStats.BuffPower();
            links.PlayerStats.BuffDamageCalculate(buffPower);
            links.PlayerStats.BuffArmorCalculate(buffPower);
            currentBuffTimer = 15f;
            StartCoroutine(BuffTimmer(currentBuffTimer));
        }        
    }
    private void NextEnemy()
    {
        gameMain.CurrentEmenyNumber++;
        if (gameMain.EndGameCheck() == true)
        {
            links.PlayerUi.EndGame();
        }
        else
        {
            nextPosition = backGround.BackGroundsPositions[links.GameMain.CurrentEmenyNumber].gameObject.transform.position;
            StartCoroutine(BackGroundMove());
        }
    }
    private IEnumerator BackGroundMove()
    {
        while (true)
        {
            if (moveTimer >= 6f && gameMain.CurrentEnemy == null)
            {
                moveTimer = 0;
                enemySpawn.EnemySpawn(gameMain.CurrentEmenyNumber, new Vector3(cam.transform.position.x, 0, 0));                
                yield break;
            }
            else if (moveTimer >= 3f && gameMain.CurrentEnemy == null)
            {
                moveTimer += Time.deltaTime;
                float pos = (nextPosition.x * Time.deltaTime) / (3 * links.GameMain.CurrentEmenyNumber);
                cam.transform.position += new Vector3(pos, 0, 0);                
            }
            else if (moveTimer < 3f)
            {
                moveTimer += Time.deltaTime;
            }
            yield return null;
        }            
    }
    private void PlayerHit(int multiplier)
    {
        if (gameMain.CurrentEnemy != null && !gameMain.CurrentEnemy.EnemeDead())
        {
            int damage = links.PlayerStats.PlayerMiddleDamage() * multiplier;
            gameMain.CurrentEnemy.EnemyReceiveDamage(damage);
            playerUi.DamageTextAnimation(damage);            
            KillEnemy();
            if (multiplier == 2)
            {
                int regenHp = damage / 5;
                links.PlayerStats.RegenHp(regenHp);
                playerUi.ChangeHpUi(links.PlayerStats.CurrentPlayerHp, links.PlayerStats.MaxPlayerHp);
            }
            else if (multiplier == 1)
            {
                int regenMp = damage / 3;
                links.PlayerStats.RegenMp(regenMp);
                playerUi.ChangeMpUi(links.PlayerStats.CurrentPlayerMp, links.PlayerStats.MaxPlayerMp);
            }
        }            
    }
    private void KillEnemy()
    {
        if (gameMain.CurrentEnemy != null && gameMain.CurrentEnemy.EnemeDead())
        {
            Destroy(gameMain.CurrentEnemy.gameObject, 3f);
            links.PlayerStats.ReceivingExp(33);
            links.PlayerStats.ReceivingGold(Random.Range(2 * gameMain.CurrentEnemy.EnemyLevel, 4 * gameMain.CurrentEnemy.EnemyLevel));
            playerUi.ChangeExpUi(links.PlayerStats.CurrentExp, links.PlayerStats.ExpToLevelUp);
            NextEnemy();
        }                    
    }
    private void ContinuitySpellTimer()
    {
        if (currentContinuitySpellTimer < 3)
        {
            currentContinuitySpellTimer += Time.deltaTime;
        }
        else
        {
            currentContinuitySpellTimer = 0;
            continuitySpell = 0;
            playerUi.SpellMultyOff();
        }
    }
    private IEnumerator BuffTimmer(float duration)
    {
        while (true)
        {
            if (currentBuffTimer > 0 )
            {
                currentBuffTimer -= Time.deltaTime;
                links.PlayerUi.BuffCooldown(duration);
            }
            else
            {
                currentBuffTimer = 0;
                buffUp = false;
                links.PlayerStats.BuffDamageCalculate(0);
                links.PlayerStats.DownBuffArmor();
                yield break;
            }
            yield return null;
        }       
    }
}