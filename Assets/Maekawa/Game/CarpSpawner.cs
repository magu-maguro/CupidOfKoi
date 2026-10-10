using UnityEngine;

public class CarpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carpPrefab; //鯉のプレハブ
    public void SpawnCarps(int koiCount)
    {
        //鯉のスポーン処理
        for (int i = 0; i < koiCount; i++)
        {
            //鯉のプレハブをランダムな位置に生成する処理をここに追加
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0f);
            Instantiate(carpPrefab, randomPosition, Quaternion.identity);
        }
    }
}
