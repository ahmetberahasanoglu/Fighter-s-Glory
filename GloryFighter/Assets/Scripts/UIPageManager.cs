using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(UIManager))]
public class UIPageManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform pageButtonsParent; // Butonlar�n parent'�
    [SerializeField] private GameObject[] screens; // Butonlara kar��l�k gelen ekranlar

    private UIManager UIManager;

    [Header("Settings")]
    [SerializeField] private Vector2 minMaxWidth = new Vector2(100f, 200f); // Geni�lik aral���
    [SerializeField] private float animationDuration = 0.3f; // Animasyon s�resi

    private int currentPageIndex = -1; // �u anda se�ili sayfan�n indeksi

    private void Awake()
    {
        UIManager = GetComponent<UIManager>();

        // Butonlara t�klama event'lar�n� ekle
        for (int i = 0; i < pageButtonsParent.childCount; i++)
        {
            int _i = i; // Closure i�in yerel de�i�ken
            pageButtonsParent.GetChild(i).GetComponent<Button>().onClick.AddListener(() => OnPageButtonClicked(_i));
        }

        // Sayfa de�i�ti�inde tetiklenecek event'� ba�la
        UIManager.pageChanged += OnPageChanged;
    }

    private void OnDestroy()
    {
        // Event ba�lant�s�n� kald�r
        if (UIManager != null)
        {
            UIManager.pageChanged -= OnPageChanged;
        }
    }

    private void OnPageButtonClicked(int pageIndex)
    {
        // �lgili ekran� g�ster
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
        // Ayn� sayfaya tekrar t�klan�rsa i�lem yapma
        if (pageIndex == currentPageIndex)
            return;

        // T�m butonlar�n geni�li�ini g�ncelle
        for (int i = 0; i < pageButtonsParent.childCount; i++)
        {
            LayoutElement element = pageButtonsParent.GetChild(i).GetComponent<LayoutElement>();
            if (element == null)
                continue;

            // �nceki tween i�lemlerini durdur
            DOTween.Kill(element);

            // Hedef geni�li�i belirle
            float targetWidth = (pageIndex == i) ? minMaxWidth.y : minMaxWidth.x;

            // Geni�lik animasyonu
            DOTween.To(
                () => element.preferredWidth, // Ba�lang�� de�eri
                x => element.preferredWidth = x, // G�ncelleme metodu
                targetWidth, // Hedef de�er
                animationDuration // S�re
            ).SetEase(Ease.OutQuad); // Easing t�r�
        }

        // Buton durumlar�n� g�ncelle
        UpdatePageButtonState(pageIndex);

        // Mevcut sayfa indeksini g�ncelle
        currentPageIndex = pageIndex;
    }

    private void UpdatePageButtonState(int pageIndex)
    {
        // Yeni sayfan�n butonunu se�ili hale getir
        pageButtonsParent.GetChild(pageIndex).GetComponent<PageButton>()?.Select();

        // �nceki sayfan�n butonunu se�imden ��kar
        if (currentPageIndex >= 0 && currentPageIndex < pageButtonsParent.childCount)
        {
            pageButtonsParent.GetChild(currentPageIndex).GetComponent<PageButton>()?.DeSelect();
        }
    }
}