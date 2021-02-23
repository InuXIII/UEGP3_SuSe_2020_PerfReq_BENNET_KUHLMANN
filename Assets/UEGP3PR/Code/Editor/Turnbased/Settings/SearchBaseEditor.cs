using UEGP3PR.Code.Runtime.Turnbased;
using UnityEditor;
using UnityEngine;

namespace UEGP3PR.Code.Editor.Turnbased.Settings
{
	[CustomEditor(typeof(SearchBase), true)]
	public class SearchBaseEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Perform Search"))
			{
				(target as SearchBase)?.Search();
			}
		}
	}
}