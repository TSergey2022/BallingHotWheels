using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvasScript : MonoBehaviour {
  // Панели
  [SerializeField] private GameObject MainMenuPanel;
  [SerializeField] private GameObject OptionsPanel;
  [SerializeField] private GameObject LeaderboardPanel;
  [SerializeField] private GameObject CreditsPanel;

  // Кнопка Quit
  [SerializeField] private Button QuitButton;
  
  // Записи в таблице лидеров
  [SerializeField] private GameObject LeaderboardInner2;
  [SerializeField] private GameObject LeaderboardEntryPrefab;
  
  private void Start() {
    // Проверка на платформу и скрытие кнопки Quit в случае Android, iOS или WebGL
    if (
      Application.platform == RuntimePlatform.Android
      || Application.platform == RuntimePlatform.IPhonePlayer
      || Application.platform == RuntimePlatform.WebGLPlayer
    ) {
      QuitButton.gameObject.SetActive(false); // Прячем кнопку Quit на этих платформах
    }
  }

  public void PlayGame() {
    CloseAllPanels();
    SceneManager.LoadScene("GameScene");
  }
  
  // Метод для открытия главного меню
  public void OpenMainMenu() {
    CloseAllPanels(); // Закрыть все панели перед открытием новой
    MainMenuPanel.SetActive(true);
  }

  // Метод для открытия панели опций
  public void OpenOptions() {
    CloseAllPanels(); // Закрыть все панели перед открытием новой
    OptionsPanel.SetActive(true);
  }

  // Метод для открытия панели лидеров
  public void OpenLeaderboard() {
    CloseAllPanels();
    for (var i = LeaderboardInner2.transform.childCount - 1; i >= 0; i--) {
      var go = LeaderboardInner2.transform.GetChild(i).gameObject;
      if (go.CompareTag("LeaderboardEntry"))
        Destroy(go);
    }
    foreach (var entry in GodScript.Instance.Leaderboard) {
      var leaderboardEntryGo = Instantiate(LeaderboardEntryPrefab, LeaderboardInner2.transform);
      var script = leaderboardEntryGo.GetComponent<LeaderboardEntryScript>();
      script.SetText(entry);
    }
    LeaderboardPanel.SetActive(true);
  }

  // Метод для открытия панели с авторами
  public void OpenCredits() {
    CloseAllPanels();
    CreditsPanel.SetActive(true);
  }

  // Метод для выхода из игры
  public void QuitGame() {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false; // Для редактора Unity
#else
    Application.Quit();  // Для сборки игры
#endif
  }

  // Метод для закрытия всех панелей
  private void CloseAllPanels() {
    MainMenuPanel.SetActive(false);
    OptionsPanel.SetActive(false);
    LeaderboardPanel.SetActive(false);
    CreditsPanel.SetActive(false);
  }
}
