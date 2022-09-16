using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkCore.BackPropagationNeuralNetwork
{
    [Serializable]
    class Layer
    {
        public int Size { get; }
        public double[] Neurones;
        public double[,] Weights;
        public double[,] LastDeltaWeights;
        public double[] Bias;
        public double[] LastDeltaBias;

        public Layer(int size, int next_size)
        {
            Size = size;
            Neurones = new double[size];
            Weights = new double[size, next_size];
            LastDeltaWeights = new double[size, next_size];
            Bias = new double[next_size];
            LastDeltaBias = new double[next_size];

            for (int x = 0; x < size; x++)
            {
                Neurones[x] = 0;
                for (int y = 0; y < next_size; y++)
                {
                    Weights[x, y] = 0;
                    LastDeltaWeights[x, y] = 0;
                }
            }
            for (int y = 0; y < next_size; y++)
            {
                Bias[y] = 0;
                LastDeltaBias[y] = 0;
            }
        }

        public Layer(int size, int next_size, Random random)
        {
            Size = size;
            Neurones = new double[size];
            Weights = new double[size, next_size];
            LastDeltaWeights = new double[size, next_size];
            Bias = new double[next_size];
            LastDeltaBias = new double[next_size];

            for (int x = 0; x < size; x++)
            {
                Neurones[x] = 0;
                for (int y = 0; y < next_size; y++)
                {
                    Weights[x, y] = random.NextDouble();
                    LastDeltaWeights[x, y] = 0;
                }
            }
            for (int y = 0; y < next_size; y++)
            {
                Bias[y] = random.NextDouble();
                LastDeltaBias[y] = 0;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int x = 0; x < Weights.GetLength(0); x++)
            {
                for (int y = 0; y < Weights.GetLength(1); y++)
                {
                    builder.Append($"w[{x}, {y}] = {Weights[x, y]}; \n");
                }
            }
            for (int i = 0; i < Bias.Length; i++)
            {
                builder.Append($"b[{i}] = {Bias[i]}; \n");
            }
            return builder.ToString();
        }
    }
}
