using UnityEngine;

public class SCR_BoardManager : MonoBehaviour
{
    int _width;
    int _height;
    [Header("Grid Objects")]
    [SerializeField] SCR_GridInfo m_gridInfo;
    [SerializeField] GameObject m_tilePrefab;
    [SerializeField] GameObject m_board;

    [Header("Settings")]
    [SerializeField] bool m_isScripted;

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
                _pos = new Vector2(i, j);
                
                GameObject tile = Instantiate(m_tilePrefab, _pos, Quaternion.identity);;
                
                tile.transform.parent = m_board.transform;
                tile.transform.name = "(" + i + "," + j + ")";

                SCR_TileBehaviour behaviour = tile.GetComponent<SCR_TileBehaviour>();
                m_allTiles[i, j] = behaviour;

                behaviour.SetBoard(this);
                behaviour.SetRandomBehaviour(true);
                behaviour.SetMyWidth(i);
                behaviour.SetMyHeight(j);
            }
        }
    }

    void SetUpScriptedGrid()
    { 
    }
    public SCR_TileBehaviour GetTileByPos(int i, int j)
    {
        return m_allTiles[i, j];
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
