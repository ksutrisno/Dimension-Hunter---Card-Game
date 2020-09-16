using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CardComponent), true)]
public class CardComponentEditor : Editor
{
    [SerializeField]
    private ConditionEnum condition;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CardComponent cardComponent = (CardComponent)target;

        GUILayout.Space(20);
        condition = (ConditionEnum)EditorGUILayout.EnumPopup("Add Condition:", condition);

        if (GUILayout.Button("Create Condition"))
        {
            cardComponent.AddCondition(condition);
        }
 
    }
}

