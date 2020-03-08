using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CardComponent), true)]
public class CardComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CardComponent cardComponent = (CardComponent)target;
        if (GUILayout.Button("Add HP Condition"))
        {
            cardComponent.AddCondition(ConditionEnum.kHP);
        }
 
        if (GUILayout.Button("Add Buff Condition"))
        {
            cardComponent.AddCondition(ConditionEnum.kBuff);
        }
    }
}

