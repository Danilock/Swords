using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class EnemyController : MonoBehaviour
{
    #region Events
    [SerializeField] UnityEvent OnTakeDamage;
    [SerializeField] UnityEvent OnEnemyDead;
    #endregion
    #region Enemy Stats
    public ScriptableEnemy enemyProfile;
    float currentHealth;
    float maxHealth;
    #endregion
    #region Gameplay
    Rigidbody2D rgb2D;
    PlayerController player;
    #endregion
    private void OnEnable()
    {
        OnTakeDamage.AddListener(PushEnemy);
    }

    private void Awake()
    {
        maxHealth = enemyProfile.baseHealth;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0f)
        {
            OnEnemyDead.Invoke();
        }

        OnTakeDamage.Invoke();
    }

    //Push the enemy in the direction where the player is facing.
    public void PushEnemy()
    {
        rgb2D?.AddForce(new Vector2(Mathf.Sign(player.transform.localScale.x) * 1f, 1f), ForceMode2D.Impulse);
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
