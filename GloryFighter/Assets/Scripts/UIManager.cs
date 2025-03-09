using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject fightScreen;
    public GameObject gymScreen;
    public GameObject shopScreen;
    public GameObject profileScreen;
    public GameObject achievementsScreen;

    public Button fightButton;
    public Button gymButton;
    public Button shopButton;
    public Button profileButton;
    public Button achievementsButton;

    public TextMeshProUGUI diamondText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI ageText;

    // Oyuncu verileri
    private int diamonds = 100;
    private int gold = 500;
    private float age = 18.0f;

    public float panelTransitionDuration = 0.5f;
    public Ease panelTransitionEase = Ease.OutQuad;

    private GameObject currentScreen;
    private GameObject targetScreen; // A��lacak yeni ekran

    // Sayfa de�i�imi event'i
    public delegate void PageChangedHandler(int pageIndex);
    public event PageChangedHandler pageChanged;

    void Start()
    {
        ShowMainMenu();
        UpdateUI();

        // Butonlara t�klama event'lar�n� ekle
        fightButton.onClick.AddListener(() => ShowScreen(fightScreen, 0));
        gymButton.onClick.AddListener(() => ShowScreen(gymScreen, 1));
        shopButton.onClick.AddListener(() => ShowScreen(shopScreen, 2));
        profileButton.onClick.AddListener(() => ShowScreen(profileScreen, 3));
        achievementsButton.onClick.AddListener(() => ShowScreen(achievementsScreen, 4));
    }

    public void AnimateText(TextMeshProUGUI text)
    {
        text.transform.DOScale(1.2f, 0.2f)
            .OnComplete(() => text.transform.DOScale(1f, 0.2f));
    }

    public void UpdateUI()
    {
        AnimateText(diamondText);
        AnimateText(goldText);
        AnimateText(ageText);

        diamondText.text = diamonds.ToString();
        goldText.text = gold.ToString();
        ageText.text = age.ToString("F1");
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        UpdateUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    public void IncreaseAge()
    {
        age += 0.5f;
        UpdateUI();
    }

    public void OnFightEnd(bool isWin)
    {
        if (isWin)
        {
            AddGold(100);
            AddDiamonds(10);
        }
        else
        {
            AddGold(50);
        }
        IncreaseAge();
    }

    public void ShowMainMenu()
    {
        SetActiveScreen(mainMenu);
    }

    public void ShowScreen(GameObject screen, int pageIndex)
    {
        // Ayn� ekrana tekrar t�klan�rsa hi�bir �ey yapma
        if (currentScreen == screen)
            return;

        // Hedef ekran� kaydet
        targetScreen = screen;

        // Mevcut ekran� kapat
        if (currentScreen != null)
        {
            CloseScreen(currentScreen);
        }
        else
        {
            // E�er mevcut ekran yoksa, direkt yeni ekran� a�
            OpenScreen(targetScreen);
            pageChanged?.Invoke(pageIndex);
        }
    }

    private void OpenScreen(GameObject screen)
    {
        screen.SetActive(true);

        RectTransform rectTransform = screen.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(2000, 0, 0);

        rectTransform.DOLocalMoveX(0, panelTransitionDuration)
            .SetEase(panelTransitionEase)
            .OnComplete(() => Debug.Log(screen.name + " a��ld�"));

        currentScreen = screen;
        mainMenu.SetActive(false);
    }

    private void CloseScreen(GameObject screen)
    {
        RectTransform rectTransform = screen.GetComponent<RectTransform>();

        rectTransform.DOLocalMoveX(-2000, panelTransitionDuration)
            .SetEase(panelTransitionEase)
            .OnComplete(() =>
            {
                screen.SetActive(false);
                Debug.Log(screen.name + " kapand�");

                // Mevcut ekran kapand�ktan sonra yeni ekran� a�
                OpenScreen(targetScreen);

                // Sayfa de�i�imi event'ini tetikle
                int pageIndex = GetPageIndex(targetScreen);
                pageChanged?.Invoke(pageIndex);
            });
    }

    private int GetPageIndex(GameObject screen)
    {
        // Ekran�n indeksini d�nd�r
        if (screen == fightScreen) return 0;
        if (screen == gymScreen) return 1;
        if (screen == shopScreen) return 2;
        if (screen == profileScreen) return 3;
        if (screen == achievementsScreen) return 4;
        return -1; // Ge�ersiz indeks
    }

    private void SetActiveScreen(GameObject activeScreen)
    {
        fightScreen.SetActive(false);
        gymScreen.SetActive(false);
        shopScreen.SetActive(false);
        profileScreen.SetActive(false);
        achievementsScreen.SetActive(false);

        activeScreen.SetActive(true);
    }
}