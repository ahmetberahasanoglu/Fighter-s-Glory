using UnityEngine;
using System.Collections.Generic;

public class FightManager : MonoBehaviour
{
    public List<Fighter> fighters; // T�m d�v����ler
    public List<Fight> fightSchedule; // D�v�� takvimi

    void Start()
    {
        SimulateFights(); // Oyun ba�lad���nda ma�lar� sim�le et
    }

    // Ma�lar� sim�le etme
    private void SimulateFights()
    {
        foreach (Fight fight in fightSchedule)
        {
            if (string.IsNullOrEmpty(fight.result)) // E�er ma� hen�z sim�le edilmediyse
            {
                fight.result = SimulateFightResult(fight.fighter1, fight.fighter2);
                Debug.Log($"{fight.date}: {fight.fighter1.fighterName} vs. {fight.fighter2.fighterName} -> {fight.result}");
            }
        }
    }

    // Ma� sonucunu sim�le etme
    private string SimulateFightResult(Fighter fighter1, Fighter fighter2)
    {
        // Yeteneklere ve �ansa g�re sonu� belirle
        int fighter1Score = fighter1.striking + fighter1.grappling + fighter1.conditioning + Random.Range(0, 10);
        int fighter2Score = fighter2.striking + fighter2.grappling + fighter2.conditioning + Random.Range(0, 10);

        if (fighter1Score > fighter2Score)
        {
            return $"{fighter1.fighterName} (TKO)";
        }
        else if (fighter2Score > fighter1Score)
        {
            return $"{fighter2.fighterName} (Submission)";
        }
        else
        {
            return "Berabere";
        }
    }
}