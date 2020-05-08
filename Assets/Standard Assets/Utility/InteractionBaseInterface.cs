using UnityEngine;
using System.Collections;

public interface IInteractionCube
{
    void StartInteraction(GravityController i_player);
    void EndInteraction();
}

public interface ITriggerSwitchFunction
{
    void StartTriggering();
}


public interface ITriggerComparison
{
    bool CompareIdentifier(string identifier);
}