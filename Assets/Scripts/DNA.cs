using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    List<int> genes = new List<int>();
    int dnaLength = 0;
    int maxValues = 0;

    public DNA(int l, int v)
    {
        this.dnaLength = l;
        this.maxValues = v;
        SetRandom();
    }

    public void SetRandom()
    {
        this.genes.Clear();
        for(int i = 0; i < this.dnaLength; i++)
        {
            this.genes.Add(Random.Range(0, this.maxValues));
        }
    }

    public void SetInt(int index, int value)
    {
        this.genes[index] = value;
    }

    public void Combine(DNA d1, DNA d2)
    {
        for(int i = 0; i < this.dnaLength; i++)
        {
            if(i < this.dnaLength / 2.0f)
            {
                int c = d1.genes[i];
                this.genes[i] = c;
            }
            else
            {
                int c = d2.genes[i];
                this.genes[i] = c;
            }
        }
    }

    public void Mutate()
    {
        this.genes[Random.Range(0, this.dnaLength)] = Random.Range(0, this.maxValues);
    }

    public int GetGene(int index)
    {
        return this.genes[index];
    }
}
