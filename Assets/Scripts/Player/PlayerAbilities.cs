using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    //Gun
    public float shootForce;
    public Transform weaponTransform;
    public GameObject bulletPrefab;
    public float cooldownBetweenShots;
    private bool onCooldown = false;
    [Space]
    //ShockWave ability
    public bool shockWaveAvailible;
    public float shockWaveCooldown;
    public float shockwavedamage;
    private bool shockWaveOnCooldown;
    public ParticleSystem shockWaveParticleSystem;
    [Space]
    //Burst ability
    public bool burstAvailible;
    public float burstCooldown;
    public bool burstOnCooldown;
    public int bulletsAmount;
    public float spread;
    [Space]
    //Machinegun ability
    public bool machinegunAvailible;
    public float machinegunCooldown;
    public bool machinegunOnCooldown;
    public float machinegunDuration;
    public float newShotCooldown;
    [Space]
    //Airstrike ability
    public bool airstrikeAvailible;
    public float airstrikeCooldown;
    public bool airstrikeOnCooldown;
    public float airstrikeDamage;
    public GameObject airstrikeAreaSphere;
    public GameObject airstrikePosition;
    [Space]
    //Shield ability
    public bool shieldAvailible;
    public float shieldCooldown;
    public bool shieldOnCooldown;
    public int shieldDurability;
    public float shieldDamage;
    public GameObject shield;

    //Upgrade mechanics
    private int shockwaveLevel;
    private int airstrikeLevel;
    private int shieldLevel;
    private int burstLevel;
    private int machinegunLevel;

    [Space]
    public AudioClip shootSound;
    public AudioClip shockwaveSound;
    public AudioClip airstrikeSound;
    public AudioClip shieldSound;

    private InputManager inputManager;
    private UIManager uiManager;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        uiManager = GetComponent<UIManager>();
        inputManager = GetComponent<InputManager>();

        shockWaveAvailible = false;
        burstAvailible = false;
        machinegunAvailible = false;
        airstrikeAvailible = false;
        shieldAvailible = false;
    }
    private void Update()
    {
        if (!onCooldown) StartCoroutine(Shoot());
        if (!shockWaveOnCooldown && shockWaveAvailible) StartCoroutine(LaunchShockWave());
        if (!burstOnCooldown && burstAvailible) StartCoroutine(LaunchBurst());
        if (!machinegunOnCooldown && machinegunAvailible) StartCoroutine(ActivateMachinegun());
        if (!airstrikeOnCooldown && airstrikeAvailible) StartCoroutine(ActivateAirstrike());
        if (!shieldOnCooldown && shieldAvailible) StartCoroutine(ActivateShield());
    }


    //Ability usage
    private IEnumerator Shoot()
    {
        audioSource.PlayOneShot(shootSound);

        onCooldown = true;
        GameObject newBullet = Instantiate(bulletPrefab, weaponTransform.position, Quaternion.identity);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = (new Vector3(mousePosition.x, transform.position.y, mousePosition.z) - transform.position).normalized;
        newBullet.GetComponent<Rigidbody>().AddForce(shootDirection * shootForce, ForceMode.Impulse);

        yield return new WaitForSeconds(cooldownBetweenShots);
        onCooldown = false;
    }
    private IEnumerator LaunchShockWave()
    {
        audioSource.PlayOneShot(shockwaveSound);
        shockWaveOnCooldown = true;
        shockWaveParticleSystem.Play();
        Collider[] targets = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider target in targets)
        {
            if (target.gameObject.CompareTag("Enemy"))
                target.gameObject.GetComponent<Entity>().TakeDamage(shockwavedamage);
        }
        yield return new WaitForSeconds(shockWaveCooldown);
        shockWaveOnCooldown = false;
    }
    private IEnumerator LaunchBurst()
    {
        burstOnCooldown = true;
        for (int i = 0; i < bulletsAmount; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab, weaponTransform.position, Quaternion.identity);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 shootDirection = (new Vector3(mousePosition.x + Random.Range(-spread, spread), transform.position.y, mousePosition.z + Random.Range(-spread, spread)) - transform.position).normalized;
            newBullet.GetComponent<Rigidbody>().AddForce(shootDirection * shootForce, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(burstCooldown);
        burstOnCooldown = false;
    }
    private IEnumerator ActivateMachinegun()
    {
        machinegunOnCooldown = true;
        float normalCooldownBetweenShots = cooldownBetweenShots;
        cooldownBetweenShots = newShotCooldown;
        yield return new WaitForSeconds(machinegunDuration);
        cooldownBetweenShots = normalCooldownBetweenShots;
        yield return new WaitForSeconds(machinegunCooldown);
        machinegunOnCooldown = false;
    }
    private IEnumerator ActivateAirstrike()
    {

        airstrikeOnCooldown = true;
        airstrikePosition.SetActive(true);
        bool mouse0Pressed = false;
        Vector3 mousePosition;

        while (mouse0Pressed == false)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            airstrikePosition.transform.position = new Vector3(mousePosition.x, 0, mousePosition.z); ;

            yield return new WaitForEndOfFrame();
            if (inputManager.m0Pressed) mouse0Pressed = true;
        }
        audioSource.PlayOneShot(airstrikeSound);
        airstrikePosition.SetActive(false);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        airstrikeAreaSphere.transform.position = new Vector3(mousePosition.x, 0, mousePosition.z);
        airstrikeAreaSphere.GetComponent<AirstrikeBehaviour>().damage = airstrikeDamage;
        airstrikeAreaSphere.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        airstrikeAreaSphere.SetActive(false);
        yield return new WaitForSeconds(airstrikeCooldown);
        airstrikeOnCooldown = false;
    }
    private IEnumerator ActivateShield()
    {
        audioSource.PlayOneShot(shieldSound);
        shieldOnCooldown = true;
        shield.GetComponent<ShieldBehaviour>().durability = shieldDurability;
        shield.GetComponent<ShieldBehaviour>().damage = shieldDamage;
        shield.SetActive(true);
        while (shield.activeInHierarchy == true)
        {
            yield return new WaitForEndOfFrame();
            shield.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        yield return new WaitForSeconds(shieldCooldown);
        shieldOnCooldown = false;
    }

    //Ability upgrades
    public void UpgradeShockwave()
    {
        shockwaveLevel++;
        switch (shockwaveLevel)
        {
            case 1:
                shockWaveAvailible = true;
                shockwavedamage = 0.5f;
                shockWaveCooldown = 15;
                uiManager.upgradeText1.text = "Уменьшение перезарядки";
                break;
            case 2:
                shockwavedamage = 0.5f;
                shockWaveCooldown = 12;
                uiManager.upgradeText1.text = "Увеличение урона";
                break;
            case 3:
                shockwavedamage = 0.75f;
                shockWaveCooldown = 12;
                uiManager.upgradeText1.text = "Уменьшение перезарядки";
                break;
            case 4:
                shockwavedamage = 0.75f;
                shockWaveCooldown = 10;
                uiManager.upgradeText1.text = "Уменьшение перезарядки и увеличение урона";
                break;
            case 5:
                shockwavedamage = 0.9f;
                shockWaveCooldown = 8;
                uiManager.upgradeShockwaveButton.GetComponent<Button>().interactable = false;
                uiManager.upgradeText1.text = "МАХ";
                break;
            case > 5:
                break;

        }
    }
    public void UpgradeBurst()
    {
        burstLevel++;
        switch (burstLevel)
        {
            case 1:
                burstAvailible = true;
                bulletsAmount = 7;
                spread = 3;
                burstCooldown = 15;
                uiManager.upgradeText2.text = "Увеличение количества снярядов";
                break;
            case 2:
                bulletsAmount = 12;
                spread = 3;
                burstCooldown = 15;
                uiManager.upgradeText2.text = "Уменьшение перезарядки";
                break;
            case 3:
                bulletsAmount = 12;
                spread = 3;
                burstCooldown = 12;
                uiManager.upgradeText2.text = "Уменьшение разброса";
                break;
            case 4:
                bulletsAmount = 12;
                spread = 2;
                burstCooldown = 12;
                uiManager.upgradeText2.text = "Увеличение количества снарядов и уменьшение перезарядки";
                break;
            case 5:
                bulletsAmount = 15;
                spread = 2;
                burstCooldown = 10;
                uiManager.upgradeText2.text = "МАХ";
                break;
            case > 5:
                break;

        }
    }
    public void UpgradeMachineGun()
    {
        machinegunLevel++;
        switch (machinegunLevel)
        {
            case 1:
                machinegunAvailible = true;
                machinegunCooldown = 15;
                machinegunDuration = 6;
                newShotCooldown = 2;
                uiManager.upgradeText3.text = "Уменьшение перезарядки";
                break;
            case 2:
                machinegunCooldown = 12;
                machinegunDuration = 6;
                newShotCooldown = 2;
                uiManager.upgradeText3.text = "Увеличение длительности";
                break;
            case 3:
                machinegunCooldown = 12;
                machinegunDuration = 10;
                newShotCooldown = 2;
                uiManager.upgradeText3.text = "Увеличение скорости атаки";
                break;
            case 4:
                machinegunCooldown = 12;
                machinegunDuration = 10;
                newShotCooldown = 1.5f;
                uiManager.upgradeText3.text = "Увеличение длительности и снижение перезарядки";
                break;
            case 5:
                machinegunCooldown = 10;
                machinegunDuration = 12;
                newShotCooldown = 1.5f;
                uiManager.upgradeText3.text = "МАХ";
                break;
            case > 5:
                break;

        }
    }
    public void UpgradeAirstrike()
    {
        airstrikeLevel++;
        switch (airstrikeLevel)
        {
            case 1:
                airstrikeAvailible = true;
                airstrikeCooldown = 15;
                airstrikeDamage = 1;
                uiManager.upgradeText4.text = "Уменьшение перезарядки";
                break;
            case 2:
                airstrikeCooldown = 12;
                airstrikeDamage = 1;
                uiManager.upgradeText4.text = "Увеличение урона";
                break;
            case 3:
                airstrikeCooldown = 12;
                airstrikeDamage = 1.5f;
                uiManager.upgradeText4.text = "Уменьшение перезарядки";
                break;
            case 4:
                airstrikeCooldown = 10;
                airstrikeDamage = 1.5f;
                uiManager.upgradeText4.text = "Увеличение урона";
                break;
            case 5:
                airstrikeCooldown = 10;
                airstrikeDamage = 1.75f;
                uiManager.upgradeText4.text = "МАХ";
                break;
            case > 5:
                break;

        }
    }
    public void UpgradeShield()
    {
        shieldLevel++;
        switch (shieldLevel)
        {
            case 1:
                shieldAvailible = true;
                shieldCooldown = 15;
                shieldDamage = 1;
                shieldDurability = 5;
                uiManager.upgradeText5.text = "Увеличение прочности";
                break;
            case 2:
                shieldCooldown = 15;
                shieldDamage = 1;
                shieldDurability = 8;
                uiManager.upgradeText5.text = "Уменьшение перезарядки";
                break;
            case 3:
                shieldCooldown = 12;
                shieldDamage = 1;
                shieldDurability = 8;
                uiManager.upgradeText5.text = "Увеличение прочности";
                break;
            case 4:
                shieldCooldown = 12;
                shieldDamage = 1;
                shieldDurability = 12;
                uiManager.upgradeText5.text = "Уменьшение перезарядки, увеличение урона и прочности";
                break;
            case 5:
                shieldCooldown = 10;
                shieldDamage = 1.5f;
                shieldDurability = 15;
                uiManager.upgradeText5.text = "МАХ";
                break;
            case > 5:
                break;

        }
    }
}
