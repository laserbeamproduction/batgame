using UnityEngine;
using System.Collections;

public class AchievementChecker : MonoBehaviour {

    public static void CheckForEndlessScoreAchievement(float score) {
        if (score >= 5000)
            GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_chicken_wings);
        if (score >= 10000)
            GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_buffalo_wings);
        if (score >= 20000)
            GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_wingin_it);
        if (score >= 50000)
            GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_ride_the_wings_of_pestilance);
    }

    public static void CheckForWelcomeAchievement() {
        GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_bun_venit);
    }
}
