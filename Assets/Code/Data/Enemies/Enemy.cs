using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 4)]
public class Enemy : ScriptableObject
{
    [SerializeField]
    private string enemyName;
    [SerializeField]
    private int enemyHitpoints;
    [SerializeField]
    private int enemyCurrentHitpoints;
    [SerializeField]
    private Spell[] enemySpells;
    [SerializeField]
    private int enemySpellCooldown;
    [SerializeField]
    private float enemyMovementSpeed;
    [SerializeField]
    private Item[] enemyInventory;
    [SerializeField]
    private int enemyDamage;
    [SerializeField]
    private bool enemyIsStunned;

    public string EnemyName
    {
        get => enemyName;
        set => enemyName = value;
    }
    
    public int EnemyHitpoints
    {
        get => enemyHitpoints;
        set => enemyHitpoints = value;
    }
    
    public int EnemyCurrentHitpoints
    {
        get => enemyCurrentHitpoints;
        set => enemyCurrentHitpoints = value;
    }
    
    public Spell[] EnemySpells
    {
        get => enemySpells;
        set => enemySpells = value;
    }
    
    public int EnemySpellCooldown
    {
        get => enemySpellCooldown;
        set => enemySpellCooldown = value;
    }
    
    public float EnemyMovementSpeed
    {
        get => enemyMovementSpeed;
        set => enemyMovementSpeed = value;
    }
    
    public Item[] EnemyInventory
    {
        get => enemyInventory;
        set => enemyInventory = value;
    }

    public int EnemyDamage
    {
        get => enemyDamage;
        set => enemyDamage = value;
    }

    public bool EnemyIsStunned
    {
        get => enemyIsStunned;
        set => enemyIsStunned = value;
    }
}