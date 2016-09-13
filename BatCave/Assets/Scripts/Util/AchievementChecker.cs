﻿using UnityEngine;
using System.Collections;

public class AchievementChecker : MonoBehaviour {

    public static void CheckForEndlessScoreAchievement(float score) {
        if (score >= 5000)
            GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_chicken_wings_100_points);
        if (score >= 10000)
            GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_buffalo_wings_120_points);
        if (score >= 20000)
            GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_wingin_it_160_points);
        if (score >= 50000)
            GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_ride_the_wings_of_pestilance_200_points);
    }

    public static void CheckForWelcomeAchievement() {
        GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_bun_venit_20_points);
    }

    public static void CheckForTimingAchievement(int excellentEchosSequence, int goodEchosSequence) {
        if (excellentEchosSequence >= 5) {
            GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_excellent_100_points);
        }

        if (goodEchosSequence >= 50) {
            GooglePlayHelper.GetInstance().UnlockAchievement(GPGSConstant.achievement_good_50_points);
        }
    }
}
