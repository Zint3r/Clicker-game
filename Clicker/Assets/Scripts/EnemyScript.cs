using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    private Animator _animator = null;
    private float _animationLength;
    private float _currentTimer = 0;
    private int _maxEnemyHP;
    private int _enemyDamage = 1;
    private LinksScript links = null;    
    private int _enemyHP = 10;
    [SerializeField] private string _enemyName = null;
    [SerializeField, Range(1, 20)] private int _enemyLevel = 1;
    [SerializeField] private float _enemyCooldawnAttack = 2f;
    [SerializeField, Range(1, 3)] private int hardMod = 1;
    private EnemyUiScript enemyUI = null;
    public EnemyUiScript EnemyUI { get => enemyUI; set => enemyUI = value; }
    private bool _isEnemyAttack = false;
    public bool IsEnemyAttack { get => _isEnemyAttack; }
    public int EnemyLevel { get => _enemyLevel; set => _enemyLevel = value; }
    void Start()
    {
        EnemyStatsScale();
        _maxEnemyHP = _enemyHP;        
        _animator = GetComponent<Animator>();
        _animationLength = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        links = FindObjectOfType<LinksScript>();        
    }   
    void Update()
    {
        EnemyAttackAndAnimation();        
    }
    public string GetEnemyName()
    {
        return _enemyName;
    }
    public int GetEnemyLevel()
    {
        return _enemyLevel;
    }
    public int EnemyDamage()
    {
        return (int)Random.Range(_enemyDamage / 2, _enemyDamage * 2);
    }
    private void EnemyStatsScale()
    {
        _enemyHP += _enemyLevel * hardMod * 25;
        _enemyDamage += _enemyLevel * 2 + hardMod * 3;
    }
    private void EnemyAttackAndAnimation()
    {
        if (_currentTimer >= _enemyCooldawnAttack && _isEnemyAttack == false && _enemyHP > 0)
        {
            _currentTimer = 0;
            _animator.SetBool("EnemyAttack", true);
            _isEnemyAttack = true;
        }
        else if (_currentTimer >= _animationLength && _isEnemyAttack == true && _enemyHP > 0)
        {
            _animator.SetBool("EnemyAttack", false);
            _isEnemyAttack = false;
            _currentTimer += Time.deltaTime;
            links.PlayerStats.ReceivingDamage(EnemyDamage());
            links.PlayerUi.ChangeHpUi(links.PlayerStats.CurrentPlayerHp, links.PlayerStats.MaxPlayerHp);
        }
        else
        {
            _currentTimer += Time.deltaTime;            
        }
    }
    public void EnemyReceiveDamage(int damage)
    {        
        _enemyHP -= damage;
        EnemyUI.ChangeEnemyHP((float) damage/_maxEnemyHP);
        
    }
    public bool EnemeDead()
    {
        if (_enemyHP <= 0)
        {
            _animator.SetBool("EnemyDead", true);
            _enemyHP = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}