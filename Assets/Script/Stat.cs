using System.Collections.Generic;
using UnityEngine;

public enum StatModifierType
{
    Flat,    // ����ġ ���� (��: +5, -3)
    Percent  // �ۼ�Ʈ ���� (��: +10%, -20%)
}

public class StatModifier
{
    public float Value { get; private set; }
    public StatModifierType Type { get; private set; }
    public Seed Attacker { get; private set; }
    public float Duration { get; private set; }  // 0�̸� ������

    public StatModifier(float value, StatModifierType type, Seed attacker, float duration = 0)
    {
        Value = value;
        Type = type;
        Attacker = attacker;
        Duration = duration;
    }
}

public class Stat
{
    public float BaseValue { get; private set; }
    private List<StatModifier> modifiers = new List<StatModifier>();

    public Stat(float baseValue)
    {
        BaseValue = baseValue;
    }

    public float FinalValue
    {
        get
        {
            float finalValue = BaseValue;
            float percentSum = 0;
            HashSet<Seed> attackers = new HashSet<Seed>();

            // Flat modifier ����
            foreach (var mod in modifiers)
            {
                if (!attackers.Contains(mod.Attacker))
                {
                    if (mod.Type == StatModifierType.Flat)
                        finalValue += mod.Value;
                    else if (mod.Type == StatModifierType.Percent)
                        percentSum += mod.Value;
                    attackers.Add(mod.Attacker);
                }
            }

            // �ۼ�Ʈ ������ �⺻���� ����
            finalValue += BaseValue * (percentSum / 100f);

            return finalValue;
        }
    }

    public int FinalValueInt => Mathf.RoundToInt(FinalValue);

    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(StatModifier modifier)
    {
        modifiers.Remove(modifier);
    }
}
