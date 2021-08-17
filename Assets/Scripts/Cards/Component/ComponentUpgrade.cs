using UnityEngine;

[System.Serializable]
public class ComponentUpgrade
{
    [SerializeField]
    int amount;

    [SerializeField]
    int execute_amount;

    public int Amount { get => amount; set => amount = value; }
    public int Execute_amount { get => execute_amount; set => execute_amount = value; }
}
