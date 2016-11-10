using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menus
{
    public class MainMenu : MonoBehaviour
    {

        // Use this for initialization
        void Start ()
        {
	
        }
	
        // Update is called once per frame
        void Update ()
        {
	
        }

        public void OnClick()
        {
            SceneManager.LoadScene("TestLevel");
        }
    }
}
