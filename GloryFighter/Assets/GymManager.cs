using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GymManager : MonoBehaviour
{
    // Yetenekler ve seviyeleri
    public int strength = 0;       // Güç
    public int grappling = 0;      // Grappling
    public int conditioning = 0;   // Kondisyon
    public int speed = 0;          // Hýz
    public int fightIQ = 0;        // Fight IQ
    public int flexibility = 0;    // Esneklik
    public int durability = 0;     // Dayanýklýlýk

    public int upgradeLimit = 3;   // Maç öncesi geliþtirme limiti
    private int upgradesUsed = 0;  // Kullanýlan geliþtirme hakký

    // UI Elementleri
    public TextMeshProUGUI fightingStyleText; // Dövüþ stili metni
    public TextMeshProUGUI countdownText;     // Geri sayým metni
    public Button[] skillButtons;             // Yetenek butonlarý
    public Slider[] skillSliders;             // Yetenek slider'larý

    private bool isUpgrading = false; // Geliþtirme iþlemi devam ediyor mu?

    void Start()
    {
        UpdateFightingStyle(); // Baþlangýçta dövüþ stilini güncelle
        UpdateSliders();       // Slider'larý güncelle
    }

    // Yetenek geliþtirme metodu
    public void UpgradeSkill(string skillName)
    {
        if (upgradesUsed >= upgradeLimit)
        {
            Debug.Log("Geliþtirme hakkýnýz kalmadý!");
            return;
        }

        if (!isUpgrading)
        {
            StartCoroutine(UpgradeSkillCoroutine(skillName));
        }
    }

    // Geliþtirme iþlemi (bekleme süresi ile)
    private IEnumerator UpgradeSkillCoroutine(string skillName)
    {
        isUpgrading = true;
        upgradesUsed++;
        Debug.Log($"{skillName} geliþtiriliyor...");

        // Butonlarý devre dýþý býrak
        SetButtonsInteractable(false);

        // Geri sayým baþlat
        float countdownTime = 5f; // 5 saniye
        while (countdownTime > 0)
        {
            countdownText.text = $"Geliþtirme bitene kalan süre: {countdownTime:F1} saniye";
            yield return new WaitForSeconds(0.1f);
            countdownTime -= 0.1f;
        }

        // Yeteneði geliþtir
        switch (skillName)
        {
            case "Strength":
                if (strength < 100) strength++;
                break;
            case "Grappling":
                if (grappling < 100) grappling++;
                break;
            case "Conditioning":
                if (conditioning < 100) conditioning++;
                break;
            case "Speed":
                if (speed < 100) speed++;
                break;
            case "FightIQ":
                if (fightIQ < 100) fightIQ++;
                break;
            case "Flexibility":
                if (flexibility < 100) flexibility++;
                break;
            case "Durability":
                if (durability < 100) durability++;
                break;
        }

        Debug.Log($"{skillName} geliþtirildi! Yeni seviye: {GetSkillLevel(skillName)}");

        // Slider'larý ve dövüþ stilini güncelle
        UpdateSliders();
        UpdateFightingStyle();

        // Geri sayým metnini sýfýrla
        countdownText.text = "";

        // Butonlarý tekrar aktif et
        SetButtonsInteractable(true);
        isUpgrading = false;
    }

    // Yetenek seviyesini alma
    private int GetSkillLevel(string skillName)
    {
        switch (skillName)
        {
            case "Strength": return strength;
            case "Grappling": return grappling;
            case "Conditioning": return conditioning;
            case "Speed": return speed;
            case "FightIQ": return fightIQ;
            case "Flexibility": return flexibility;
            case "Durability": return durability;
            default: return 0;
        }
    }

    // Dövüþ stilini güncelleme
    private void UpdateFightingStyle()
    {
        // Agresif stil: Güç ve Hýz yüksek
        if (strength >= 70 && speed >= 70)
        {
            fightingStyleText.text = "Fighting Style: Agresif";
        }
        // Dengeli stil: Tüm yetenekler orta seviyede
        else if (strength >= 50 && grappling >= 50 && conditioning >= 50 && speed >= 50 && fightIQ >= 50 && flexibility >= 50 && durability >= 50)
        {
            fightingStyleText.text = "Fighting Style: Dengeli";
        }
        // Savunmacý stil: Dayanýklýlýk ve Grappling yüksek
        else if (durability >= 70 && grappling >= 70)
        {
            fightingStyleText.text = "Fighting Style: Savunmacý";
        }
        // Teknik stil: Fight IQ ve Esneklik yüksek
        else if (fightIQ >= 70 && flexibility >= 70)
        {
            fightingStyleText.text = "Fighting Style: Teknik";
        }
        else
        {
            fightingStyleText.text = "Fighting Style: Belirsiz";
        }
    }

    // Slider'larý güncelleme
    private void UpdateSliders()
    {
        skillSliders[0].value = speed;       // Güç
        skillSliders[1].value = durability;      // Grappling
        skillSliders[2].value = grappling;   // Kondisyon
        skillSliders[3].value = strength;          // Hýz
        skillSliders[4].value = conditioning;        // Fight IQ
        skillSliders[5].value = fightIQ;    // Esneklik
        skillSliders[6].value = flexibility;     // Dayanýklýlýk
    }

    // Butonlarý aktif/pasif yapma
    private void SetButtonsInteractable(bool isInteractable)
    {
        foreach (Button button in skillButtons)
        {
            button.interactable = isInteractable;
        }
    }
}