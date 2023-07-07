using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathNet.Numerics.Statistics;

namespace _Scripts.CSVData
{
    public class AnalysisCsvData
    {
        // I have to sift through the data and look for changes in iteration. Essentially, if A(x) changes in
        // I(Iteration), and B(x) also changes from the I -> I - 1, how does that affect the data.
        //Did B(x) affect A(x) or is there another variable that changes it. 
        //Going to look through all of it to find out similarities
        //Ended up using a correlation function, the Pearson Correlation Coefficient Test. 
        //May run a meta-analysis in the future
        public void CorrelationMatrix(Csv csv)
        {
            int count = csv.Data.Count;
            for (int i = 0; i < count; i++) //O(N^3) Needs to be reduced to O(N^2) or O(NLogN) if possible
            {
                CsvNodes currentNode = csv.Data[i];
                List<double> currentNodeStates = currentNode.States;
                for (int j = 0; j < count; j++)
                {
                    if(i == j)
                        continue;
                    CsvNodes otherNode = csv.Data[j];
                    List<double> otherNodeStates = otherNode.States;
                    double correlation = Correlation.Pearson(currentNodeStates, otherNodeStates);
                    //double spearmans = Correlation.Spearman(currentNodeStates, otherNodeStates);
                    if (Double.IsNaN(correlation))
                        correlation = 0;
                    currentNode.CasualtyInformationList.Add(new CasualtyInformation
                    {
                        Name = otherNode.Name,
                        CasualtyPercent =  correlation
                    });
                }
                
            }
        }
    }
}