using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T:IHeapitem<T> {
    T[] items;

    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
        currentItemCount = 0;       
    }

    public void Add (T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
        SortDown(item);
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex]);
    }

    public T RemoveFirstItem()
    {
        T firstItem = items[0];

        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;

        SortDown(items[0]);


        return firstItem;
    }

    void SortDown(T item)
    {
        int childIndexLeft = item.HeapIndex * 2 + 1;
        int childIndexRight = item.HeapIndex * 2 + 2;
        int swapIndex = 0;

        if (childIndexLeft < currentItemCount)
        {
            swapIndex = childIndexLeft;

            if (childIndexRight < currentItemCount)
            {
                if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                {
                    swapIndex = childIndexRight;
                }
            }

            if (item.CompareTo(items[swapIndex]) < 0)
            {
                Swap(item, items[swapIndex]);
            }
            else
            {
                return;
            }
        }

        else
        {
            return;
        }

    }


    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }

            else
            {
                break;
            }
        }
    }

    void Swap (T item1, T item2)
    {
        items[item1.HeapIndex] = item2;
        items[item2.HeapIndex] = item1;

        int itemAindex = item1.HeapIndex;
        item1.HeapIndex = item2.HeapIndex;
        item2.HeapIndex = itemAindex;
    }
}


public interface IHeapitem<T> : IComparable<T>
{
    int HeapIndex { get; set; }

}
