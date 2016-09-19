using System.Collections.Generic;
using UnityEngine;

public class BatchModel {

    private SpawnPointModel[] spawnPoints;
    private PickupModel[] pickups;
    private ObstacleModel[] obstacles;

    public BatchModel(SpawnPointModel[] spawnPoints, PickupModel[] pickups, ObstacleModel[] obstacles) {
        this.spawnPoints = spawnPoints;
        this.pickups = pickups;
        this.obstacles = obstacles;
    }
}
