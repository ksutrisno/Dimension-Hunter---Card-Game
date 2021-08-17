using UnityEngine;

public class ZoneView : MonoBehaviour
{
    private static ZoneView instance;

    public static ZoneView Instance { get => instance; set => instance = value; }

    [SerializeField]
    private Transform content;

    [SerializeField]
    private CardView m_cardView;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {

    }

    // Start is called before the first frame update
    public void Show(Transform cards)
    {
        transform.GetChild(0).gameObject.SetActive(true);


        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in cards)
        {
            var card = child.GetComponent<Card>();

            var cardView = Instantiate(m_cardView, content.position, content.rotation);

            cardView.transform.SetParent(content);

            cardView.transform.localScale = new Vector3(1, 1, 1);

            cardView.Image.sprite = card.Image;
            cardView.DescriptionText.text = card.DescriptionText.text;
            cardView.EnergyCostText.text = card.EnergyCostText.text;
            cardView.NameText.text = card.NameText.text;
        }
    }

}
