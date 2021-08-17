using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public abstract class Buff:ScriptableObject
{
    private int m_duration = 0;

    [SerializeField]
    private Sprite m_sprite;



    public int Duration { get => m_duration; set => m_duration = value; }
    public Sprite Sprite { get => m_sprite; set => m_sprite = value; }

    public virtual void Execute(Character owner)
    {
        m_duration--;
    }


    public abstract void AddEffect(Character owner);


    public abstract void RemoveEffect(Character owner);
 
}


