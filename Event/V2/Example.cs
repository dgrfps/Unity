using UnityEngine;

public class ExampleEventListener : EventListener
{
    void Start()
    {
        // Inscreva-se no evento "example"
        EventManager.GetInstance().AddListener("example", this);
    }

    void OnDestroy()
    {
        // Cancele a inscrição no evento "example"
        EventManager.GetInstance().RemoveListener("example", this);
    }

    public override void OnEvent(string eventName, EventArgs args)
    {
        // Execute algum código aqui quando o evento "example" for acionado
        Debug.Log("Event " + eventName + " was triggered!");
    }
}
