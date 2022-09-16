using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkCore.BackPropagationNeuralNetwork
{
    [Serializable]
    public class NeuralNetwork
    {
        private Layer input_layer;
        private Layer[] layers;
        private Layer output_layer;

        private double learning_rate = 0.7;
        private double moment = 0.3;

        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        private double SigmoidDx(double x)
        {
            double t = x;// Sigmoid(x);
            return t * (1 - t);
        }

        public NeuralNetwork(double lr, double m, params int[] layers_cnt)
        {
            Random random = new Random();
            learning_rate = lr;
            moment = m;

            if (layers_cnt.Length < 2)
            {
                throw new ArgumentOutOfRangeException("layers_cnt.Length < 2");
            }

            layers = new Layer[layers_cnt.Length];
            int next_size = 0;
            for (int i = layers_cnt.Length - 1; i >= 0; i--)
            {
                layers[i] = new Layer(layers_cnt[i], next_size, random);
                next_size = layers_cnt[i];
            }

            input_layer = layers[0];
            output_layer = layers[layers.Length - 1];
        }

        public NeuralNetwork(Topology topology)
            : this(topology.LearningRate, topology.Moment, topology.Layers)
        {
            //
        }

        public double[] FeedForward(double[] input)
        {
            if (input.Length != input_layer.Size)
            {
                throw new Exception("Input dimension is different from the number of input neurons");
            }

            for (int i = 0; i < input_layer.Size; i++)
            {
                input_layer.Neurones[i] = input[i];
            }

            for (int layer_index = 1; layer_index < layers.Length; layer_index++)
            {
                Layer prev_layer = layers[layer_index - 1];
                Layer current = layers[layer_index];
                for (int cur_i = 0; cur_i < current.Size; cur_i++)
                {
                    current.Neurones[cur_i] = 0;
                    for (int prev_i = 0; prev_i < prev_layer.Size; prev_i++)
                    {
                        double signal = prev_layer.Neurones[prev_i];
                        double weight = prev_layer.Weights[prev_i, cur_i];
                        current.Neurones[cur_i] += signal * weight;
                    }
                    current.Neurones[cur_i] += prev_layer.Bias[cur_i];
                    current.Neurones[cur_i] = Sigmoid(current.Neurones[cur_i]);
                }
            }

            return output_layer.Neurones.ToArray();
        }
        #region WITH_FEED
        public double ChechErrorWithFeed(double[] input, double[] targets)
        {
            if (input.Length != input_layer.Size)
            {
                throw new Exception("Input dimension is different from the number of input neurons");
            }

            if (targets.Length != output_layer.Size)
            {
                throw new Exception("Targets dimension is different from the number of output neurons");
            }

            FeedForward(input);

            double error = 0;
            for (int i = 0; i < output_layer.Size; i++)
            {
                double t = targets[i] - output_layer.Neurones[i];
                t *= t;
                error += t;
            }
            error /= output_layer.Size;

            return error;
        }

        public double LearnWithFeed(double[] input, double[] targets)
        {
            if (input.Length != input_layer.Size)
            {
                throw new Exception("Input dimension is different from the number of input neurons");
            }

            if (targets.Length != output_layer.Size)
            {
                throw new Exception("Targets dimension is different from the number of output neurons");
            }

            double start_error = ChechErrorWithFeed(input, targets);

            for (int i = 0; i < output_layer.Size; i++)
            {
                double t = targets[i] - output_layer.Neurones[i];
                output_layer.Neurones[i] = t * SigmoidDx(output_layer.Neurones[i]);
            }

            for (int layer_index = layers.Length - 2; layer_index >= 0; layer_index--)
            {
                Layer next = layers[layer_index + 1];
                Layer current = layers[layer_index];

                for (int cur_i = 0; cur_i < current.Size; cur_i++)
                {
                    double t_neuron = SigmoidDx(current.Neurones[cur_i]);
                    double neuron_upd_mult = 0;

                    for (int next_i = 0; next_i < next.Size; next_i++)
                    {
                        neuron_upd_mult += current.Weights[cur_i, next_i] * next.Neurones[next_i];

                        double gradient = current.Neurones[cur_i] * next.Neurones[next_i];
                        double delta_weight = learning_rate * gradient + moment * current.LastDeltaWeights[cur_i, next_i];
                        current.Weights[cur_i, next_i] += delta_weight;
                        current.LastDeltaWeights[cur_i, next_i] = delta_weight;
                    }

                    t_neuron *= neuron_upd_mult;
                    current.Neurones[cur_i] = t_neuron;
                }

                for (int next_i = 0; next_i < next.Size; next_i++)
                {
                    double gradient = next.Neurones[next_i];
                    double delta_weight = learning_rate * gradient + moment * current.LastDeltaBias[next_i];
                    current.Bias[next_i] += delta_weight;
                    current.LastDeltaBias[next_i] = delta_weight;
                }
            }

            double end_error = ChechErrorWithFeed(input, targets);

            return start_error - end_error;
        }
        #endregion
        #region WOTHOUT_FEED
        public double ChechError(double[] targets)
        {
            if (targets.Length != output_layer.Size)
            {
                throw new Exception("Targets dimension is different from the number of output neurons");
            }

            double error = 0;
            for (int i = 0; i < output_layer.Size; i++)
            {
                double t = targets[i] - output_layer.Neurones[i];
                t *= t;
                error += t;
            }
            error /= output_layer.Size;

            return error;
        }

        public void Learn(double[] targets)
        {
            if (targets.Length != output_layer.Size)
            {
                throw new Exception("Targets dimension is different from the number of output neurons");
            }

            for (int i = 0; i < output_layer.Size; i++)
            {
                double t = targets[i] - output_layer.Neurones[i];
                output_layer.Neurones[i] = t * SigmoidDx(output_layer.Neurones[i]);
            }

            for (int layer_index = layers.Length - 2; layer_index >= 0; layer_index--)
            {
                Layer next = layers[layer_index + 1];
                Layer current = layers[layer_index];

                for (int cur_i = 0; cur_i < current.Size; cur_i++)
                {
                    double t_neuron = SigmoidDx(current.Neurones[cur_i]);
                    double neuron_upd_mult = 0;

                    for (int next_i = 0; next_i < next.Size; next_i++)
                    {
                        neuron_upd_mult += current.Weights[cur_i, next_i] * next.Neurones[next_i];

                        double gradient = current.Neurones[cur_i] * next.Neurones[next_i];
                        double delta_weight = learning_rate * gradient + moment * current.LastDeltaWeights[cur_i, next_i];
                        current.Weights[cur_i, next_i] += delta_weight;
                        current.LastDeltaWeights[cur_i, next_i] = delta_weight;
                    }

                    t_neuron *= neuron_upd_mult;
                    current.Neurones[cur_i] = t_neuron;
                }

                for (int next_i = 0; next_i < next.Size; next_i++)
                {
                    double gradient = next.Neurones[next_i];
                    double delta_weight = learning_rate * gradient + moment * current.LastDeltaBias[next_i];
                    current.Bias[next_i] += delta_weight;
                    current.LastDeltaBias[next_i] = delta_weight;
                }
            }
        }
        #endregion
        public static NeuralNetwork GenerateNetwork(Topology topology,
           DataSet[] data, double max_error = 1.0e-5, double min_delta_error = 1.0e-20)
        {
            NeuralNetwork network = new NeuralNetwork(topology);

            double current_mid_error = double.MaxValue;
            do
            {
                double t_error = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    var set = data[i];
                    t_error += network.ChechErrorWithFeed(set.Input, set.Output);
                    network.Learn(set.Output);
                }
                t_error /= data.Length;

                double t_delta = current_mid_error - t_error;
                if (t_delta < min_delta_error)
                {
                    network = new NeuralNetwork(topology);
                    current_mid_error = double.MaxValue;
                }
                else
                {
                    current_mid_error = t_error;
                }
            }
            while (current_mid_error >= max_error);

            return network;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < layers.Length - 1; i++)
            {
                builder.Append($"Layer {i}:\n");
                builder.Append(layers[i].ToString());
                builder.Append("\n");
            }
            return builder.ToString();
        }
    }
}
