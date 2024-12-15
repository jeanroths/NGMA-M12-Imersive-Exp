using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketObjectChecker : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;

    private void Awake()
    {
        // Obtém o XRSocketInteractor anexado a este GameObject
        socketInteractor = GetComponent<XRSocketInteractor>();

        // Verifica se o Socket Interactor existe
        if (socketInteractor != null)
        {
            // Inscreve-se nos eventos
            socketInteractor.selectEntered.AddListener(OnObjectEntered);
            socketInteractor.selectExited.AddListener(OnObjectExited);
        }
    }

    private void OnDestroy()
    {
        // Remove os eventos para evitar vazamentos de memória
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectEntered);
            socketInteractor.selectExited.RemoveListener(OnObjectExited);
        }
    }

    // Método chamado quando um objeto entra no socket
    private void OnObjectEntered(SelectEnterEventArgs args)
    {
        // Obtém o objeto interativo
        GameObject collidingObject = args.interactableObject.transform.gameObject;

        // Exibe o nome do objeto no console
        Debug.Log($"Objeto inserido no socket: {collidingObject.name}");
    }

    // Método chamado quando um objeto sai do socket
    private void OnObjectExited(SelectExitEventArgs args)
    {
        // Obtém o objeto interativo
        GameObject collidingObject = args.interactableObject.transform.gameObject;

        // Exibe no console que o objeto foi removido
        Debug.Log($"Objeto removido do socket: {collidingObject.name}");
    }
}
