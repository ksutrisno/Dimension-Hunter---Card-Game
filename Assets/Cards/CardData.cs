using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject
{
    [Header("--Card Info--")]
    [Space(10)]
    [SerializeField]
    private string m_name;
    [SerializeField]
    private Sprite m_image;
    [SerializeField]
    private string m_description;
    [SerializeField]
    private int m_energyCost;

}
