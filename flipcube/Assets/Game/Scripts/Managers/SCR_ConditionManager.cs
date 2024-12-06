using UnityEngine;

public class SCR_ConditionManager : MonoBehaviour
{
    [SerializeField] SCR_ConditionInfo m_gameCondition;
    [SerializeField] SCR_UIManager m_uiManager;

    int _rRedPoints;
    int _rBluePoints;
    int _rGreenPoints;
    int _rYellowPoints;

    void SetUpInfo()
    {
        SCR_GameStatus.ResetAll();

        _rRedPoints = m_gameCondition.GetRequiredRedPoints();
        _rBluePoints = m_gameCondition.GetRequiredBluePoints();
        _rGreenPoints = m_gameCondition.GetRequiredGreenPoints();
        _rYellowPoints = m_gameCondition.GetRequiredYellowPoints();
        SCR_GameStatus.SetMovimentLimit(m_gameCondition.GetMaxMoviments());


        m_uiManager.SetupRequiredPoints(_rRedPoints, _rBluePoints, _rGreenPoints, _rYellowPoints);
        m_uiManager.UpdateActualPoints();
    }
    void Start()
    {
        SetUpInfo();
    }

    public void CheckWinCondition()
    {
        if (!SCR_GameState.IsGameStateEnded())
        {
            if (SCR_GameStatus.CheckRedPoints(_rRedPoints) && SCR_GameStatus.CheckBluePoints(_rBluePoints) && SCR_GameStatus.CheckGreenPoints(_rGreenPoints) && SCR_GameStatus.CheckYellowPoints(_rYellowPoints))
            {
                SCR_GameState.SetCurrentGameState(GameState.ended);
                m_uiManager.SetVictoryScreen();
            }
            else
            {
                if (SCR_GameStatus.CheckMovimentsLeft() <= 0)
                {
                    SCR_GameState.SetCurrentGameState(GameState.ended);
                    m_uiManager.SetLoseScreen();
                }
            }
        }
    }
}
