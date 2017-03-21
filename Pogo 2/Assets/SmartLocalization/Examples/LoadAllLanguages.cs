//
//  LoadAllLanguages.cs
//
//
// Written by Niklas Borglund and Jakob Hillerström
//

using System.Collections.Generic;
using UnityEngine;

namespace SmartLocalization.Examples
{
    public class LoadAllLanguages : MonoBehaviour 
    {
	    private List<string> _currentLanguageKeys;
	    private List<SmartCultureInfo> _availableLanguages;
	    private LanguageManager _languageManager;
	    private Vector2 _valuesScrollPosition = Vector2.zero;
	    private Vector2 _languagesScrollPosition = Vector2.zero;

	    void Start () 
	    {
		    _languageManager = LanguageManager.Instance;
		
		    SmartCultureInfo deviceCulture = _languageManager.GetDeviceCultureIfSupported();
		    if(deviceCulture != null)
		    {
			    _languageManager.ChangeLanguage(deviceCulture);	
		    }
		    else
		    {
			    Debug.Log("The device language is not available in the current application. Loading default."); 
		    }
		
		    if(_languageManager.NumberOfSupportedLanguages > 0)
		    {
			    _currentLanguageKeys = _languageManager.GetAllKeys();
			    _availableLanguages = _languageManager.GetSupportedLanguages();
		    }
		    else
		    {
			    Debug.LogError("No languages are created!, Open the Smart Localization plugin at Window->Smart Localization and create your language!");
		    }

		    LanguageManager.Instance.OnChangeLanguage += OnLanguageChanged;
	    }

	    void OnDestroy()
	    {
		    if(LanguageManager.HasInstance)
		    {
			    LanguageManager.Instance.OnChangeLanguage -= OnLanguageChanged;
		    }
	    }

	    void OnLanguageChanged(LanguageManager languageManager)
	    {
		    _currentLanguageKeys = languageManager.GetAllKeys();
	    }
	
	    void OnGui() 
	    {
		    if(_languageManager.NumberOfSupportedLanguages > 0)
		    {
			    if(_languageManager.CurrentlyLoadedCulture != null)
			    {
				    GUILayout.Label("Current Language:" + _languageManager.CurrentlyLoadedCulture.ToString());
			    }
			
			    GUILayout.BeginHorizontal();
			    GUILayout.Label("Keys:", GUILayout.Width(460));
			    GUILayout.Label("Values:", GUILayout.Width(460));
			    GUILayout.EndHorizontal();
			
			    _valuesScrollPosition = GUILayout.BeginScrollView(_valuesScrollPosition);
			    foreach(var languageKey in _currentLanguageKeys)
			    {
				    GUILayout.BeginHorizontal();
				    GUILayout.Label(languageKey, GUILayout.Width(460));
				    GUILayout.Label(_languageManager.GetTextValue(languageKey), GUILayout.Width(460));
				    GUILayout.EndHorizontal();
			    }
			    GUILayout.EndScrollView();
			
			    _languagesScrollPosition = GUILayout.BeginScrollView (_languagesScrollPosition);
			    foreach(SmartCultureInfo language in _availableLanguages)
			    {
				    if(GUILayout.Button(language.nativeName, GUILayout.Width(960)))
				    {
					    _languageManager.ChangeLanguage(language);
				    }
			    }

			    GUILayout.EndScrollView();
		    }
	    }
    }
}//namespace SmartLocalization
