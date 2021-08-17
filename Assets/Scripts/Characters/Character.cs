using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{

    public enum Type
    {
        kPlayer,
        kEnemy
    }

    [SerializeField]
    protected Type m_type;

    [SerializeField]
    private Sprite m_sprite;

    [SerializeField]
    private float m_currentHealth;
    [SerializeField]
    private float m_health;

    protected int m_attack;
    protected int m_armor;
    protected int m_luck;



    [SerializeField]
    protected int m_energy = 3;

    [SerializeField]
    protected int m_currentEnergy = 3;


    [SerializeField]
    protected int m_drawPower = 5;
    protected int m_actionPoint;

    [SerializeField]
    protected Hand m_hand;

    [SerializeField]
    protected Deck m_deck;

    [SerializeField]
    protected DrawPile m_drawPile;
    [SerializeField]
    protected DiscardPile m_discardPile;

    protected Character m_opponent;
    [SerializeField]
    protected TextMeshProUGUI m_energyText;

    [SerializeField]
    private Transform m_tempZone;

    public Transform TempZone { get => m_tempZone; set => m_tempZone = value; }


    #region Getter & Setter


    public virtual int Energy
    {
        get
        {
            return m_energy;
        }

        set
        {
            m_energy = value;

        }
    }

    public Hand Hand
    {
        get
        {
            return m_hand;
        }

        set
        {
            m_hand = value;
        }
    }

    public DrawPile DrawPile
    {
        get
        {
            return m_drawPile;
        }

    }

    public DiscardPile DiscardPile
    {
        get
        {
            return m_discardPile;
        }

    }

    public virtual int CurrentEnergy
    {
        get
        {
            return m_currentEnergy;
        }

        set
        {
            m_currentEnergy = value;

            if (m_energyText)
                m_energyText.text = m_currentEnergy + "/" + m_energy;
        }
    }

    #endregion



    [SerializeField]
    private Text m_characterHealthText;

    [SerializeField]
    private Image m_characterHealthFill;

    [SerializeField]
    private SpriteRenderer m_spriteRenderer;

    private List<Buff> m_buffList = new List<Buff>();

    [SerializeField]
    private GameObject m_buffs;

    [SerializeField]
    private StatusEffect m_statusEffectPrefab;


    private Vector2 m_startPos;


    public Type type { get => m_type; set => m_type = value; }
    public int Attack { get => m_attack; set => m_attack = value; }
    public int Armor { get => m_armor; set => m_armor = value; }
    public int Luck { get => m_luck; set => m_luck = value; }
    public List<Buff> BuffList { get => m_buffList; set => m_buffList = value; }
    public float CurrentHealth { get => m_currentHealth; set => m_currentHealth = value; }
    public float Health { get => m_health; set => m_health = value; }

    protected virtual void Awake()
    {
        m_spriteRenderer.sprite = m_sprite;

        m_startPos = transform.GetChild(0).position;

        m_characterHealthText.text = m_currentHealth.ToString() + "/" + Health.ToString();
        m_characterHealthFill.fillAmount = m_currentHealth / Health;
    }

    public void TakeDamage(int damage, float hit = 0, bool dodgeAble = true)
    {
        int rand = UnityEngine.Random.Range(0, 100);



        m_currentHealth -= damage;

        m_currentHealth = m_currentHealth < 0 ? 0 : m_currentHealth;

        m_characterHealthText.text = m_currentHealth.ToString() + "/" + Health.ToString();
        m_characterHealthFill.fillAmount = m_currentHealth / Health;

        PopUp(damage.ToString(), Color.red);

        if (m_currentHealth <= 0)
        {
            Die();
        }

    }

    protected virtual void Die()
    {

    }



    public void Heal(int amount)
    {
        m_currentHealth += amount;

        m_currentHealth = m_currentHealth > Health ? Health : m_currentHealth;

        m_characterHealthText.text = m_currentHealth.ToString() + "/" + Health.ToString();
        m_characterHealthFill.fillAmount = m_currentHealth / Health;

        PopUp(amount.ToString(), Color.green);
    }


    private void PopUp(string message, Color color)
    {
        Vector2 position = transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(0.5f, 1.0f), 0);
        GameObject obj = Instantiate(Resources.Load("DamagePopUp")) as GameObject;
        obj.GetComponent<Text>().color = color;
        obj.transform.position = position;
        obj.GetComponent<Text>().text = message;
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.up * 2f - Vector2.right * 0.5f;
        Destroy(obj, 0.6f);

    }

    public abstract IEnumerator Turn();


    public virtual IEnumerator StartTurnCoroutine()
    {

        EvaluateBuff();

        CombatManager.instance.CheckEnemyList();

        m_currentEnergy = m_energy;


        yield return null;

    }

    public IEnumerator Draw()
    {
        yield return Hand.DrawCoroutine(m_drawPower);
    }

    public virtual IEnumerator Initialize()
    {
        yield return new WaitUntil(() => m_drawPile.PopulateFromDeck(m_deck));
    }



    public virtual IEnumerator EndTurnCoroutine()
    {
        yield return null;
    }


    public void AddBuff(Buff buff)
    {

        for (int i = 0; i < m_buffList.Count; i++)
        {

            if (m_buffList[i].name == buff.name)
            {
                m_buffList[i].Duration += buff.Duration;
                m_buffs.transform.GetChild(i).GetComponent<StatusEffect>().DurationText.text =
                m_buffList[i].Duration.ToString();

                return;
            }
        }



        buff.AddEffect(this);
        m_buffList.Add(buff);

        StatusEffect status = Instantiate(m_statusEffectPrefab, m_buffs.transform);
        status.Image.sprite = buff.Sprite;
        status.DurationText.text = buff.Duration.ToString();

    }

    private void EvaluateBuff()
    {
        List<Buff> m_toBeRemovedList = new List<Buff>();
        for (int i = 0; i < m_buffList.Count; i++)
        {
            m_buffList[i].Execute(this);
            m_buffs.transform.GetChild(i).GetComponent<StatusEffect>().DurationText.text =
            m_buffList[i].Duration.ToString();

            if (m_buffList[i].Duration == 0)
            {
                Destroy(m_buffs.transform.GetChild(i).gameObject);
                m_toBeRemovedList.Add(m_buffList[i]);
            }
        }

        foreach (var buff in m_toBeRemovedList)
        {
            buff.RemoveEffect(this);
            m_buffList.Remove(buff);
        }
    }

    public void PlayAttackAnim(float timer)
    {
        StartCoroutine(PlayAttackCoroutine(timer));
    }

    IEnumerator PlayAttackCoroutine(float timer)
    {
        var sprite = transform.GetChild(0);

        float time = 0;

        Vector2 direction = (new Vector2(0, 0) - new Vector2(m_startPos.x, 0)).normalized;

        Vector2 endPos = m_startPos + direction;

        while (time < timer / 2)
        {
            yield return new WaitForEndOfFrame();

            time += Time.deltaTime;
            sprite.position = Vector2.Lerp(sprite.position, endPos, time / timer / 2);

        }
        time = 0;

        while (time < timer / 2)
        {
            yield return new WaitForEndOfFrame();

            time += Time.deltaTime;
            sprite.position = Vector2.Lerp(endPos, m_startPos, time / timer / 2);

        }
        sprite.position = m_startPos;

    }

    public virtual IEnumerator PlayCardEffect(Card card)
    {
        yield return null;


    }

    public void PostPlayCard()
    {
        CombatManager.instance.CheckEnemyList();
    }
}
