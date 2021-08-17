using UnityEngine;

[System.Serializable]
public class CardUpgrade
{
    [SerializeField]
    int cost;


    public int Cost { get => cost; set => cost = value; }

}
