using UnityEngine;
public class GameMainScript : MonoBehaviour
{
    private LinksScript links = null;    
    private float cooldownAttack = 1f;
    private int allEnemyCount = 0;
    private NewInputScript input = null;
    private int currentEmenyNumber = 1;
    private EnemyScript currentEnemy = null;
    public int CurrentEmenyNumber { get => currentEmenyNumber; set => currentEmenyNumber = value; }
    public EnemyScript CurrentEnemy { get => currentEnemy; }    
    private void Awake()
    {
        links = GetComponent<LinksScript>();
        input = new NewInputScript();        
        input.Player.Click.performed += function => OnPlayerClick();        
    }
    private void Start()
    {
        allEnemyCount = links.EnemySpawn.EnemyPrefabs.Count;
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    private void OnPlayerClick()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(input.Player.Position.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<ItemScript>() == true)
            {
                ItemScript item = hit.collider.GetComponent<ItemScript>();
                if (item.GoldChest == true)
                {
                    links.PlayerStats.ReceivingGold(Random.Range(5, 25));
                }
                else if (item.HpPotion == true)
                {
                    int hp = (int)(links.PlayerStats.MaxPlayerHp * 0.25);
                    links.PlayerStats.RegenHp(hp);
                    links.PlayerUi.ChangeHpUi(links.PlayerStats.CurrentPlayerHp, links.PlayerStats.MaxPlayerHp);
                }
                else if (item.MpPotion == true)
                {
                    int mp = (int)(links.PlayerStats.MaxPlayerMp * 0.25);
                    links.PlayerStats.RegenMp(mp);
                    links.PlayerUi.ChangeMpUi(links.PlayerStats.CurrentPlayerMp, links.PlayerStats.MaxPlayerMp);
                }
                hit.collider.GetComponent<ItemScript>().OnChestClick();                
            }
        }             
    }    
    public void SetCurrentEnemy(EnemyScript enemy)
    {
        currentEnemy = enemy;
    }    
    public float GetCooldownAttack()
    {
        return cooldownAttack;
    }
    public bool EndGameCheck()
    {
        if (allEnemyCount - 1 == currentEmenyNumber)
        {            
            return true;
        }
        else
        {            
            return false;
        }
    }
    public bool GameOver(int playerHealth)
    {
        if (playerHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}