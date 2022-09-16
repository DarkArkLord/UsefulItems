using NeuralNetworkCore;
using NeuralNetworkCore.BackPropagationNeuralNetwork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkConsole
{
    class Program
    {
        static void CheckXor()
        {
            DataSet[] data = new DataSet[4];
            data[0] = DataSet.Make().SetInput(0, 0).SetOutput(0);
            data[1] = DataSet.Make().SetInput(0, 1).SetOutput(1);
            data[2] = DataSet.Make().SetInput(1, 0).SetOutput(1);
            data[3] = DataSet.Make().SetInput(1, 1).SetOutput(0);

            Topology topology = new Topology
            {
                LearningRate = 0.7,
                Moment = 0.3,
                Layers = new int[] { 2, 4, 1 }
            };

            //NeuralNetwork network = NeuralNetwork.GenerateNetwork(topology, data, 1e-6);
            NeuralNetwork network = new NeuralNetwork(topology);
            double max_error = 1.0e-5;
            double min_delta_error = 1.0e-20;
            double current_mid_error = double.MaxValue;
            int epoch = 0;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            do
            {
                epoch++;
                double t_error = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    var set = data[i];
                    t_error += network.ChechErrorWithFeed(set.Input, set.Output);
                    network.Learn(set.Output);
                }
                t_error /= data.Length;

                if (epoch % 1000 == 0)
                {
                    Console.WriteLine($"Epoch {epoch}");
                    Console.WriteLine(network);
                }

                double t_delta = current_mid_error - t_error;
                if (t_delta < min_delta_error)
                {
                    Console.WriteLine("Use new\n");

                    epoch = 0;
                    network = new NeuralNetwork(topology);
                    current_mid_error = double.MaxValue;
                }
                else
                {
                    current_mid_error = t_error;
                }
            }
            while (current_mid_error >= max_error);

            timer.Stop();
            Console.WriteLine(network);
            Console.WriteLine($"Time: {timer.ElapsedMilliseconds / 1000.0} s");
            timer.Reset();

            foreach (var set in data)
            {
                Console.WriteLine(network.FeedForward(set.Input)[0]);
            }
            Console.WriteLine();

            // ***** SAVE *****
            //BinaryFormatter formatter = new BinaryFormatter();
            //using (FileStream fs = new FileStream("t.bnn", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, network);
            //}

            // ***** LOAD *****
            //using (FileStream fs = new FileStream("t.bnn", FileMode.OpenOrCreate))
            //{
            //    network = (NeuralNetwork)formatter.Deserialize(fs);
            //}
        }

        static void CheckDigits()
        {
            FileStream fs = new FileStream("log.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(fs);
            DateTime dt = DateTime.Now;
            string s = $"Start {dt.Hour}:{dt.Minute}:{dt.Second}";
            Console.WriteLine(s);
            writer.WriteLine(s);

            DirectoryInfo directory = new DirectoryInfo("train");
            FileInfo[] all_files = directory.GetFiles();
            DataSet[] train_data = new DataSet[all_files.Length];

            for (int file_index = 0; file_index < train_data.Length; file_index++)
            {
                FileInfo file = all_files[file_index];
                Bitmap bitmap = new Bitmap(file.FullName);
                int w = bitmap.Width;
                int h = bitmap.Height;
                double[] input = new double[w * h];
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        int i = x * h + y;
                        Color color = bitmap.GetPixel(x, y);
                        double t = (color.R + color.G + color.B) / 3.0;
                        t /= byte.MaxValue;
                        input[i] = t;
                    }
                }

                string name = file.Name;
                int num = (name[name.Length - 5] - '0');
                double[] output = new double[10];
                output[num] = 1;

                train_data[file_index] = new DataSet
                {
                    Input = input,
                    Output = output
                };
            }

            dt = DateTime.Now;
            s = $"Data inited {dt.Hour}:{dt.Minute}:{dt.Second}";
            Console.WriteLine(s);
            writer.WriteLine(s);

            Topology topology = new Topology
            {
                LearningRate = 0.7,
                Moment = 0.3,
                Layers = new int[] { 784, 512, 128, 32, 10 }
            };

            //NeuralNetwork network = NeuralNetwork.GenerateNetwork(topology, train_data, 0.9);
            NeuralNetwork network = new NeuralNetwork(topology);

            int one_ten_part = train_data.Length / 100;
            s = $"Epoch started";
            Console.WriteLine(s);
            writer.WriteLine(s);
            double t_error = 0;
            for (int i = 0; i < train_data.Length; i++)
            {
                if (i % one_ten_part == 0)
                {
                    dt = DateTime.Now;
                    s = $"Learn {i / one_ten_part}% {dt.Hour}:{dt.Minute}:{dt.Second}";
                    Console.WriteLine(s);
                    writer.WriteLine(s);
                }

                var set = train_data[i];
                t_error += network.ChechErrorWithFeed(set.Input, set.Output);
                network.Learn(set.Output);
            }
            t_error /= train_data.Length;
            Console.WriteLine();
            writer.WriteLine();
            s = $"Epoch finished";
            Console.WriteLine(s);
            writer.WriteLine(s);
            s = $"Mid error: {t_error}";
            Console.WriteLine(s);
            writer.WriteLine(s);
            Console.WriteLine();
            writer.WriteLine();

            dt = DateTime.Now;
            s = $"NN learned {dt.Hour}:{dt.Minute}:{dt.Second}";
            Console.WriteLine(s);
            writer.WriteLine(s);

            //BinaryFormatter formatter = new BinaryFormatter();

            //// ***** SAVE *****

            //using (FileStream fls = new FileStream("t.bnn", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fls, network);
            //}

            //// ***** LOAD *****

            //using (FileStream fls = new FileStream("t.bnn", FileMode.OpenOrCreate))
            //{
            //    network = (NeuralNetwork)formatter.Deserialize(fls);
            //}

            directory = new DirectoryInfo("test");
            all_files = directory.GetFiles();
            DataSet[] check_data = new DataSet[all_files.Length];

            for (int file_index = 0; file_index < check_data.Length; file_index++)
            {
                FileInfo file = all_files[file_index];
                Bitmap bitmap = new Bitmap(file.FullName);
                int w = bitmap.Width;
                int h = bitmap.Height;
                double[] input = new double[w * h];
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        int i = x * h + y;
                        Color color = bitmap.GetPixel(x, y);
                        double t = (color.R + color.G + color.B) / 3.0;
                        t /= byte.MaxValue;
                        input[i] = t;
                    }
                }

                string name = file.Name;
                int num = (name[name.Length - 5] - '0');
                double[] output = new double[10];
                output[num] = 1;

                check_data[file_index] = new DataSet
                {
                    Input = input,
                    Output = output
                };
            }

            dt = DateTime.Now;
            s = $"Data inited {dt.Hour}:{dt.Minute}:{dt.Second}";
            Console.WriteLine(s);
            writer.WriteLine(s);

            double error = 0;
            one_ten_part = check_data.Length / 100;
            for(int i = 0; i < check_data.Length; i++)
            {
                var set = check_data[i];
                if (i % one_ten_part == 0)
                {
                    dt = DateTime.Now;
                    s = $"Check {i / one_ten_part}% {dt.Hour}:{dt.Minute}:{dt.Second}";
                    Console.WriteLine(s);
                    writer.WriteLine(s);
                }
                error += network.ChechErrorWithFeed(set.Input, set.Output);
            }
            error /= train_data.Length;

            dt = DateTime.Now;
            s = $"Result {dt.Hour}:{dt.Minute}:{dt.Second}";
            Console.WriteLine(s);
            writer.WriteLine(s);
            s = error.ToString();
            Console.WriteLine(s);
            writer.WriteLine(s);

            writer.Close();
            if (fs != null) fs.Close();
        }

        static void Main(string[] args)
        {
            CheckXor();
            //CheckDigits();

            Console.WriteLine("\nend");
            Console.ReadKey();
        }
    }
}
