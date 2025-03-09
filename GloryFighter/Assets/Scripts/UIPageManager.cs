using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(UIManager))]
public class UIPageManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform pageButtonsParent; // Butonlarýn parent'ý
    [SerializeField] private GameObject[] screens; // Butonlara karþýlýk gelen ekranlar

    private UIManager UIManager;

    [Header("Settings")]
    [SerializeField] private Vector2 minMaxWidth = new Vector2(100f, 200f); // Geniþlik aralýðý
    [SerializeField] private float animationDuration = 0.3f; // Animasyon süresi

    private int currentPageIndex = -1; // Þu anda seçili sayfanýn indeksi

    private void Awake()
    {
        UIManager = GetComponent<UIManager>();

        // Butonlara týklama event'larýný ekle
        for (int i = 0; i < pageButtonsParent.childCount; i++)
        {
            int _i = i; // Closure için yerel deðiþken
            pageButtonsParent.GetChild(i).GetComponent<Button>().onClick.AddListener(() => OnPageButtonClicked(_i));
        }

        // Sayfa deðiþtiðinde tetiklenecek event'ý baðla
        UIManager.pageChanged += OnPageChanged;
    }

    private void OnDestroy()
    {
        // Event baðlantýsýný kaldýr
        if (UIManager != null)
        {
            UIManager.pageChanged -= OnPageChanged;
        }
    }

    private void OnPageButtonClicked(int pageIndex)
    {
        // Ýlgili ekraný göster
        if (pageIndex >= 0 && pageIndex < screens.Length)
        {
            UIManager.ShowScreen(screens[pageIndex], pageIndex);
        }
        else
        {
            Debug.LogError("Invalid page index: " + pageIndex);
        }
    }

    private void OnPageChanged(int pageIndex)
    {
        // Ayný sayfaya tekrar týklanýrsa iþlem yapma
        if (pageIndex == currentPageIndex)
            return;

        // Tüm butonlarýn geniþliðini güncelle
        for (int i = 0; i < pageButtonsParent.childCount; i++)
        {
            LayoutElement element = pageButtonsParent.GetChild(i).GetComponent<LayoutElement>();
            if (element == null)
                continue;

            // Önceki tween iþlemlerini durdur
            DOTween.Kill(element);

            // Hedef geniþliði belirle
            float targetWidth = (pageIndex == i) ? minMaxWidth.y : minMaxWidth.x;

            // Geniþlik animasyonu
            DOTween.To(
                () => element.preferredWidth, // Baþlangýç deðeri
                x => element.preferredWidth = x, // Güncelleme metodu
                targetWidth, // Hedef deðer
                animationDuration // Süre
            ).SetEase(Ease.OutQuad); // Easing türü
        }

        // Buton durumlarýný güncelle
        UpdatePageButtonState(pageIndex);

        // Mevcut sayfa indeksini güncelle
        currentPageIndex = pageIndex;
    }

    private void UpdatePageButtonState(int pageIndex)
    {
        // Yeni sayfanýn butonunu seçili hale getir
        pageButtonsParent.GetChild(pageIndex).GetComponent<PageButton>()?.Select();

        // Önceki sayfanýn butonunu seçimden çýkar
        if (currentPageIndex >= 0 && currentPageIndex < pageButtonsParent.childCount)
        {
            pageButtonsParent.GetChild(currentPageIndex).GetComponent<PageButton>()?.DeSelect();
        }
    }
}