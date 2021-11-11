using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PlayerMoveAuto)), CanEditMultipleObjects]
public class PlayerAutoMoveEditor : Editor
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		PlayerMoveAuto script = target as PlayerMoveAuto;

		CustomEditorList.Show(serializedObject.FindProperty("wayPoints"), EditorListOption.ListSize | EditorListOption.ListLabel | EditorListOption.Buttons);
		CustomEditorList.Show(serializedObject.FindProperty("rotatePoints"), EditorListOption.ListSize | EditorListOption.ListLabel | EditorListOption.Buttons);
		CustomEditorList.Show(serializedObject.FindProperty("_bossEvnet02"));
		CustomEditorList.Show(serializedObject.FindProperty("bossRoomWall"));

		serializedObject.ApplyModifiedProperties();
	}
}