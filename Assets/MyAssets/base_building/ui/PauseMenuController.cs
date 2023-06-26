using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyAssets.base_building.ui
{
    public class PauseMenuController : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            SceneManager.LoadScene("MainMenu");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}