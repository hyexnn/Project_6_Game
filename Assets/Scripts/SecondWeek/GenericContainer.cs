using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericContainer<T>
{
    private T[] items;
    private int currentIdex = 0;
    public GenericContainer(int capacity)
    {
        items = new T[capacity];
    }
    public void Add(T item)
    {
        if (currentIdex < items.Length)
        {
            items[currentIdex] = item;
            currentIdex++;
        }
    }
    public T[] GetItems()
    {
        return items;
    }
    
}
