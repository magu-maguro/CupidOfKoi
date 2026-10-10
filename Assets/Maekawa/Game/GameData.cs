using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    //鯉の数、制限時間などのデータを格納
    public int koiCount;
    public float timeLimit;
}
