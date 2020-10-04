using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    [SerializeField] private string enemyName = null;
    [SerializeField, Range(1, 20)] private int enemyLevel = 1;
    [SerializeField] private float enemyCooldawnAttack = 2f;
    [SerializeField, Range(1, 3)] private int hardMod = 1;
    private LinksScript links = null;
    private Animator animator = null;
    private EnemyUiScript enemyUI = null;
    private float animationLength;
    private float currentTimer = 0;
    private int maxEnemyHP;
    private int enemyDamage = 1;       
    private int enemyHP = 10;
    private bool isEnemyAttack = false;    
    public EnemyUiScript EnemyUI { get => enemyUI; set => enemyUI = value; }    
    public bool IsEnemyAttack { get => isEnemyAttack; }
    public int EnemyLevel { get => enemyLevel; set => enemyLevel = value; }
    void Start()
    {
        EnemyStatsScale();
        maxEnemyHP = enemyHP;        
        animator = GetComponent<Animator>();
        animationLength = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        links = FindObjectOfType<LinksScript>();        
    }   
    void Update()
    {
        EnemyAttackAndAnimation();        
    }
    public string GetEnemyName()
    {
        return enemyName;
    }
    public int GetEnemyLevel()
    {
        return enemyLevel;
    }
    public int EnemyDamage()
    {
        return (int)Random.Range(enemyDamage / 2, enemyDamage * 2);
    }
    private void EnemyStatsScale()
    {
        enemyHP += enemyLevel * hardMod * 25;
        enemyDamage += enemyLevel * 2 + hardMod * 3;
    }
    private void EnemyAttackAndAnimation()
    {
        if (currentTimer >= enemyCooldawnAttack && isEnemyAttack == false && enemyHP > 0)
        {
            currentTimer = 0;
            animator.SetBool("EnemyAttack", true);
            isEnemyAttack = true;
        }
        else if (currentTimer >= animationLength && isEnemyAttack == true && enemyHP > 0)
        {
            animator.SetBool("EnemyAttack", false);
            isEnemyAttack = false;
            currentTimer += Time.deltaTime;
            links.PlayerStats.ReceivingDamage(EnemyDamage());
            links.PlayerUi.ChangeHpUi(links.PlayerStats.CurrentPlayerHp, links.PlayerStats.MaxPlayerHp);
        }
        else
        {
            currentTimer += Time.deltaTime;            
        }
    }
    public void EnemyReceiveDamage(int damage)
    {        
        enemyHP -= damage;
        EnemyUI.ChangeEnemyHP((float) damage/maxEnemyHP);        
    }
    public bool EnemeDead()
    {
        if (enemyHP <= 0)
        {
            animator.SetBool("EnemyDead", true);
            enemyHP = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}