using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class Card : MyObject
{

    public enum Type
    {
        kOffense,
        kBuff,
        kUtility,
    }

    [SerializeField]
    private Type m_type;


    [Header("--Card UI--")]
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private TextMeshProUGUI m_descriptionText;
    [SerializeField]
    private TextMeshProUGUI m_nameText;
    [SerializeField]
    private TextMeshProUGUI m_energyCostText;

    [SerializeField]
    private SpriteRenderer m_energySlot;
    [SerializeField]
    private SpriteRenderer m_template;

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

    [SerializeField]
    private GameObject m_uiDesc;


    protected int m_siblingIndex;



    [Header("--Card Component--")]
    [SerializeField]
    private CardComponent[] m_components;

    [SerializeField]
    private TargetArrow arrow;

    private int level = 0;

    public int EnergyCost
    {
        get
        {
            return m_energyCost;
        }

        set
        {
            m_energyCost = value;
        }
    }


    public SpriteRenderer SpriteRenderer { get => m_spriteRenderer; set => m_spriteRenderer = value; }
    public TextMeshProUGUI DescriptionText { get => m_descriptionText; set => m_descriptionText = value; }
    public TextMeshProUGUI NameText { get => m_nameText; set => m_nameText = value; }
    public TextMeshProUGUI EnergyCostText { get => m_energyCostText; set => m_energyCostText = value; }
    public int Level { get => level; set => level = value; }
    public UnityAction DiscardAction { get => m_discardAction; set => m_discardAction = value; }
    public Sprite Image { get => m_image; set => m_image = value; }

    private UnityAction m_discardAction;

    [SerializeField]
    private CardUpgrade m_onUpgrade;

    void Start()
    {

        m_spriteRenderer.sprite = m_image;

        m_nameText.text = name.Replace("(Clone)", "").Trim();

        m_energyCostText.text = m_energyCost.ToString();

        m_components = GetComponents<CardComponent>();

        foreach (var component in m_components)
        {
            component.Init(this);
        }

        UpdateDesc();

    }

    /// <summary>
    /// This section handles card user interaction
    /// </summary>
    #region Interaction
    protected void OnMouseEnter()
    {
        if (transform.parent != null && m_currentZone.GetComponent<Hand>().ChoosenCard == null)
        {
            m_siblingIndex = transform.GetSiblingIndex();

            transform.localScale *= 1.7f;

            transform.localPosition -= Vector3.forward * 5;

            m_uiDesc.SetActive(true);


            transform.localPosition += Vector3.up * 10f;


        }

    }

    protected void OnMouseExit()
    {

        if (transform.parent != null && m_currentZone.GetComponent<Hand>().ChoosenCard == null)
        {
            m_uiDesc.SetActive(false);

            transform.localScale = new Vector3(1, 1, 1);

            transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
            m_currentZone.Arrange();

        }

    }



    protected void OnMouseDrag()
    {
        if ((m_currentZone.GetComponent<Hand>().ChoosenCard == null || m_currentZone.GetComponent<Hand>().ChoosenCard == this) && m_energyCost <= m_owner.CurrentEnergy)
        {
            transform.SetParent(null);

            transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);



            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = pos;


            transform.position = new Vector3(transform.position.x, transform.position.y, -5);

            m_currentZone.GetComponent<Hand>().ChoosenCard = this;


        }


    }

    protected void OnMouseUp()
    {
        if (InPlayArea())
        {
            transform.localScale = new Vector3(0.135f, 0.135f, 0.135f);
            StartCoroutine(PrePlay());

        }
        else
        {
            ReturnToHand();
        }
        m_currentZone.transform.GetComponent<Hand>().ChoosenCard = null;

    }

    #endregion


    /// <summary>
    /// Helper functions
    /// </summary>
    #region Helper
    private void UpdateDesc()
    {
        m_descriptionText.text = m_description;
        foreach (var component in m_components)
        {
            m_descriptionText.text = m_descriptionText.text + component.GetDescription() + "\n";
        }

    }

    public void Discard()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        m_owner.DiscardPile.Add(this, 0.60f);
    }

    public void PutOnTop()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        m_owner.DrawPile.Add(this, 0.60f);
    }

    bool InPlayArea()
    {
        if (transform.position.x > -6 && transform.position.x < 6 && transform.position.y < 2.0f && transform.position.y > -2.5f)
        {
            return true;
        }

        return false;
    }

    private void ReturnToHand()
    {
        transform.SetParent(m_currentZone.transform);

        transform.SetSiblingIndex(m_siblingIndex);

        transform.localScale = new Vector3(1f, 1f, 1f);

        transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
        m_currentZone.Arrange();
    }
    #endregion


    /// <summary>
    /// This section handle card execution
    /// </summary>
    private bool play;

    private IEnumerator PrePlay()
    {
        play = true;
        foreach (CardComponent component in m_components)
        {
            if (component.TargetType == CardComponent.TargetEnum.kSingle)
            {
                play = false;
                yield return SelectTarget(component);
                break;
            }
        }

        if (play)
        {
            yield return Play();
            play = false;
        }
    }

    private IEnumerator SelectTarget(CardComponent component)
    {

        Player player = (Player)Owner;
        transform.position = player.TempZone.position;

        arrow.gameObject.SetActive(true);

        bool hasTarget = false;

        while (true)
        {

            if (arrow.Target != null && !hasTarget)
            {
                SetTarget(arrow.Target);
                UpdateDesc();
                hasTarget = true;
            }

            if (arrow.Target == null && hasTarget == true)
            {

                ClearTarget();

                UpdateDesc();
                hasTarget = false;
            }



            if (Input.GetMouseButton(1))
            {
                component.Target.Clear();
                arrow.gameObject.SetActive(false);
                play = false;
                ReturnToHand();

                break;
            }

            if (Input.GetMouseButton(0))
            {
                if (arrow.Target != null)
                {
                    play = true;
                    arrow.gameObject.SetActive(false);

                    break;
                }
                else
                {
                    arrow.gameObject.SetActive(false);
                    play = false;
                    ReturnToHand();

                    break;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void SetTarget(Character target)
    {
        foreach (CardComponent component in m_components)
        {
            if (component.TargetType == CardComponent.TargetEnum.kSingle)
            {
                component.Target.Add(target);

            }
        }
    }

    private void ClearTarget()
    {
        foreach (CardComponent component in m_components)
        {
            if (component.TargetType == CardComponent.TargetEnum.kSingle)
            {
                component.Target.Clear();
                break;
            }
        }
    }

    public virtual IEnumerator Play()
    {

        m_owner.CurrentEnergy -= m_energyCost;



        StartCoroutine(ExecuteComponents());

        yield return m_owner.PlayCardEffect(this);

        Discard();



    }

    private IEnumerator ExecuteComponents()
    {
        foreach (CardComponent component in m_components)
        {
            for (int i = 0; i < component.ExecuteAmount; i++)
            {
                yield return new WaitForSeconds(0.15f);
                component.Execute();

            }
        }
        ClearTarget();

        m_owner.PostPlayCard();
    }

    public void Upgrade()
    {

        m_energyCost += m_onUpgrade.Cost;
        foreach (CardComponent component in m_components)
        {
            for (int i = 0; i < component.ExecuteAmount; i++)
            {

                component.Upgrade();

            }
        }

    }



}
