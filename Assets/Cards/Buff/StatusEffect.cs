using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusEffect : MonoBehaviour
{
    [SerializeField]
    private Text m_durationText;
    [SerializeField]
    private Image m_image;

    public Text DurationText
    {
        get
        {
            return m_durationText;
        }

        set
        {
            m_durationText = value;
        }
    }
    public Image Image { get => m_image; set => m_image = value; }
}
