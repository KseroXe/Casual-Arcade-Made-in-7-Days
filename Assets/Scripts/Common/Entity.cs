using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    public float maxHeatlh;
    public float currentHealth;
    public float expDrop;

    public float invincibilityTime;
    public bool isInvincible;
    public bool canBeInvincible;
    public bool isDead;

    private ParticleSystem deathParticleSystem;
    public GameObject HitIndicatorPart;
    private Renderer renderer;
    private PlayerPrefs playerPrefs;
    private UIManager uiManager;
    private DestroyParentOnDeath destroyParentOnDeath;

    private void Awake()
    {
        if (canBeInvincible) uiManager = GetComponent<UIManager>();
        playerPrefs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPrefs>();
        renderer = HitIndicatorPart.GetComponent<Renderer>();
        currentHealth = maxHeatlh;
        deathParticleSystem = transform.parent.GetComponentInChildren<ParticleSystem>();
        destroyParentOnDeath = GetComponentInParent<DestroyParentOnDeath>();
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            StartCoroutine(HitIndicator());
            currentHealth -= damage;
            uiManager?.UpdateHealth();
            CheckIfDead();
            if (canBeInvincible) StartCoroutine(InvincibilityTime());
        }
    }

    public void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            deathParticleSystem.transform.position = transform.position;
            deathParticleSystem?.Play();
            DropExp(expDrop);
            if (gameObject.CompareTag("Enemy")) destroyParentOnDeath.DestroyParent();
            Destroy(gameObject);
        }
    }

    private IEnumerator InvincibilityTime()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    private IEnumerator HitIndicator()
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        renderer.material.color = Color.white;
    }

    public void DropExp(float amount)
    {
        playerPrefs.GainExp(amount);
    }
}
