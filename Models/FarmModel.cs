using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAnalogyApp.Models
{
    public class FarmModel
    {
        // Farm Properties
        public int Length { get; private set; }
        public int Width { get; private set; }
        public int TotalArea { get; private set; }
        public int CowArea { get; } = 3; // Area per cow in square meters
        public double CowValue { get; } = 1000; // Value of each cow

        // Paddock Properties
        public List<Paddock> Paddocks { get; private set; }

        public class Paddock
        {
            public string Name { get; set; }
            public int Length { get; set; }
            public int Width { get; set; }
            public int CowCount { get; set; }
            //public double[][] Coordinates { get; set; }
            public string Coordinates { get; set; }
        }

        public FarmModel(int length, int width)
        {
            SetFarmDimensions(length, width);
        }

        public void SetFarmDimensions(int length, int width)
        {
            Length = length;
            Width = width;
            TotalArea = length * width;
        }

        public int CalculateMaxCows()
        {
            return Fitcows(Length, Width);
        }

        private int Fitcows(int farmLength, int farmWidth)
        {
            double farmArea = farmLength * farmWidth,
                cowsAlongLength = 0, cowsAlongWidth = 0;

            if (farmLength % 2 == 0)
            {
                cowsAlongLength = farmLength / 2;
                cowsAlongWidth = farmWidth / 1.5;
            }
            else if (farmLength % 3 == 0)
            {
                cowsAlongLength = farmLength / 1.5;
                cowsAlongWidth = farmWidth / 2;
            }
            else if (farmWidth % 3 == 0)
            {
                cowsAlongWidth = farmWidth / 1.5;
                cowsAlongLength = farmLength / 2;
            }
            else if (farmWidth % 2 == 0)
            {
                cowsAlongWidth = farmWidth / 2;
                cowsAlongLength = farmLength / 1.5;
            }
            else
            {
                double arr1Length = Math.Floor(farmLength / 2.0);
                double arr1Width = Math.Floor(farmWidth / 1.5);
                double arr1Cows = arr1Length * arr1Width;

                double arr2Width = Math.Floor(farmWidth / 2.0);
                double arr2Length = Math.Floor(farmLength / 1.5);
                double arr2Cows = arr2Length * arr2Width;

                return (int)Math.Max(arr1Cows, arr2Cows);
            }

            return (int)(cowsAlongWidth * cowsAlongLength);
        }

        public (int, string) GeneratePaddocks(int cowsPerPaddock)
        {
            int onePaddockArea = cowsPerPaddock * CowArea;
            int noOfPaddocks = TotalArea / onePaddockArea;

            Paddocks = new List<Paddock>();
            var paddockNames = GeneratePaddockNames(noOfPaddocks);
            var paddockDimensions = CalculatePaddockDimensions(noOfPaddocks, onePaddockArea);
            var paddockCoordinates = CalculatePaddockCoordinates(noOfPaddocks, paddockDimensions);

            for (int i = 0; i < noOfPaddocks; i++)
            {
                Paddocks.Add(new Paddock
                {
                    //Name = paddockNames[i],
                    Name = GetPaddockName(paddockNames, i),
                    Length = paddockDimensions[i][0],
                    Width = paddockDimensions[i][1],
                    CowCount = cowsPerPaddock,
                    //Coordinates = paddockCoordinates[i]
                    Coordinates = GetPaddockCoordinates(paddockCoordinates[i])
                });
            }
            

            return (noOfPaddocks, $"{paddockDimensions[0][0]} X {paddockDimensions[0][1]}");
        }

        private string GetPaddockName( char[][] paddock, int index)
        {
            var name = string.Empty;
            foreach (var item in paddock[index])
            {
                name += item;
            }
            return name;
        }
        private string GetPaddockCoordinates(double[][] paddock)
        {
            var coord = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    coord += paddock[i][j] + ",";
                }
                coord += "-,";
            }
            return coord;
        }
        private char[][] GeneratePaddockNames(int noOfPaddocks)
        {
            char[][] paddocksList = new char[noOfPaddocks][];
            char[] alphabets = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int index = 0;
            int letterCount = 1;

            int[] positions = new int[letterCount];

            while (index < noOfPaddocks)
            {
                paddocksList[index] = new char[letterCount];
                for (int i = 0; i < letterCount; i++)
                {
                    paddocksList[index][i] = alphabets[positions[i]];
                }

                index++;

                // Increment the base-26 counter
                int pos = letterCount - 1;
                while (pos >= 0)
                {
                    positions[pos]++;
                    if (positions[pos] < alphabets.Length)
                        break;

                    positions[pos] = 0;
                    pos--;

                    if (pos < 0)
                    {
                        letterCount++;
                        positions = new int[letterCount];
                    }
                }
            }

            return paddocksList;
        }

        private int[][] CalculatePaddockDimensions(int noOfPaddocks, int paddockArea)
        {
            int[][] paddockDimensions = new int[noOfPaddocks][];
            int paddockLength = 0;
            int paddockWidth = 0;

            if (Length % 3 == 0 || Width % 3 == 0)
            {
                paddockLength = Length % 3 == 0 ? Length : Width;
                paddockWidth = paddockArea / paddockLength;
            }
            else if (Length % 2 == 0 || Width % 2 == 0)
            {
                paddockLength = Length % 2 == 0 ? Length : Width;
                paddockWidth = paddockArea / paddockLength;
            }
            else
            {
                while (!((paddockLength % 2 == 0 && paddockWidth % 3 == 0) ||
                         (paddockWidth % 2 == 0 && paddockLength % 3 == 0)) ||
                         (paddockLength * paddockWidth != paddockArea))
                {
                    paddockLength--;
                    paddockWidth = paddockArea / paddockLength;
                }
            }

            for (int i = 0; i < noOfPaddocks; i++)
            {
                paddockDimensions[i] = new int[] { paddockLength, paddockWidth };
            }

            return paddockDimensions;
        }

        private double[][][] CalculatePaddockCoordinates(int noOfPaddocks, int[][] paddockDimensions)
        {
            double[][][] paddockCoordinates = new double[noOfPaddocks][][];
            double x = 0, y = 0;

            for (int i = 0; i < noOfPaddocks; i++)
            {
                double width = paddockDimensions[i][0];
                double height = paddockDimensions[i][1];

                if (x + width > Length)
                {
                    x = 0;
                    y += height;
                }

                if (y + height > Width)
                {
                    break;
                }

                paddockCoordinates[i] = new double[][]
                {
                    new double[] { x, y },
                    new double[] { x + width, y },
                    new double[] { x + width, y + height },
                    new double[] { x, y + height }
                };

                x += width;
            }

            return paddockCoordinates;
        }

        public void MoveCows(char[] fromPaddock, char[] toPaddock, int numberOfCows)
        {
            var fromPaddockObj = Paddocks.FirstOrDefault(p => p.Name.SequenceEqual(fromPaddock));
            var toPaddockObj = Paddocks.FirstOrDefault(p => p.Name.SequenceEqual(toPaddock));

            if (fromPaddockObj != null && toPaddockObj != null &&
                fromPaddockObj.CowCount >= numberOfCows)
            {
                fromPaddockObj.CowCount -= numberOfCows;
                toPaddockObj.CowCount += numberOfCows;
            }
        }

        public void SellCows(char[] paddock, int numberOfCows)
        {
            var paddockObj = Paddocks.FirstOrDefault(p => p.Name.SequenceEqual(paddock));

            if (paddockObj != null && paddockObj.CowCount >= numberOfCows)
            {
                paddockObj.CowCount -= numberOfCows;
            }
        }

        public void RestockCows(char[] paddock, int numberOfCows)
        {
            var paddockObj = Paddocks.FirstOrDefault(p => p.Name.SequenceEqual(paddock));

            if (paddockObj != null)
            {
                paddockObj.CowCount += numberOfCows;
            }
        }
    }
}
