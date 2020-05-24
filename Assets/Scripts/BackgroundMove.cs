using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private MeshRenderer renderer;
    [SerializeField] private float scrollSpeed;

    private void Start()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // renderer.material.SetTextureOffset(renderer.material.name, new Vector2(0, ));
        renderer.material.mainTextureOffset = new Vector2(0, -Time.time * scrollSpeed);
    }
}