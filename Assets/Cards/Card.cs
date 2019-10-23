using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MyObject {

    public enum Type
    {
        KActive,
        kPowerUp,
    }



    [Header("--Card UI--")]
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private Text m_descriptionText;
    [SerializeField]
    private Text m_nameText;
    [SerializeField]
    private Text m_energyCostText;

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

    [TextArea(3, 10)]
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
    public Text DescriptionText { get => m_descriptionText; set => m_descriptionText = value; }
    public Text NameText { get => m_nameText; set => m_nameText = value; }
    public Text EnergyCostText { get => m_energyCostText; set => m_energyCostText = value; }
    public int Level { get => level; set => level = value; }

    void Start()
    {
        name = m_name;
        m_spriteRenderer.sprite = m_image;

        m_nameText.text = m_name;

        m_energyCostText.text = m_energyCost.ToString();

        m_components = GetComponents<CardComponent>();
        m_descriptionText.text = "";
        foreach (CardComponent component in m_components)
        {
            component.Init(this);
            m_descriptionText.text += component.GetDescription() + "\n";
        }
    }

    protected void OnMouseEnter()
    {
        if (transform.parent != null && m_currentZone.GetComponent<Hand>().ChoosenCard == null)
        {
            m_siblingIndex = transform.GetSiblingIndex();

            transform.localScale *= 1.4f;

            transform.localPosition -= Vector3.forward * 5;

            m_uiDesc.SetActive(true);


            transform.localPosition += Vector3.up * 7f;

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

            transform.localScale = new Vector3(0.135f, 0.135f, 0.135f);



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

    private void ReturnToHand()
    {
        transform.SetParent(m_currentZone.transform);

        transform.SetSiblingIndex(m_siblingIndex);

        transform.localScale = new Vector3(1f, 1f, 1f);

        transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
        m_currentZone.Arrange();
    }

   
    private bool play;

    private IEnumerator PrePlay()
    {
        play = true;
        foreach (CardComponent component in m_components)
        {
            if(component.TargetType == CardComponent.TargetEnum.kSingle)
            {
                play = false;
                yield return SelectTarget(component);
            }
        }

        if(play)
        {
            yield return Play();
            play = false;
        }
    }

    private IEnumerator SelectTarget(CardComponent component)
    {
        Character target = null;

        Player player = (Player)Owner;
        transform.position = player.TempZone.position;

        arrow.gameObject.SetActive(true);
        while(true)
        {
            target = arrow.Target;

            if(Input.GetMouseButton(1))
            {
                component.Target.Clear();
                arrow.gameObject.SetActive(false);
                play = false;
                ReturnToHand();

                break;
            }

            if (Input.GetMouseButton(0))
            {
                if(target != null)
                {
                    play = true;
                    component.Target.Add(target);
                    arrow.gameObject.SetActive(false);
               
                    break;
                }       
       
            }

            yield return new WaitForEndOfFrame();
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
   }

    public void Discard()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        m_owner.DiscardPile.Add(this, 0.60f);
    }

    bool InPlayArea()
    {
        if(transform.position.x > -6 && transform.position.x < 6  && transform.position.y < 2.0f && transform.position.y > -2.5f)
        {
            return true;
        }

        return false;
    }
    
 
}
