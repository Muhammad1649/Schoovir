using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ActivityDetectionNeuralNetwork {
    private static readonly System.Random Random = new System.Random();

    public static float[][] values;
    public static float[][] biases;
    public static float[][][] weights;

    // Training
    public static float[][] desiredValues;
    public static float[][] biasesSmudge;
    public static float[][][] weightsSmudge;
    // Training
    
    private const float WeightDecay = 0.001f;
    private const float LearningRate = 1f;

    public static void SetupNeuralNetwork(IReadOnlyList<int> structure) {

        values = new float[structure.Count][];
        desiredValues = new float[structure.Count][];
        biases = new float[structure.Count][];
        biasesSmudge = new float[structure.Count][];
        weights = new float[structure.Count - 1][][];
        weightsSmudge = new float[structure.Count - 1][][];

        for (var i = 0; i < structure.Count; i++) {

            values[i] = new float[structure[i]];
            desiredValues[i] = new float[structure[i]];
            biases[i] = new float[structure[i]];
            biasesSmudge[i] = new float[structure[i]];
        }

        for (var i = 0; i < structure.Count - 1; i++) {

            weights[i] = new float[values[i + 1].Length][];
            weightsSmudge[i] = new float[values[i + 1].Length][];

            for (var j = 0; j < weights[i].Length; j++) {

                weights[i][j] = new float[values[i].Length];
                weightsSmudge[i][j] = new float[values[i].Length];

                for (var k = 0; k < weights[i][j].Length; k++) {
                    weights[i][j][k] = (float) Random.NextDouble() * Mathf.Sqrt(2f / weights[i][j].Length);
                }
            }
        }
    }

    public static float[] Input(float[] input) {

        for (var i = 0; i < values[0].Length; i++) values[0][i] = input[i];

        for (var i = 1; i < values.Length; i++)
        for (var j = 0; j < values[i].Length; j++) {

            values[i][j] = Sigmoid(Sum(values[i - 1], weights[i - 1][j]) + biases[i][j]);
            desiredValues[i][j] = values[i][j];
        }

        return values[values.Length - 1];
    }

    private static float Sum(IEnumerable<float> values, IReadOnlyList<float> weights) => values.Select((v, i) => v * weights[i]).Sum();

    private static float Sigmoid(float x) => 1f / (1f + (float) Math.Exp(-x));

    private static float HardSigmoid(float x)
    {
        if (x < -2.5f) {return 0; }
        if (x > 2.5f) { return 1; }
        return 0.2f * x + 0.5f;
    }

    // Train
    public static void Train(float[][] trainingInputs, float[][] trainingOutputs) {
        for (var i = 0; i < trainingInputs.Length; i++) {

            Input(trainingInputs[i]);

            for (var j = 0; j < desiredValues[desiredValues.Length - 1].Length; j++)
                desiredValues[desiredValues.Length - 1][j] = trainingOutputs[i][j];

            for (var j = values.Length - 1; j >= 1; j--) {
                
                for (var k = 0; k < values[j].Length; k++) {

                    var biasSmudge = SigmoidDerivative(values[j][k]) *
                                     (desiredValues[j][k] - values[j][k]);
                    biasesSmudge[j][k] += biasSmudge;
                    
                    for (var l = 0; l < values[j - 1].Length; l++) {

                        var weightSmudge = values[j - 1][l] * biasSmudge;
                        weightsSmudge[j - 1][k][l] += weightSmudge;
                        
                        var valueSmudge = weights[j - 1][k][l] * biasSmudge;
                        desiredValues[j - 1][l] += valueSmudge;
                    }
                }
            }
        }
        
        for (var i = values.Length - 1; i >= 1; i--) {

            for (var j = 0; j < values[i].Length; j++) {

                biases[i][j] += biasesSmudge[i][j] * LearningRate;
                biases[i][j] *= 1 - WeightDecay;
                biasesSmudge[i][j] = 0;

                for (var k = 0; k < values[i - 1].Length; k++) {
                    
                    weights[i - 1][j][k] += weightsSmudge[i - 1][j][k] * LearningRate;
                    weights[i - 1][j][k] *= 1 - WeightDecay;
                    weightsSmudge[i - 1][j][k] = 0;
                }

                desiredValues[i][j] = 0;
            }
        }
    }

    private static float SigmoidDerivative(float x) => x * (1 - x);
}