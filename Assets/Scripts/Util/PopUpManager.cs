using UnityEngine;

public class PopUpManager : MonoBehaviour
{

    private static PopUpManager instance;

    public static PopUpManager Instance { get => instance; set => instance = value; }


    [SerializeField]
    private GameObject rewardPopUp;

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


    public void ShowRewardPopup()
    {
        rewardPopUp.SetActive(true);
    }
}
