using System;
using UnityEngine;

namespace GameUtils
{
    [Serializable]
    public class Array2D<T>
    {
        [SerializeField] private T[] flatArray;
        [SerializeField] private int rows;
        [SerializeField] private int columns;

        public int Rows => rows;
        public int Columns => columns;

        public Array2D(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            flatArray = new T[rows * columns];
        }

        public T this[int row, int col]
        {
            get => flatArray[row * columns + col];
            set => flatArray[row * columns + col] = value;
        }

        public T[] GetFlatArray() => flatArray;

        public void Resize(int newRows, int newColumns)
        {
            var oldArray = flatArray;
            var oldRows = rows;
            var oldColumns = columns;

            rows = newRows;
            columns = newColumns;
            flatArray = new T[rows * columns];

            for (int i = 0; i < Mathf.Min(oldRows, newRows); i++)
            {
                for (int j = 0; j < Mathf.Min(oldColumns, newColumns); j++)
                {
                    this[i, j] = oldArray[i * oldColumns + j];
                }
            }
        }
    }
}
