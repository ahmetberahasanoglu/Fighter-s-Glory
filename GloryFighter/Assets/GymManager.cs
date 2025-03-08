using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GymManager : MonoBehaviour
{
    // Yetenekler ve seviyeleri
    public int strength = 0;       // G��
    public int grappling = 0;      // Grappling
    public int conditioning = 0;   // Kondisyon
    public int speed = 0;          // H�z
    public int fightIQ = 0;        // Fight IQ
    public int flexibility = 0;    // Esneklik
    public int durability = 0;     // Dayan�kl�l�k

    public int upgradeLimit = 3;   // Ma� �ncesi geli�tirme limiti
    private int upgradesUsed = 0;  // Kullan�lan geli�tirme hakk�

    // UI Elementleri
    public TextMeshProUGUI fightingStyleText; // D�v�� stili metni
    public TextMeshProUGUI countdownText;     // Geri say�m metni
    public Button[] skillButtons;             // Yetenek butonlar�
    public Slider[] skillSliders;             // Yetenek slider'lar�

    private bool isUpgrading = false; // Geli�tirme i�lemi devam ediyor mu?

    void Start()
    {
        UpdateFightingStyle(); // Ba�lang��ta d�v�� stilini g�ncelle
        UpdateSliders();       // Slider'lar� g�ncelle
    }

    // Yetenek geli�tirme metodu
    public void UpgradeSkill(string skillName)
    {
        if (upgradesUsed >= upgradeLimit)
        {
            Debug.Log("Geli�tirme hakk�n�z kalmad�!");
            return;
        }

        if (!isUpgrading)
        {
            StartCoroutine(UpgradeSkillCoroutine(skillName));
        }
    }

    // Geli�tirme i�lemi (bekleme s�resi ile)
    private IEnumerator UpgradeSkillCoroutine(string skillName)
    {
        isUpgrading = true;
        upgradesUsed++;
        Debug.Log($"{skillName} geli�tiriliyor...");

        // Butonlar� devre d��� b�rak
        SetButtonsInteractable(false);

        // Geri say�m ba�lat
        float countdownTime = 5f; // 5 saniye
        while (countdownTime > 0)
        {
            countdownText.text = $"Geli�tirme bitene kalan s�re: {countdownTime:F1} saniye";
            yield return new WaitForSeconds(0.1f);
            countdownTime -= 0.1f;
        }

        // Yetene�i geli�tir
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

        Debug.Log($"{skillName} geli�tirildi! Yeni seviye: {GetSkillLevel(skillName)}");

        // Slider'lar� ve d�v�� stilini g�ncelle
        UpdateSliders();
        UpdateFightingStyle();

        // Geri say�m metnini s�f�rla
        countdownText.text = "";

        // Butonlar� tekrar aktif et
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

    // D�v�� stilini g�ncelleme
    private void UpdateFightingStyle()
    {
        // Agresif stil: G�� ve H�z y�ksek
        if (strength >= 70 && speed >= 70)
        {
            fightingStyleText.text = "Fighting Style: Agresif";
        }
        // Dengeli stil: T�m yetenekler orta seviyede
        else if (strength >= 50 && grappling >= 50 && conditioning >= 50 && speed >= 50 && fightIQ >= 50 && flexibility >= 50 && durability >= 50)
        {
            fightingStyleText.text = "Fighting Style: Dengeli";
        }
        // Savunmac� stil: Dayan�kl�l�k ve Grappling y�ksek
        else if (durability >= 70 && grappling >= 70)
        {
            fightingStyleText.text = "Fighting Style: Savunmac�";
        }
        // Teknik stil: Fight IQ ve Esneklik y�ksek
        else if (fightIQ >= 70 && flexibility >= 70)
        {
            fightingStyleText.text = "Fighting Style: Teknik";
        }
        else
        {
            fightingStyleText.text = "Fighting Style: Belirsiz";
        }
    }

    // Slider'lar� g�ncelleme
    private void UpdateSliders()
    {
        skillSliders[0].value = speed;       // G��
        skillSliders[1].value = durability;      // Grappling
        skillSliders[2].value = grappling;   // Kondisyon
        skillSliders[3].value = strength;          // H�z
        skillSliders[4].value = conditioning;        // Fight IQ
        skillSliders[5].value = fightIQ;    // Esneklik
        skillSliders[6].value = flexibility;     // Dayan�kl�l�k
    }

    // Butonlar� aktif/pasif yapma
    private void SetButtonsInteractable(bool isInteractable)
    {
        foreach (Button button in skillButtons)
        {
            button.interactable = isInteractable;
        }
    }
}