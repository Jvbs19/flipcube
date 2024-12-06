using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SCR_MatchFinder : MonoBehaviour
{
    [SerializeField] SCR_BoardManager m_board;

    [SerializeField] List<SCR_TileBehaviour> m_currentMatches = new List<SCR_TileBehaviour>();

    [SerializeField] float m_findSpeed = 0.6f;

    SCR_TileBehaviour _currentTile;

    public void FindMatches()
    {
        StartCoroutine(FindAllMatches());
    }

    public void FindBombMatches(int i, int j)
    {
        StartCoroutine(FindBombMatch(i, j));
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
                            }
                    }
                }
            }
        }
    }

    public void Check4More()
    {
        if (m_currentMatches.Count() < 3)
        {
            int redCount = 0;
            int blueCount = 0;
            int greenCount = 0;
            int yellowCount = 0;
            int rowCount = 0;
            int colCount = 0;
            SCR_BombBehaviour.BombType higher;

            foreach (SCR_TileBehaviour tile in m_currentMatches)
            {
                if (tile.GetMyTypeNum() == 0)
                    redCount++;
                else if (tile.GetMyTypeNum() == 1)
                    blueCount++;
                else if (tile.GetMyTypeNum() == 2)
                    greenCount++;
                else
                    yellowCount++;


                if (tile.GetMyHeight() > tile.GetMyWidth())
                    rowCount++;
                else
                    colCount++;

            }

            if (rowCount <= 3 && colCount <= 3)
            {

                if (redCount <= 3 && blueCount <= 3 && greenCount <= 3 && yellowCount <= 3)
                {

                    higher = GetHigher(redCount, blueCount, greenCount, yellowCount);

                    int i = _currentTile.GetMyWidth();
                    int j = _currentTile.GetMyHeight();

                    if (rowCount > colCount)
                        m_board.SpawnBombTile(i, j, 1);
                    else
                        m_board.SpawnBombTile(i, j, 0);

                }
            }

        }
    }
    SCR_BombBehaviour.BombType GetHigher(int a, int b, int c, int d)
    {
        int[] val = { a, b, c, d };
        int high = a;
        int highpos = 0;

        for (int i = 0; i < val.Length; i++)
        {
            if (val[i] > high)
            {
                high = val[i];
                highpos = i;
            }
        }
        return (SCR_BombBehaviour.BombType)highpos;
    }
    IEnumerator FindBombMatch(int i, int j)
    {
        yield return new WaitForSeconds(m_findSpeed);

        SCR_BombBehaviour bomb = m_board.GetBombByPos(i, j);
        if (bomb != null)
        {
            if (bomb.GetBombTypeNum() == 0)
            {
                GetColumnTiles(i);
                Debug.Log("MATCH FOUND");
            }
            else if (bomb.GetBombTypeNum() == 1)
            {
                GetRowTiles(j);
                Debug.Log("MATCH FOUND");
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

    List<SCR_TileBehaviour> GetColumnTiles(int val)
    {
        List<SCR_TileBehaviour> tiles = new List<SCR_TileBehaviour>();

        for (int i = 0; i < m_board.GetBoardHeight(); i++)
        {
            if (m_board.GetTileByPos(val, i) != null)
            {
                tiles.Add(m_board.GetTileByPos(val, i));
                m_board.GetTileByPos(val, i).SetIsMatched(true);
            }
        }
        return tiles;
    }

    List<SCR_TileBehaviour> GetRowTiles(int val)
    {
        List<SCR_TileBehaviour> tiles = new List<SCR_TileBehaviour>();

        for (int i = 0; i < m_board.GetBoardWidth(); i++)
        {
            if (m_board.GetTileByPos(i, val) != null)
            {
                tiles.Add(m_board.GetTileByPos(i, val));
                m_board.GetTileByPos(i, val).SetIsMatched(true);
            }
        }
        return tiles;
    }

    bool CheckForT(List<SCR_TileBehaviour> tiles)
    {
        if (tiles.Count < 5)
            return false;

        return false;
    }

    public void SetCurrentTile(SCR_TileBehaviour ct)
    {
        _currentTile = ct;
    }

    public SCR_TileBehaviour GetCurrentTile()
    {
        return _currentTile;
    }
}

