using UnityEngine;

public class Building : MonoBehaviour {
    public Renderer mainRenderer;
    
    Color startColor;
    
    private void Awake() {
        startColor = mainRenderer.material.color;
    }

    public void setTransparent(bool avalable) {
        mainRenderer.material.color = avalable ? Color.green : Color.red;
    }

    public void setNormal() {
        mainRenderer.material.color = startColor;
    }
}
