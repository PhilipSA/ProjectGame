using SmartLocalization.Editor;
using UnityEditor;
using UnityEngine;

namespace SmartLocalization.Scripts.uGUI.Editor{
    [CustomEditor(typeof(LocalizedText))]
public class LocalizedTextInspector : UnityEditor.Editor 
{
	private string selectedKey = null;
	
	void Awake()
	{
		LocalizedText textObject = ((LocalizedText)target);
		if(textObject != null)
		{
			selectedKey = textObject.LocalizedKey;
		}
	}
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		
		selectedKey = LocalizedKeySelector.SelectKeyGUI(selectedKey, true, LocalizedObjectType.STRING);
		
		if(!Application.isPlaying && GUILayout.Button("Use Key", GUILayout.Width(70)))
		{
			LocalizedText textObject = ((LocalizedText)target);
			textObject.LocalizedKey = selectedKey;
		}
	}
	
}
}