using UnityEngine;
public class LinksScript : MonoBehaviour
{
    [Header("Base links")]
    [SerializeField] private GameMainScript gameMain = null;
    [SerializeField] private PlayerControllerScript playerController = null;
    [SerializeField] private PlayerStatsScript playerStats = null;
    [SerializeField] private PlayerUiScript playerUi = null;    
    [SerializeField] private EnemySpawnScript enemySpawn = null;
    public GameMainScript GameMain { get => gameMain; }
    public PlayerControllerScript PlayerController { get => playerController; }
    public PlayerStatsScript PlayerStats { get => playerStats; }
    public PlayerUiScript PlayerUi { get => playerUi; }
    public EnemySpawnScript EnemySpawn { get => enemySpawn; }
}