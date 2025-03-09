using UnityEngine;
using System.Collections.Generic;

public class FightManager : MonoBehaviour
{
    public List<Fighter> fighters; // Tüm dövüþçüler
    public List<Fight> fightSchedule; // Dövüþ takvimi

    void Start()
    {
        SimulateFights(); // Oyun baþladýðýnda maçlarý simüle et
    }

    // Maçlarý simüle etme
    private void SimulateFights()
    {
        foreach (Fight fight in fightSchedule)
        {
            if (string.IsNullOrEmpty(fight.result)) // Eðer maç henüz simüle edilmediyse
            {
                fight.result = SimulateFightResult(fight.fighter1, fight.fighter2);
                Debug.Log($"{fight.date}: {fight.fighter1.fighterName} vs. {fight.fighter2.fighterName} -> {fight.result}");
            }
        }
    }

    // Maç sonucunu simüle etme
    private string SimulateFightResult(Fighter fighter1, Fighter fighter2)
    {
        // Yeteneklere ve þansa göre sonuç belirle
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