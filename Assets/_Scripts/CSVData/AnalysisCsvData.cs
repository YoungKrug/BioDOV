using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Accord.Math;
using Accord.Math.Decompositions;
using Accord.Statistics.Analysis;
using Accord.Statistics.Kernels;
using UnityEngine;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using Codice.Client.Common.TreeGrouper;
using MathNet.Numerics.LinearAlgebra.Double;
using Matrix = Accord.Math.Matrix;

namespace _Scripts.CSVData
{
    public class AnalysisCsvData
    {
        public string path = "Assets/SimulationFiles/";
        // I have to sift through the data and look for changes in iteration. Essentially, if A(x) changes in
        // I(Iteration), and B(x) also changes from the I -> I - 1, how does that affect the data.
        //Did B(x) affect A(x) or is there another variable that changes it. 
        //Going to look through all of it to find out similarities
        //Ended up using a correlation function, the Pearson Correlation Coefficient Test. 
        //May run a meta-analysis in the future
        // public void CorrelationMatrix(Csv csv)
        // {
        //     int count = csv.Data.Count;
        //     for (int i = 0; i < count; i++) //O(N^3) Needs to be reduced to O(N^2) or O(NLogN) if possible
        //     {
        //         CsvNodes currentNode = csv.Data[i];
        //         List<double> currentNodeStates = currentNode.States;
        //         for (int j = 0; j < count; j++)
        //         {
        //             if(i == j)
        //                 continue;
        //             CsvNodes otherNode = csv.Data[j];
        //             List<double> otherNodeStates = otherNode.States;
        //             double correlation = Correlation.Pearson(currentNodeStates, otherNodeStates);
        //             //double spearmans = Correlation.Spearman(currentNodeStates, otherNodeStates);
        //             if (Double.IsNaN(correlation))
        //                 correlation = 0;
        //             currentNode.CasualtyInformationList.Add(new CasualtyInformation
        //             {
        //                 Name = otherNode.Name,
        //                 CasualtyPercent =  correlation
        //             });
        //         }
        //         
        //     }
        // }
        //
        // public void CorrespondingAnalysis(Csv csv)
        // {
        //     //You could use neural networks, (we dont have many data points)
        //     //input(t) = state(t) t = time
        //     /*
        //      *Categorize the data into the number of times certain states occur (Ex ->  
        //      *
        //      * Orginal Values ->
        //      *         None      Low         High       Avg
        //      * GCDE44  4          5            3        4
        //      *
        //      * And we would do this for all variables,
        //      * Then transform the data using
        //      * Column Avg -> cA
        //      * Row Avg -> rA
        //      * Overall Average -> oA (This is the average of all the averages)
        //      *
        //      * ExpectedValue = (rA * cA)/oA (New value) this is for each cell
        //      * Calculate the residuals
        //      * OriginalValue - ExpectedValue = residual
        //      */
        //     //  Frequencies Calculated ->
        //     StringBuilder stringBuilder = new StringBuilder();
        //     SortedSet<int> numbersContained = new SortedSet<int>();
        //     Dictionary<string,  SortedDictionary<int, int>> numberFrequency = new Dictionary<string,  SortedDictionary<int, int>>();
        //     double totalAmount = 0;
        //     foreach (var node in csv.Data)
        //     {
        //         SortedDictionary<int, int> numberCounter = new SortedDictionary<int, int>();
        //         foreach(var state in node.States)
        //         {
        //             int key = (int)state; // This is going to causes issues later
        //             numbersContained.Add(key);
        //             if (!numberCounter.ContainsKey(key))
        //             {
        //                 numberCounter.Add(key, 1);
        //             }
        //             numberCounter[key]++;
        //             totalAmount += 1;
        //         }
        //         
        //         if (!numberFrequency.ContainsKey(node.Name))
        //         {
        //             numberFrequency.Add(node.Name, numberCounter);
        //         }
        //     }
        //
        //     // Calculating the Observed Proportions and Row/Column Masses
        //
        //     int xCount = numbersContained.Count; // columns
        //     int yCount = numberFrequency.Count; // rows
        //     
        //     Matrix<double>  observedProportions = new Matrix<double>(xCount,yCount);
        //     double[] columnMass = new double[xCount];
        //     double[] rowMass = new double[yCount];
        //     for (int i = 0; i < yCount; i++)
        //     {
        //         foreach (var key in numbersContained)
        //         {
        //             if (!numberFrequency.ElementAt(i).Value.ContainsKey(key))
        //             {
        //                 numberFrequency.ElementAt(i).Value.Add(key, 0);
        //             }
        //         }
        //
        //         double rowVal = 0;
        //         for (int j = 0; j < xCount; j++)
        //         {
        //             int val = numberFrequency.ElementAt(i).Value[j];
        //             double value = (double)val/(double)totalAmount;
        //             observedProportions.MatrixData[i, j] = value;
        //             rowVal += value;
        //             columnMass[j] += value;
        //         }
        //         rowMass[i] = rowVal;
        //     }
        //
        //     CreateTextFile(observedProportions.ToString(), "ObservedProportions");
        //     
        //     // Calculating Expected Proportions Rmass(i) * columnMass(i)
        //     Matrix<double> expectedProportions = new Matrix<double>(xCount, yCount);
        //
        //     for (int i = 0; i < yCount; i++)
        //     {
        //         for (int j = 0; j < xCount; j++)
        //         {
        //             double massOfColumn = columnMass[j];
        //             double massOfRow = rowMass[i];
        //             expectedProportions.MatrixData[i, j] = massOfColumn * massOfRow;
        //         }
        //     }
        //     
        //     //Residual Calculation
        //     //Residual Table = Observed Proportion - Expected Proportion
        //     //Created a Indexed Residual 
        //     Matrix<double> residualMatrix = new Matrix<double>(xCount, yCount); 
        //     for (int i = 0; i < yCount; i++)
        //     {
        //         for (int j = 0; j < xCount; j++)
        //         {
        //             double observedProportionValue = observedProportions.MatrixData[i, j];
        //             double expectedProportionValue = expectedProportions.MatrixData[i, j];
        //             residualMatrix.MatrixData[i, j] = (observedProportionValue - expectedProportionValue)/expectedProportionValue;
        //         }
        //     }
        //     
        //     CreateTextFile(residualMatrix.ToString(), "ResidualMatrix_Indexed");
        //     
        //     //Calculate Standarized residual
        //     Matrix<double> standardizedResidual = new Matrix<double>(xCount, yCount);
        //     for (int i = 0; i < yCount; i++)
        //     {
        //         for (int j = 0; j < xCount; j++)
        //         {
        //             double indexResidualVal = residualMatrix.MatrixData[i, j];
        //             double sqrtOfExpected = Math.Sqrt(expectedProportions.MatrixData[i, j]);
        //             standardizedResidual.MatrixData[i, j] = indexResidualVal * sqrtOfExpected;
        //         }
        //     }
        //
        //     var matrixBuilder = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build;
        //     var modifiedMatrixOfResiduals = matrixBuilder.DenseOfArray(standardizedResidual.MatrixData);
        //     
        //     var svdOfResiduals = modifiedMatrixOfResiduals.Svd();
        //     // U = left singular vectors
        //     // S = singular Values
        //     // VT = transpose of right singular vectors
        //     //Calculating the Singular Value Decomposition (SVD)
        //     // standardized Residual(Z) = Indexed Residual(I) * sqrt(Expected Proportions)
        //     Matrix<double> leftSingularValues = new Matrix<double>(svdOfResiduals.U.ColumnCount,
        //         svdOfResiduals.U.RowCount); // / sqrt(RowMasses)
        //     Matrix<double> rightSingularValues = new Matrix<double>(svdOfResiduals.VT.ColumnCount,
        //         svdOfResiduals.VT.RowCount);//  /sqrt(columnMasses)
        //     List<double> singularValues = new List<double>(); 
        //     leftSingularValues.MatrixData = svdOfResiduals.U.ToArray();
        //     rightSingularValues.MatrixData = svdOfResiduals.VT.ToArray();
        //     singularValues = svdOfResiduals.S.ToArray().ToList();
        //     int colCountU = svdOfResiduals.U.ColumnCount;
        //     int rowCountU = svdOfResiduals.U.RowCount;
        //     int colCountVT = svdOfResiduals.VT.ColumnCount;
        //     int rowCountVT = svdOfResiduals.VT.RowCount;
        //     // Getting coordinates for left singular
        //     for (int i = 0; i < rowCountU; i++)
        //     {
        //         for (int j = 0; j < colCountU; j++)
        //         {
        //             leftSingularValues.MatrixData[i, j] /= (Math.Sqrt(rowMass[i]));
        //         }
        //     }
        //     CreateTextFile(leftSingularValues.ToString(), "LeftSingularValues Matrix");
        //     //Coordinates for right singular
        //     for (int i = 0; i < rowCountVT; i++)
        //     {
        //         for (int j = 0; j < colCountVT; j++)
        //         {
        //             rightSingularValues.MatrixData[i, j] /= (Math.Sqrt(columnMass[j]));
        //         }
        //     }
        //     CreateTextFile(rightSingularValues.ToString(), "RightSingularValue Matrix");
        //
        //
        //     for (int i = 0; i < csv.Data.Count; i++)
        //     {
        //         float x = (float)leftSingularValues.MatrixData[i, 0];
        //         float y = (float)leftSingularValues.MatrixData[i, 1];
        //         csv.Data[i].Coords = new Vector2(x, y);
        //         Debug.Log($"{csv.Data[i].Name} is at: {csv.Data[i].Coords.ToString()}");
        //     }
        // }

        public void Test()
        {
            // Sample input feature data
            double[][] inputs = new double[][]
            {
                new double[] { 1.2, 3.4, 5.6, 7.8 },
                new double[] { 2.3, 4.5, 6.7, 8.9 },
                new double[] { 3.4, 5.6, 7.8, 9.0 },
                new double[] { 4.5, 6.7, 8.9, 1.2 },
                new double[] { 5.6, 7.8, 9.0, 2.3 }
            };

            // Sample target variable data
            double[][] outputs = new double[][]
            {
                new double[] { 10 },
                new double[] { 15 },
                new double[] { 20 },
                new double[] { 25 },
                new double[] { 30 }
            };

            // Number of latent components (adjust this based on your preference)
            int numberOfComponents = 2;

            // Create the Partial Least Squares (PLS) analysis
            PartialLeastSquaresAnalysis pls = new PartialLeastSquaresAnalysis();

            // Learn the PLS model with the input data and the target variable
            pls.Learn(inputs, outputs);

            // Assuming 'newSampleChanges' contains the new feature vector with changed states
            double[] newSampleChanges = new double[] { 6.0, 7.0, 8.0, 9.0 };

            // Normalize the feature vector and make predictions
            double[][] newSampleInput = new double[][] { newSampleChanges };
            double[][] predictions = pls.Transform(newSampleInput);

            double prediction = predictions[0][0];

            Debug.Log("Predicted Y value: " + prediction);
        }
        public void PartialLeastSquareRegression(Csv csv)
        {
              //   /*
              //    * Has to be used to predict,
              //    * Based on this state, and this state of this gene, how is this gene effected
              //    * Column names are the features **/
              //Test();
             // return;
             string target = "CCL5";
             int targetIndex = 0;
             int index = csv.Data.FindIndex(x => x.Name == target);
              // Your input feature data as a 2D array
             List<double> targetList = new List<double>();
             int statesCount = csv.Data[0].States.Count;
             int dataCount = csv.Data.Count;
             double[][] inputList = new double[statesCount][];
             for (int i = 0; i < statesCount; i++) //row
             {
                 int indexNum = 0;
                 double[] data = new double[dataCount - 1];
                 for (int j = 0; j < dataCount; j++) //column
                 {
                     double val = csv.Data[j].States[i];
                     if (csv.Data[j].Name == target)
                     {
                         targetList.Add(val);
                         continue;
                     }
                     data[indexNum++] = val;
                 }
                 inputList[i] = data;
             }

             
             double[][] inputs = inputList.Select(row => row.Take(row.Length - 1).ToArray()).ToArray();
             double[][] outputs = new double[statesCount][];
             for (int i = 0; i < statesCount; i++)
             {
                 outputs[i] = new double[] { targetList[i] };
             }
            // double[][] outputs = new double[targetList.Count][];  // Your target variable data as a 1D array
             // Number of latent components (similar to the 'n_components' in Python's PLS model)
             int numberOfComponents = 4; // Adjust this value based on your preference or model evaluation
          
             // Create the PLS model and specify the number of latent components
          
            PartialLeastSquaresAnalysis pls = new PartialLeastSquaresAnalysis  {
                Method = AnalysisMethod.Center,
                Algorithm = PartialLeastSquaresAlgorithm.NIPALS
            };
            var dataR = pls.Learn(inputs, outputs);
            double[] newSampleChanges = new double[] { 2, 1, 0, 0, 0, 1, 1, 0, 0, 1, 0 };

            // Normalize the feature vector and make predictions
            double[][] newSampleInput = new double[][] { newSampleChanges };
            double[][] predictions = pls.Transform(newSampleInput);
            // Fit the model to the data
            //              pls.Learn(reducedInputs,);
            //
// // Assuming 'newSampleChanges' contains the new feature vector with changed states
//              double[] newSampleChanges = new double[] { 2, 0, /*...and so on*/ };
//
// // Reduce the new sample to the same number of components as used in training
//              double[] newSampleReduced = newSampleChanges.Dot(truncatedSingularVectors).Dot(truncatedDiagonalMatrix);
//
// // Normalize the feature vector and make predictions
//              double[][] predictions = pls.Transform(new double[][] { newSampleReduced });
//
// // Extract the prediction for 'CCL5' (assuming it's the first target variable)
//              double ccl5Prediction = predictions[0][0];

        }
        public void CreateTextFile(string text, string fileName)
        {
            string file = $"{path}/{fileName}.csv";
            using (FileStream fs = File.Create(file))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(text);
                fs.Write(info, 0, info.Length);
            }
        }
    }
}