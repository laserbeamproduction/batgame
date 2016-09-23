using UnityEngine;
using System.Collections;

public class TransitionManager : MonoBehaviour {
    private EnvironmentModel currentEnvironment;
    public EnvironmentModel[] environments;

    private int currentEnviroment = -1;
    private int currentStage = 0;

    void Start () {
        EventManager.StartListening(EventTypes.CHANGE_ENVIRONMENT, ChangeEnvironment);
	}

    private void ChangeEnvironment(object value)
    {
        // move to the next stage
        currentEnviroment++;
        int chosenStage = currentEnviroment;

        // if all enviroments have been played, start random enviroments
        if (currentEnviroment >= environments.Length - 1)
            chosenStage = UnityEngine.Random.Range(0, environments.Length - 1);

        EventManager.TriggerEvent(EventTypes.TRANSITION_END, environments[chosenStage]);
    }
}
