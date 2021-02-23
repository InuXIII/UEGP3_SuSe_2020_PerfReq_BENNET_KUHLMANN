using UnityEditor;
using UnityEngine;
using Grid = UEGP3PR.Code.Runtime.Turnbased.Grid;

namespace UEGP3PR.Code.Editor.Turnbased.Settings
{
	[CustomEditor(typeof(Grid), true)]
	public class GridEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Save"))
			{
				(target as Grid)?.SaveGrid(1);
			}
			
			if (GUILayout.Button("Load"))
			{
				(target as Grid)?.LoadGrid(1);
			}
		}
	}
}