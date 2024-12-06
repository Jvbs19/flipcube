using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ConditionManager : MonoBehaviour
{
    [SerializeField] SCR_ConditionInfo m_gameCondition;

    int m_rRedPoints;
    int m_rBluePoints;
    int m_rGreenPoints;
    int m_rYellowPoints;
    int maxMoviments;
    float maxTimer;

    void SetUpInfo()
    {
        SCR_GameStatus.ResetAll();
        m_rRedPoints = m_gameCondition.GetRequiredRedPoints();
        m_rBluePoints = m_gameCondition.GetRequiredBluePoints();
        m_rGreenPoints = m_gameCondition.GetRequiredGreenPoints();
        m_rYellowPoints = m_gameCondition.GetRequiredYellowPoints();
        SCR_GameStatus.SetMovimentLimit(m_gameCondition.GetMaxMoviments());
    }
    void Start()
    {
        SetUpInfo();
    }

    public void CheckWinCondition()
    {
        if (SCR_GameStatus.CheckRedPoints(m_rRedPoints) && SCR_GameStatus.CheckBluePoints(m_rBluePoints) && SCR_GameStatus.CheckGreenPoints(m_rGreenPoints) && SCR_GameStatus.CheckYellowPoints(m_rYellowPoints))
        {
            Debug.Log("Voce Venceu!!!");
            SCR_GameState.SetCurrentGameState(GameState.ended);
        }
        else
        {
            if (SCR_GameStatus.CheckMovimentsLeft() <= 0)
            {
                Debug.Log("Voce Perdeu!!!");
                SCR_GameState.SetCurrentGameState(GameState.ended);
            }
        }
    }
}
