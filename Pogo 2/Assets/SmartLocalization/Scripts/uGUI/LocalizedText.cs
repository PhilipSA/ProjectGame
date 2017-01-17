using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SmartLocalization.Scripts.uGUI
{
    [RequireComponent (typeof (Text))]
public class LocalizedText : MonoBehaviour 
{
	public string LocalizedKey = "INSERT_KEY_HERE";
	Text _textObject;
	
	void Start () 
	{
		_textObject = this.GetComponent<Text>();
	
		//Subscribe to the change language event
		LanguageManager languageManager = LanguageManager.Instance;
		languageManager.OnChangeLanguage += OnChangeLanguage;
		
		//Run the method one first time
		OnChangeLanguage(languageManager);
	}
	
	void OnDestroy()
	{
		if(LanguageManager.HasInstance)
		{
			LanguageManager.Instance.OnChangeLanguage -= OnChangeLanguage;
		}
	}
	
	void OnChangeLanguage(LanguageManager languageManager)
	{
		_textObject.text = LanguageManager.Instance.GetTextValue(LocalizedKey);
	}
}
}