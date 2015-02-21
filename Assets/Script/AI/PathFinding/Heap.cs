using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;//to get a timer

public class Heap<T> where T: IHeapItem<T>
{

    #region class variables and properties

    private T[] items;
    private int currentItemCount;

    public int CurrentItemCount
    {
        get { return currentItemCount; }
        set { currentItemCount = value; }
    }

    public int Count
    {
        get { return currentItemCount; }
    }

    #endregion


    #region constructor

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
        

    }


    #endregion

    #region Heap functions


    /// <summary>
    /// each item must keep track of its position in the array
    /// </summary>
    /// <param name="item"></param>
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public T RemoveFirstItem()
    {
        T firstItem = items[0];
        currentItemCount --;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown((items[0]));
        return firstItem;
    }

    void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = (2*item.HeapIndex) + 1;

            int childIndexRight = (2*item.HeapIndex) + 2;
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
                    return;

            }
            else
            {
                return;
            }
        }
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex],item);
    }
    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1)/2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
                //if item has a higher priority than parent item then CompareTo returns 1, if its lower then return -1, else if its the same then return 0
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
        }

    }


    void Swap(T itemA, T ItemB )
    {
        items[itemA.HeapIndex] = ItemB;
        items[ItemB.HeapIndex] = itemA;
        int tempItemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = ItemB.HeapIndex;
        ItemB.HeapIndex = tempItemAIndex;

    }
    #endregion

}

public interface IHeapItem<T>:IComparable 
{
    int HeapIndex{get; set; }
}