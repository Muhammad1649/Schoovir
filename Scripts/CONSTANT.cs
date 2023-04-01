using UnityEngine;

public class CONSTANT : MonoBehaviour {
    public static Vector3 playerPosition;

    public enum ExparimentMode {Learn, Test};
    public static ExparimentMode exparimentMode;

    // Test Parameters
    public static bool measureAccuracy = true;
    public static bool measureDelay = true;
    public static bool measureNeatness = true;
    public static bool displayTime = true;

    // Prompt Parameters
    public enum PromptType {Choice, Loading, Message, Warning, Error,};
    public static float promptMessageTimeout = 2f;
    public static float promptWarningTimeout = 3f;
    public static float promptErrorTimeout = 4f;
}

public class Enums {
    
    
}
