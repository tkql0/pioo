using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable Object/Character Data", order = int.MaxValue)]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private string _characterName;
    public string CharacterName { get { return _characterName; } }

    [SerializeField]
    private int _hp;
    public int MaxHp { get { return _hp; } }

    [SerializeField]
    private int _damage;
    public int damage { get { return _damage; } }

    [SerializeField]
    private int _criticalDamage;
    public int criticalDamage { get { return _criticalDamage; } }

    [SerializeField]
    private float _sightRange;
    public float SightRange { get { return _sightRange; } }

    [SerializeField]
    private float _maxDelayTime;
    public float MaxDelayTime { get { return _maxDelayTime; } }

    [SerializeField]
    private float _minDelayTime;
    public float MinDelayTime { get { return _minDelayTime; } }
}

