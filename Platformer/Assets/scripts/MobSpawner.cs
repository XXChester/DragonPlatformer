using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MobSpawner : MonoBehaviour {
	private struct SpawnData {
		public GameObject Spawn { get; set; }
		public List<GameObject> Waypoints { get; set; }
	}

	private Score score;
	private List<SpawnData> spawnDatas;

	public GameObject spawnPrefab;

	private const int SPAWN_DELAY_MULTIPLIER = 1;

	private List<GameObject> FindWaypoints(GameObject parent) {
		List<GameObject> nodes = null;
		if (parent != null) {

			Transform[] children = parent.GetComponentsInChildren<Transform>();
			if (children.Length < 2 && parent.transform.parent != null) {
				nodes = FindWaypoints(parent.transform.parent.gameObject);
			} else {
				bool foundSearchStop = false;
				foreach (var child in children) {
					if ("Waypoint".Equals(child.gameObject.tag)) {
						if (nodes == null) {
							nodes = new List<GameObject>();
						}
						nodes.Add(child.gameObject);
					} else if ("SearchStop".Equals(child.gameObject.tag)) {
						foundSearchStop = true;
					}
				}
				// The node may have had children, but none were waypoints
				if (parent.transform.parent != null && (nodes == null || nodes.Count < 2)) {
					if (!foundSearchStop) {
						nodes = FindWaypoints(parent.transform.parent.gameObject);
					}
				}
			}
		}
		return nodes;
	}

	// Use this for initialization
	void Start() {
		score = LoadingUtils.LoadAndValidate<Score>(gameObject);
		LoadingUtils.Validate<GameObject>(this.spawnPrefab);

		this.spawnDatas = new List<SpawnData>();
		GameObject[] spawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");
		foreach (var spawn in spawnLocations) {
			List<GameObject> nodes = FindWaypoints(spawn.gameObject.transform.parent.gameObject);
			SpawnData data = new SpawnData() {
				Spawn = spawn,
				Waypoints = nodes
			};
			spawnDatas.Add(data);
		}
	}

	public void StartGame() {
		StartCoroutine(SpawnMobs());
	}

	IEnumerator SpawnMobs() {
		yield return new WaitForSeconds(2f);
		while (true) {
			int index = Random.Range(0, spawnDatas.Count - 1);
			SpawnData spawnData = spawnDatas[index];
			Vector3 spawnPosition = spawnData.Spawn.transform.position;

			Quaternion rotation = Quaternion.identity;
			GameObject spawned = (GameObject)Instantiate(spawnPrefab, spawnPosition, rotation);
			spawned.GetComponentInChildren<Health>().Score = score;
			WaypointPathing pathing = spawned.GetComponent<WaypointPathing>();
			// if we are not a spawn location that has way points, remove the pathing component
			if (spawnData.Waypoints == null) {
				Destroy(pathing);
			} else {
				pathing.Nodes = spawnData.Waypoints;
			}

			yield return new WaitForSeconds(Random.Range(1f * SPAWN_DELAY_MULTIPLIER, 4f * SPAWN_DELAY_MULTIPLIER));
		}
	}
}
