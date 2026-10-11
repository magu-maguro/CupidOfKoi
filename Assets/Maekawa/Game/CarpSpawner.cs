using UnityEngine;
using System.Collections.Generic;

public class CarpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carpPrefab; //鯉のプレハブ
    [SerializeField, Tooltip("0番目が正解、それ以外はテキトーに")] private List<Material> bodyMaterials;
    [SerializeField, Tooltip("0番目が正解、それ以外はテキトーに")] private List<Material> eyeMaterials;
    private List<GameObject> spawnedCarps = new List<GameObject>();
    public int selectedCarpIndex = -1; // 選択された鯉のインデックス
    public void SpawnCarps(int koiCount)
    {
        //鯉のスポーン処理
        for (int i = 0; i < koiCount; i++)
        {
            //鯉のプレハブをランダムな位置に生成する処理をここに追加
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0f);
            GameObject carp = Instantiate(carpPrefab, randomPosition, Quaternion.identity);
            carp.transform.SetParent(transform); // CarpSpawnerの子として設定
            ApplyCarpMaterials(carp, i); // 鯉のマテリアルを適用
            spawnedCarps.Add(carp);
        }
    }

    private void ApplyCarpMaterials(GameObject carp, int index)
    {
        //子オブジェクトを全て順に取得、0, 1番目に目のマテリアル、2番目に体のマテリアルセット
        MeshRenderer[] renderers = carp.GetComponentsInChildren<MeshRenderer>();
        if (index == 0)
        {
            renderers[0].material = eyeMaterials[0]; // 目のマテリアルを適用
            renderers[1].material = eyeMaterials[0]; // 目のマテリアルを適用
            renderers[2].material = bodyMaterials[0]; // 体のマテリアルを適用
        }
        else
        {
            renderers[0].material = eyeMaterials[Random.Range(1, eyeMaterials.Count)]; // ランダムな目のマテリアルを適用
            renderers[1].material = eyeMaterials[Random.Range(1, eyeMaterials.Count)]; // ランダムな目のマテリアルを適用
            renderers[2].material = bodyMaterials[Random.Range(1, bodyMaterials.Count)]; // ランダムな体のマテリアルを適用
        }
    }

    public void KoiSelected(GameObject selectedCarp)
    {
        selectedCarpIndex = selectedCarp.transform.GetSiblingIndex(); // 選択された鯉のインデックスを取得
        if (selectedCarpIndex == 0)
        {
            GameManager.Instance.isCleared = true;
        }
        else
        {
            GameManager.Instance.isCleared = false;
        }
        MeshRenderer[] renderers = selectedCarp.GetComponentsInChildren<MeshRenderer>();
        GameManager.Instance.SetSelectedMaterials(renderers[2].material, renderers[0].material); // 体のマテリアルと目のマテリアルをGameManagerに渡す
        GameManager.Instance.gameEnded = true;
    }

    public void ClearCarps()
    {
        foreach (var carp in spawnedCarps)
        {
            Destroy(carp);
        }
        spawnedCarps.Clear();
    }
}
