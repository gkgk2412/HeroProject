using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractUI)), CanEditMultipleObjects]
public class InteractEditor : Editor
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		InteractUI script = target as InteractUI;

		CustomEditorList.Show(serializedObject.FindProperty("objType"));
		CustomEditorList.Show(serializedObject.FindProperty("Player"));

		if (script.objType == 0)
		{
			CustomEditorList.Show(serializedObject.FindProperty("text"));
			CustomEditorList.Show(serializedObject.FindProperty("_board"));
			CustomEditorList.Show(serializedObject.FindProperty("InteractiveLength"));
			CustomEditorList.Show(serializedObject.FindProperty("minLength"));
		}

		if (script.objType == 1)
		{
			CustomEditorList.Show(serializedObject.FindProperty("text"));
			CustomEditorList.Show(serializedObject.FindProperty("InteractiveLength"));
			CustomEditorList.Show(serializedObject.FindProperty("minLength"));
			CustomEditorList.Show(serializedObject.FindProperty("_mainQEvent"));
		}

		serializedObject.ApplyModifiedProperties();
	}
}