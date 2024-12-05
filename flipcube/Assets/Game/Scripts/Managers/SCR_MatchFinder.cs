using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MatchFinder : MonoBehaviour
{
    [SerializeField] SCR_BoardManager m_board;

    [SerializeField] List<SCR_TileBehaviour> m_currentMatches = new List<SCR_TileBehaviour>();

    [SerializeField] float m_findSpeed = 0.6f;

    public void FindMatches()
    {
        StartCoroutine(FindAllMatches());
    }

    IEnumerator FindAllMatches()
    {
        yield return new WaitForSeconds(m_findSpeed);
        for (int i = 0; i < m_board.GetBoardWidth(); i++)
        {
            for (int j = 0; j < m_board.GetBoardHeight(); j++)
            {
                SCR_TileBehaviour tile = m_board.GetTileByPos(i, j);

                if (tile != null)
                {
                    if (i > 0 && i < m_board.GetBoardWidth() - 1)
                    {
                        SCR_TileBehaviour leftTile = m_board.GetTileByPos(i - 1, j);
                        SCR_TileBehaviour rightTile = m_board.GetTileByPos(i + 1, j);

                        if (leftTile != null && rightTile != null)
                            if (leftTile.GetMyTypeName() == tile.GetMyTypeName() && rightTile.GetMyTypeName() == tile.GetMyTypeName())
                            {
                                AddToList(tile);
                                AddToList(leftTile);
                                AddToList(rightTile);

                                leftTile.SetIsMatched(true);
                                rightTile.SetIsMatched(true);
                                tile.SetIsMatched(true);

                                Debug.Log("MATCH FOUND");
                            }
                    }
                    if (j > 0 && j < m_board.GetBoardHeight() - 1)
                    {
                        SCR_TileBehaviour upTile = m_board.GetTileByPos(i, j + 1);
                        SCR_TileBehaviour downTile = m_board.GetTileByPos(i, j - 1);

                        if (upTile != null && downTile != null)
                            if (upTile.GetMyTypeName() == tile.GetMyTypeName() && downTile.GetMyTypeName() == tile.GetMyTypeName()) // Up and Down
                            {
                                AddToList(tile);
                                AddToList(upTile);
                                AddToList(downTile);

                                upTile.SetIsMatched(true);
                                downTile.SetIsMatched(true);
                                tile.SetIsMatched(true);

                                Debug.Log("MATCH FOUND");
                            }
                    }
                }
            }
        }
    }
    void AddToList(SCR_TileBehaviour obj)
    {
        if (!m_currentMatches.Contains(obj))
            m_currentMatches.Add(obj);
    }
    public void RemoveFromList(SCR_TileBehaviour obj)
    {
        m_currentMatches.Remove(obj);
    }
}
