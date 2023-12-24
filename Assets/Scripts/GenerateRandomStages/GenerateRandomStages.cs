using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;
using System.Linq;


public class GenerateRandomStages : MonoBehaviour
{
	private string dirPath = "Scripts/GenerateRandomStages";
	private string outputFile = "stage.txt";
	public Tilemap[] tilePrefabs;
	public List<CharGameObjectPair> charGameObjectPair;
	private Dictionary<char, GameObject> charGameObjectMap;
	private int mapHeight = 14;
	//元となるテンプレートの数
	private int fileCount = 20;
	//つなげるファイル数
	private int totalFilesToCombine = 8;
	void Start()
	{
		GenerateStage();
	}

	public void GenerateStage()
	{
		MapAssociation();
		RenderMap();
	}

	public void MapAssociation()
	{
		List<string> combinedLines = new List<string>();

		// 最初の地形情報を読み込み
		AddTemplateFile(combinedLines, Path.Combine(Application.dataPath, dirPath + "/tmpl", "tmpl_start.txt"), dirPath);

		// 中間の地形情報のテンプレートをランダムに読み込み
		List<int> shuffledIndices = new List<int>(Enumerable.Range(0, fileCount));
		System.Random rng = new System.Random();
		Debug.Log("~~~~~~~~~~~~~~~~~~~~");
		for (int i = 0; i < totalFilesToCombine; i++)
		{
			int index = shuffledIndices[rng.Next(shuffledIndices.Count)];
			shuffledIndices.Remove(index);
			string filename = Path.Combine(Application.dataPath, dirPath + "/tmpl", "tmpl_" + index + ".txt");
			Debug.Log($"tmpl_ {index} .txt");
			AddTemplateFile(combinedLines, filename, dirPath);
		}
		Debug.Log("~~~~~上記のファイル達を結合した~~~~~");

		// 最後の地形情報を読み込み
		AddTemplateFile(combinedLines, Path.Combine(Application.dataPath, dirPath + "/tmpl", "tmpl_end.txt"), dirPath);

		// 結果をファイルに書き込み
		string fullPathOutputFile = Path.Combine(Application.dataPath, dirPath, outputFile);
		using (StreamWriter writer = new StreamWriter(fullPathOutputFile))
		{
			foreach (string line in combinedLines)
			{
				writer.WriteLine(line);
			}
		}

		Debug.Log($"Stage file {outputFile} created successfully");
	}

	private void AddTemplateFile(List<string> combinedLines, string filename, string dirPath)
	{
		if (!File.Exists(filename))
		{
			Debug.Log($"{filename}ファイルが存在しない");
			filename = Path.Combine(Application.dataPath, dirPath + "/tmpl", "tmpl_default.txt");
		}

		string[] lines = File.ReadAllLines(filename);
		if (combinedLines.Count == 0)
		{
			combinedLines.AddRange(lines);
		}
		else
		{
			for (int j = 0; j < lines.Length; j++)
			{
				if (lines.Length != mapHeight)
				{
					Debug.LogError("mapの縦の長さが正しくない");
					Debug.LogError($"{lines.Length}!={mapHeight}");
				}
				combinedLines[j] += lines[j];
			}
		}
	}

	void RenderMap()
	{
		string fullPathOutputFile = Path.Combine(Application.dataPath, dirPath, outputFile);
		string[] lines = File.ReadAllLines(Path.Combine(Application.dataPath, fullPathOutputFile));
		int mapWidth = 10;
		//startとtitleのテンプレートの列の長さを足したもの
		int frontBackWidth = 16;
		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < (totalFilesToCombine * mapWidth + frontBackWidth); x++)
			{
				char mapChar = lines[y][x];
				if ('0' <= mapChar && mapChar <= '9')//tileの場合
				{
					Tilemap tilePrefab = GetTilemapPrefabForChar(mapChar);
					if (tilePrefab != null)
						Instantiate(tilePrefab, new Vector3(x, mapHeight - y - 1, 0), Quaternion.identity);
				}
				else if ('A' <= mapChar && mapChar <= 'Z')//障害物の場合
				{
					GameObject obstaclePrefab = GetObstaclePrefabForChar(mapChar);
					if (obstaclePrefab != null)
						Instantiate(obstaclePrefab, new Vector3(x, mapHeight - y - 1, 0), Quaternion.identity);
				}
			}
		}
	}

	Tilemap GetTilemapPrefabForChar(char c)
	{
		if (c == ' ' || c == '\t' || c == '\n')
			return null;

		int index = 0;
		index = c - '0' - 1; // 0から9までの文字に対応するプレハブを取得する場合
		if (index >= 0 && index < tilePrefabs.Length)
		{
			return tilePrefabs[index];
		}
		return null; // 該当するプレハブがない場合
	}

	GameObject GetObstaclePrefabForChar(char c)
	{
		if (c == ' ' || c == '\t' || c == '\n')
			return null;
		// ListからDictionaryへの変換
		charGameObjectMap = new Dictionary<char, GameObject>();
		foreach (var pair in charGameObjectPair)
		{
			charGameObjectMap[pair.character] = pair.gameObject;
		}
		//keyとvalueがあることをチェックする
		if (charGameObjectMap.ContainsKey(c) && charGameObjectMap[c] != null)
			return charGameObjectMap[c];
		return null;

	}
}
