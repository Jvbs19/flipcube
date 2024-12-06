using UnityEngine;

[CreateAssetMenu(fileName = "Board", menuName = "Board/New Grid")]
public class SCR_GridInfo : ScriptableObject
{
   [Header("Level Status")]
   [SerializeField] int level;
   [SerializeField] int width;
   [SerializeField] int height;

    public int GetName()
    {
        return level;
    }
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
}
