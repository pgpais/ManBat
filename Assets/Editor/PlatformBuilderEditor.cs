using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlatformBuilder))]
public class PlatformBuilderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		PlatformBuilder myScript = (PlatformBuilder)target;
		if(GUILayout.Button("Build Object"))
		{
			myScript.BuildObject();
		}
	}
}