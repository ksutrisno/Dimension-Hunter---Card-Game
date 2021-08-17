using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI m_descriptionText;
    [SerializeField]
    private TextMeshProUGUI m_nameText;
    [SerializeField]
    private TextMeshProUGUI m_energyCostText;


    [SerializeField]
    private Image m_frontTemplate;

    [SerializeField]
    private Image m_image;

    public TextMeshProUGUI DescriptionText { get => m_descriptionText; set => m_descriptionText = value; }
    public TextMeshProUGUI NameText { get => m_nameText; set => m_nameText = value; }
    public TextMeshProUGUI EnergyCostText { get => m_energyCostText; set => m_energyCostText = value; }
    public Image FrontTemplate { get => m_frontTemplate; set => m_frontTemplate = value; }
    public Image Image { get => m_image; set => m_image = value; }
}
