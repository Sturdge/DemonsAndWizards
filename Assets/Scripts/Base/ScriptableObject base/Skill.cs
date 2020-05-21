using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Attacks/Skill")]
public class Skill : ScriptableObject
{
    [Header("Skill Attributes")]
    [SerializeField]
    private int _manaCost = 0;
    [SerializeField]
    private int _baseDamage = 0;
    [SerializeField]
    private int cooldown = 0;
    [SerializeField]
    private PlayerProjectile objectToSpawn = null;

    private float cooldownTimer;
    private bool isOnCooldown;
    private PlayerController parent;

    public int ManaCost => _manaCost;
    public int BaseDamage => _baseDamage;

    public void Initialisation(PlayerController controller)
    {
        isOnCooldown = false;
        cooldownTimer = 0;
        parent = controller;
    }

    public void DoSkill()
    {
        if (!isOnCooldown)
        {
            Instantiate(objectToSpawn, parent.SpellPoint.position, parent.SpellPoint.rotation);
            isOnCooldown = true;
        }
    }

    public void UpdateCooldown(float deltatime)
    {
        if (isOnCooldown)
        {
            if (cooldownTimer < cooldown)
            {
                cooldownTimer += deltatime;
                CheckTimerEnd();
            }
        }
    }

    private void CheckTimerEnd()
    {
        if (cooldownTimer >= cooldown)
        {
            cooldownTimer = 0;
            isOnCooldown = false;
        }
    }
}