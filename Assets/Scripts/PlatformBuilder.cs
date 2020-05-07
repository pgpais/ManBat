using UnityEngine;
using System.Collections;

public class PlatformBuilder : MonoBehaviour 
{
	public GameObject SpritePrefab;
	public GameObject BottomBorderPrefab;
	public GameObject LeftBorderPrefab;
	public GameObject RightBorderPrefab;

	public GameObject spritesEmptyContainer;
	public GameObject spritesContainer;

	[Header("Dimensions")]
	public int X;
	public int Y;

	void Start(){
		
	}

	public void BuildObject()
	{
		Vector3 position = Vector3.zero;

		if (spritesContainer == null) {
			Transform t = transform.FindChild ("SpritesContainer");
			if (t != null) {
				spritesContainer = t.gameObject;
			}
		}
		if (spritesContainer != null) {
			DestroyImmediate (spritesContainer);
		}
		spritesContainer = Instantiate (spritesEmptyContainer, transform.position, Quaternion.identity, this.transform);
		spritesContainer.name = "SpritesContainer";
		for (int y = 0; y < Y; y++) {
			for (int x = 0; x < X; x++) {
				position = new Vector3 (x, y, 0);
				Instantiate(SpritePrefab, position + transform.position, Quaternion.identity, spritesContainer.transform);
				if (y == 0) {
					Instantiate (BottomBorderPrefab, new Vector3 (x, 0 -0.5f, 0) + transform.position, Quaternion.identity, spritesContainer.transform);
				}
			}
			Instantiate (LeftBorderPrefab, new Vector3(-0.5f,0.375f + y,0) + transform.position , Quaternion.identity, spritesContainer.transform);
			Instantiate (RightBorderPrefab, new Vector3(-0.5f + X, y,0) + transform.position, Quaternion.identity, spritesContainer.transform);
		}
	}
}
