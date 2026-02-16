using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public UIDocument uiDocument;


    private VisualElement root;
    private VisualElement checkpointImage;   // <-- VisualElement
    private Button continueButton;

    private void Awake()
    {
        root = uiDocument.rootVisualElement;

        checkpointImage = root.Q<VisualElement>("CheckpointImage");
        continueButton = root.Q<Button>("ContinueButton");

        root.style.display = DisplayStyle.None;

        continueButton.clicked += OnContinueClicked;
    }

    public void ShowImage(Sprite sprite)
    {
        checkpointImage.style.backgroundImage = new StyleBackground(sprite);
        root.style.display = DisplayStyle.Flex;

        // FREEZE PLAYER: Stop time and unlock cursor
        Time.timeScale = 0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    private void OnContinueClicked()
    {
        root.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked; // Relock for driving
        UnityEngine.Cursor.visible = false;
    }
}
