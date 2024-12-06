using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BoardManager : MonoBehaviour
{
    int _width;
    int _height;
    [Header("Grid Objects")]
    [SerializeField] SCR_GridInfo m_gridInfo;
    [SerializeField] GameObject m_tilePrefab;
    [SerializeField] GameObject[] m_bombPrefab;
    [SerializeField] GameObject m_board;

    [Header("Settings")]
    [SerializeField] bool m_isScripted;
    [SerializeField] float m_decreaseHeightSpeed = 0.4f;
    [SerializeField] float m_fillBoardSpeed = 0.3f;
    [SerializeField] float m_offset = 0.6f;

    [Header("Game Reference")]
    [SerializeField] SCR_MatchFinder m_matchFinder;

    SCR_TileBehaviour[,] m_allTiles;

    Vector2 _pos;

    void SetUpInfo()
    {
        _width = m_gridInfo.GetWidth();
        _height = m_gridInfo.GetHeight();
    }

    void Start()
    {
        SetUpInfo();

        if (!m_isScripted)
        {
            SetUpInfo();
            SetUpRandomGrid();
        }
        else
        {
            SetUpScriptedGrid();
        }
    }

    void SetUpRandomGrid()
    {
        m_allTiles = new SCR_TileBehaviour[_width, _height];

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                SpawnTile(i, j);
            }
        }
    }
    void SetUpScriptedGrid()
    {

    }
    void SpawnTile(int i, int j, bool useOffset = false, bool random = true)
    {
        if (useOffset)
            _pos = new Vector2(i, j + m_offset);
        else
            _pos = new Vector2(i, j);

        GameObject tile = Instantiate(m_tilePrefab, _pos, Quaternion.identity); ;

        tile.transform.parent = m_board.transform;
        tile.transform.name = "(" + i + "," + j + ")";

        SCR_TileBehaviour behaviour = tile.GetComponent<SCR_TileBehaviour>();
        m_allTiles[i, j] = behaviour;

        behaviour.SetBoard(this);
        behaviour.SetMatchFinder(m_matchFinder);
        behaviour.SetMyWidth(i);
        behaviour.SetMyHeight(j);

        if (random)
        {
            int num = Random.Range(0, 3);
            behaviour.SetType(num);

            while (behaviour.CheckMatches(i, j))
            {
                num = Random.Range(0, 3);
                behaviour.SetType(num);
            }

            behaviour.SetUpColor(num);
        }
        else
        {
            behaviour.SetUpColor(behaviour.GetMyTypeNum());
        }
    }

    public void SpawnBombTile(int i, int j, int type, bool useOffset = false)
    {
        Debug.Log("SPAWN BOMBA NA " + i + " " + j);
        if (useOffset)
            _pos = new Vector2(i, j + m_offset);
        else
            _pos = new Vector2(i, j);

        GameObject tile = Instantiate(m_bombPrefab[type], _pos, Quaternion.identity); ;

        tile.transform.parent = m_board.transform;
        tile.transform.name = "(" + i + "," + j + ")" + " B";

        SCR_BombBehaviour behaviour = tile.GetComponent<SCR_BombBehaviour>();

        m_allTiles[i, j] = behaviour;

        behaviour.SetBoard(this);
        behaviour.SetMatchFinder(m_matchFinder);
        behaviour.SetMyWidth(i);
        behaviour.SetMyHeight(j);
        behaviour.SetBombType(type);
        behaviour.SetIsMatched(false);
    }

    public void DestroyMatchesAt(int i, int j)
    {
        if (m_allTiles[i, j].GetIsMatched())
        {
            m_matchFinder.RemoveFromList(m_allTiles[i, j]);

            Destroy(m_allTiles[i, j].gameObject);
            m_allTiles[i, j] = null;
           
        }
    }
    public void DestroyMatches()
    {
       // m_matchFinder.Check4More();
        // SCR_GameState.SetCurrentGameState(GameState.wait);
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (m_allTiles[i, j] != null)
                    DestroyMatchesAt(i, j);
            }
        }
        StartCoroutine(DecreaseRow());
    }
    IEnumerator DecreaseRow()
    {
        int nullCount = 0;

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (m_allTiles[i, j] == null)
                {
                    nullCount++;
                }
                else
                {
                    m_allTiles[i, j].DecreaseHeight(nullCount);
                    m_allTiles[i, j] = null;
                }

            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(m_decreaseHeightSpeed);

        StartCoroutine(FillBoard());
    }
    void RefillBoard()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (m_allTiles[i, j] == null)
                    SpawnTile(i, j, true);
            }
        }
    }
    bool MatchesOnBoard()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (m_allTiles[i, j] != null)
                    if (m_allTiles[i, j].GetIsMatched())
                        return true;
            }
        }
        return false;
    }

    IEnumerator FillBoard()
    {
        RefillBoard();
        yield return new WaitForSeconds(m_fillBoardSpeed);
        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(m_fillBoardSpeed);
            DestroyMatches();
        }

        yield return new WaitForSeconds(m_fillBoardSpeed);

        m_matchFinder.FindMatches();
        SCR_GameState.SetCurrentGameState(GameState.move);
    }

    public SCR_TileBehaviour GetTileByPos(int i, int j)
    {
        return m_allTiles[i, j];
    }
    public SCR_BombBehaviour GetBombByPos(int i, int j)
    {
        return m_allTiles[i, j].GetComponent<SCR_BombBehaviour>();
    }

    public void SetTileByPos(int i, int j, SCR_TileBehaviour tile)
    {
        m_allTiles[i, j] = tile;
    }

    public int GetBoardWidth()
    {
        return _width;
    }
    public int GetBoardHeight()
    {
        return _height;
    }
}
