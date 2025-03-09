using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string title;
        public string description;
        public int currentProgress;
        public int maxProgress;
        public bool isUnlocked;
        public bool isCollected;
        public int rewardGold;
        public Sprite icon;
    }

    public List<Achievement> achievements = new List<Achievement>();
    public Transform achievementListParent;
    public GameObject achievementPrefab;

    public GameObject achievementScreen;
 //   public Button closeButton;

    private int playerGold = 0;

    private void Start()
    {
       // closeButton.onClick.AddListener(CloseAchievementScreen);
        PopulateAchievements();
    }

    void PopulateAchievements()
    {
        foreach (Transform child in achievementListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var achievement in achievements)
        {
            GameObject newAchievement = Instantiate(achievementPrefab, achievementListParent);

            TextMeshProUGUI titleText = newAchievement.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descText = newAchievement.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            Image iconImage = newAchievement.transform.Find("Icon").GetComponent<Image>();
            Slider progressSlider = newAchievement.transform.Find("ProgressBar").GetComponent<Slider>();
            Button collectButton = newAchievement.transform.Find("CollectButton").GetComponent<Button>();
            TextMeshProUGUI rewardText = newAchievement.transform.Find("RewardText").GetComponent<TextMeshProUGUI>();

            titleText.text = achievement.title;
            descText.text = achievement.description;
            iconImage.sprite = achievement.icon;
            progressSlider.maxValue = achievement.maxProgress;
            progressSlider.value = achievement.currentProgress;
            rewardText.text = achievement.rewardGold + " Gold";

            if (achievement.isUnlocked)
            {
                iconImage.color = Color.white;
                titleText.alpha = 1f;
                descText.alpha = 1f;
            }
            else
            {
                iconImage.color = Color.gray;
                titleText.alpha = 0.5f;
                descText.alpha = 0.5f;
            }

            if (achievement.currentProgress >= achievement.maxProgress)
            {
                collectButton.interactable = true;
                collectButton.onClick.AddListener(() => CollectReward(achievement));
            }
            else
            {
                collectButton.interactable = false;
            }

            newAchievement.transform.localScale = Vector3.zero;
            newAchievement.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        }
    }

    public void UnlockAchievement(string title)
    {
        foreach (var achievement in achievements)
        {
            if (achievement.title == title && !achievement.isUnlocked)
            {
                achievement.isUnlocked = true;
                Debug.Log("Baþarým Açýldý: " + title);
                PopulateAchievements();
                break;
            }
        }
    }

    public void UpdateAchievementProgress(string title, int amount)
    {
        foreach (var achievement in achievements)
        {
            if (achievement.title == title && !achievement.isCollected)
            {
                achievement.currentProgress += amount;
                if (achievement.currentProgress >= achievement.maxProgress)
                {
                    achievement.isUnlocked = true;
                }
                PopulateAchievements();
                break;
            }
        }
    }

    public void CollectReward(Achievement achievement)
    {
        if (!achievement.isCollected)
        {
            playerGold += achievement.rewardGold;
            achievement.isCollected = true;
            Debug.Log(achievement.title + " ödülü alýndý! + " + achievement.rewardGold + " Gold");
            PopulateAchievements();
        }
    }

    public void OpenAchievementScreen()
    {
        achievementScreen.SetActive(true);
        achievementScreen.transform.localScale = Vector3.zero;
        achievementScreen.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
    }

    /*public void CloseAchievementScreen()
    {
        achievementScreen.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() => achievementScreen.SetActive(false));
    }*/
}
