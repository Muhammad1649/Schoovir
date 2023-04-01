using System.Collections.Generic;
using UnityEngine;

public class AssistantAI : MonoBehaviour {

    // INPUTS
    public static int 
    RIGHT_HAND_GRABBING, LEFT_HAND_GRABBING,
    BEAKER_GRABBED, BEAKER_POURING, BEAKER_RECEIVING,
    BURETTE_GRABBED, BURETTE_POURING, BURETTE_RECEIVING,
    CONICAL_FLASK_GRABBED, CONICAL_FLASK_POURING, CONICAL_FLASK_RECEIVING,
    VOLUMETRIC_PIPETTE_GRABBED, VOLUMETRIC_PIPETTE_POURING, VOLUMETRIC_PIPETTE_RECEIVING,
    GRADUATED_CYLINDER_GRABBED, GRADUATED_CYLINDER_POURING, GRADUATED_CYLINDER_RECEIVING,
    RETHORD_STAND_GRABBED, BURETTE_CLAMP_GRABBED, BURETTE_CLAMP_ATTACHED,
    WEIGHT_BALANCE_GRABBED, WEIGHT_BALANCE_WEIGHING,
    GAS_CYLINDER_GRABBED, GAS_CYLINDER_ATTACHED,
    TRASH_CAN_GRABBED, TRASH_CAN_DUMPED,
    CHEMICAL_BOTTLE_GRABBED, CHEMICAL_BOTTLE_OPENED, CHEMICAL_BOTTLE_POURING,
    FILTER_FUNNEL_GRABBED, FILTER_FUNNEL_RECEIVING, FILTER_FUNNEL_ATTACHED,
    SOLID_GRABBED, SOLID_POURED;
    // INPUTS


    // OUTPUT REFERANCE
    private static string[] outputReferance = {
    "GRABBING", "POURING", "RECEIVING", "ATTACHED", "DUMPED", "OPENED",
    "BEAKER", "BURETTE", "CONICAL_FLASK", "VOLUMETRIC PIPETTE", "GRADUATED_CYLINDER",
    "RETHORD_STAND", "BURETTE_CLAMP", "WEIGHT_BALANCE", "GAS_CYLINDER", "TRASH_CAN", "CHEMICAL_BOTTLE", "FILTER_FUNNEL",
    "SOLID"};
    // OUTPUT REFERANCE

    private static float[] outputs = new float[19];

    public static void TriggerOn(string field) {
        AssistantAI baseClass = new AssistantAI();
        var type = baseClass.GetType().GetField(field);

        if (type != null) {
            type.SetValue(baseClass, 1);

            ActivityDetectionNeuralNetwork.SetupNeuralNetwork(new List<int>() {34, 40, 40, 20, 20});
            outputs =  ActivityDetectionNeuralNetwork.Input(new float[34] {RIGHT_HAND_GRABBING,
            LEFT_HAND_GRABBING,
            BEAKER_GRABBED,
            BEAKER_POURING,
            BEAKER_RECEIVING,
            BURETTE_GRABBED,
            BURETTE_POURING,
            BURETTE_RECEIVING,
            CONICAL_FLASK_GRABBED,
            CONICAL_FLASK_POURING,
            CONICAL_FLASK_RECEIVING,
            VOLUMETRIC_PIPETTE_GRABBED,
            VOLUMETRIC_PIPETTE_POURING,
            VOLUMETRIC_PIPETTE_RECEIVING,
            GRADUATED_CYLINDER_GRABBED,
            GRADUATED_CYLINDER_POURING,
            GRADUATED_CYLINDER_RECEIVING,
            RETHORD_STAND_GRABBED,
            BURETTE_CLAMP_GRABBED,
            BURETTE_CLAMP_ATTACHED,
            WEIGHT_BALANCE_GRABBED,
            WEIGHT_BALANCE_WEIGHING,
            GAS_CYLINDER_GRABBED,
            GAS_CYLINDER_ATTACHED,
            TRASH_CAN_GRABBED,
            TRASH_CAN_DUMPED,
            CHEMICAL_BOTTLE_GRABBED,
            CHEMICAL_BOTTLE_OPENED,
            CHEMICAL_BOTTLE_POURING,
            FILTER_FUNNEL_GRABBED,
            FILTER_FUNNEL_RECEIVING,
            FILTER_FUNNEL_ATTACHED,
            SOLID_GRABBED,
            SOLID_POURED});
            
            ProcessOutput();
            return;
        }

        Debug.Log("An object is trying to trigger a field that dose'nt exist ( '" + field + "' )");
    }

    public static void TriggerOff(string field) {
        AssistantAI baseClass = new AssistantAI();
        var type = baseClass.GetType().GetField(field);

        if (type != null) {
            type.SetValue(baseClass, 0);

            ActivityDetectionNeuralNetwork.SetupNeuralNetwork(new List<int>() {34, 40, 40, 20, 20});
            outputs =  ActivityDetectionNeuralNetwork.Input(new float[34] {RIGHT_HAND_GRABBING,
            LEFT_HAND_GRABBING,
            BEAKER_GRABBED,
            BEAKER_POURING,
            BEAKER_RECEIVING,
            BURETTE_GRABBED,
            BURETTE_POURING,
            BURETTE_RECEIVING,
            CONICAL_FLASK_GRABBED,
            CONICAL_FLASK_POURING,
            CONICAL_FLASK_RECEIVING,
            VOLUMETRIC_PIPETTE_GRABBED,
            VOLUMETRIC_PIPETTE_POURING,
            VOLUMETRIC_PIPETTE_RECEIVING,
            GRADUATED_CYLINDER_GRABBED,
            GRADUATED_CYLINDER_POURING,
            GRADUATED_CYLINDER_RECEIVING,
            RETHORD_STAND_GRABBED,
            BURETTE_CLAMP_GRABBED,
            BURETTE_CLAMP_ATTACHED,
            WEIGHT_BALANCE_GRABBED,
            WEIGHT_BALANCE_WEIGHING,
            GAS_CYLINDER_GRABBED,
            GAS_CYLINDER_ATTACHED,
            TRASH_CAN_GRABBED,
            TRASH_CAN_DUMPED,
            CHEMICAL_BOTTLE_GRABBED,
            CHEMICAL_BOTTLE_OPENED,
            CHEMICAL_BOTTLE_POURING,
            FILTER_FUNNEL_GRABBED,
            FILTER_FUNNEL_RECEIVING,
            FILTER_FUNNEL_ATTACHED,
            SOLID_GRABBED,
            SOLID_POURED});
            
            ProcessOutput();
            return;
        }

        Debug.Log("An object is trying to trigger a field that dose'nt exist ( '" + field + "' )");
    }

    private static void ProcessOutput() {
        List<string> processedOutput = new List<string>();
        for (int i = 0; i < outputs.Length - 1; i++) {
            if (outputs[i] > 0.9f) {
                processedOutput.Add(outputReferance[i] + " [" + outputs[i] + "]");
            }
        }
        // Debug.LogWarning(processedOutput.Count + " Outputs");
        DebugLog(processedOutput);
    }

    private static void DebugLog(List<string> log) {
        foreach(string i in log) {
            // Debug.LogWarning(i);
        }
    }  
}


