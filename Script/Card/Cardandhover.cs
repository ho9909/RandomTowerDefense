using UnityEngine;
//https://dot.net/core-sdk-vscode
public class Cardandhover : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Sprite[] objectSprites;

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = objectSprites[currentSpriteIndex];
    }

    private void OnMouseDown()
    {
        currentSpriteIndex++;
        if (currentSpriteIndex >= objectSprites.Length)
        {
            SpawnObject();
            Destroy(gameObject);
        }
        else
        {
            spriteRenderer.sprite = objectSprites[currentSpriteIndex];
        }
    }

    private void SpawnObject()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}
