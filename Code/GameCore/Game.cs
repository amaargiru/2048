﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore;

public enum Direction
{
    Up,
    Down,
    Right,
    Left
}

public static class Game
{
    private static readonly Random random = new();

    // Генерация начального состояния игрового поля
    public static GameState Init(int rows, int cols, int digitsOnNewBoard)
    {
        if (digitsOnNewBoard > rows * cols)
            throw new ArgumentOutOfRangeException
            ("Число ячеек игрового поля, которые должны быть заполнены в начале игры, больше, чем общее число ячеек." +
             $" Сейчас {rows} (колонки) * {cols} (столбцы) < {digitsOnNewBoard} (ненулевые ячейки).");

        if (rows < 2 || cols < 2)
            throw new ArgumentOutOfRangeException("Число строк и столбцов создаваемого игрового поля д. б. > 2.");

        var initState = new GameState
        {
            Board = new ulong[rows, cols],
            Score = 0,
            IsGameOver = false
        };

        // Добавляем на пустое игровое поле ячейки с цифрами
        initState.Board = AddNewRandomSlots(initState.Board, digitsOnNewBoard);

        return initState;
    }

    // Вычисление следующей конфигурации игрового поля после хода игрока
    public static GameState NextState(GameState inputState, Direction? direction, int newSlots)
    {
        if (newSlots > inputState.Board.GetLength(0) * inputState.Board.GetLength(1))
            throw new ArgumentOutOfRangeException
            ("Число ячеек игрового поля, которые должны быть заполнены, больше, чем общее число ячеек." +
             $" Сейчас {inputState.Board.GetLength(0)} (колонки) * {inputState.Board.GetLength(1)} (столбцы)" +
             $"< {newSlots} (новые ячейки).");

        var nextState = Update(inputState, direction);

        // Если ни одно движение не приводит к изменениям, то игра закончена
        if (!TryToMove(nextState.Board))
        {
            nextState.IsGameOver = true;
            return nextState;
        }

        // Возможна ситуация, когда свободных ячеек меньше, чем количество
        // новых ячеек, которые мы желаем добавить
        var freeSlots = nextState.Board.Length - Utility.Calculate2DArrayNonZeroValues(nextState.Board);
        var addedSlots = Math.Min(newSlots, freeSlots);
        AddNewRandomSlots(nextState.Board, addedSlots);

        return nextState;
    }

    private static GameState Update(GameState inputState, Direction? direction)
    {
        var updateState = new GameState
        {
            Board = (ulong[,])inputState.Board.Clone(),
            Score = inputState.Score,
            IsGameOver = false
        };

        var isHorizontalMove = direction == Direction.Left || direction == Direction.Right;
        var isIncreasing = direction == Direction.Left || direction == Direction.Up;

        var maxRow = inputState.Board.GetLength(0);
        var maxCol = inputState.Board.GetLength(1);
        // В зависимости от направления движения нам понадобится только maxRow или maxCol
        var Lines = isHorizontalMove ? maxRow : maxCol;
        var maxNumInLine = isHorizontalMove ? maxCol : maxRow;

        // Выделяем из двумерного массива одну строку или столбец
        var extractLine = isHorizontalMove
            ? new Func<ulong[,], int, ulong[]>((board, num) => Enumerable.Range(0, maxCol)
                .Select(x => board[num, x]).ToArray())
            : (board, num) => Enumerable.Range(0, maxRow)
                .Select(x => board[x, num]).ToArray();

        var condition = isIncreasing ? new Func<int, int, bool>((a, b) => a < b - 1) : (a, b) => a >= 0;
        var change = isIncreasing ? new Func<int, int>(j => j + 1) : j => j - 1;

        // Сдвиг всех ненулевых элементов массива к началу или к концу
        var shift = isIncreasing
            ? new Func<ulong[], ulong[]>(a => a.OrderBy(x => x == 0).ToArray())
            : a => a.OrderByDescending(x => x == 0).ToArray();

        // Для вставки строки используем быстрый метод Buffer.BlockCopy, а для вставки столбца - цикл
        var insertLine = isHorizontalMove
            ? new Action<ulong[,], ulong[], int>((board, row, i) =>
                Buffer.BlockCopy(row, 0, board, i * maxCol * sizeof(ulong), maxNumInLine * sizeof(ulong)))
            : (board, col, i) =>
            {
                for (var j = 0; j < maxNumInLine; j++) board[j, i] = col[j];
            };

        var line = new ulong[maxNumInLine];

        // Перебор строк или столбцов (в зависимости от хода игрока)
        for (var i = 0; i < Lines; i++)
        {
            // Выделяем одномерный массив из общего двумерного
            line = extractLine(updateState.Board, i);

            // Сдвигаем все ненулевые элементы
            line = shift(line);

            // Суммируем одинаковые значения и вычисляем заработанные очки
            for (var j = isIncreasing ? 0 : maxNumInLine - 2; condition(j, maxNumInLine); j = change(j))
                if (line[j] != 0 && line[j] == line[j + 1])
                {
                    line[j] *= 2;
                    line[j + 1] = 0;

                    updateState.Score += line[j];

                    if (!isIncreasing)
                        j--;
                }

            // Убираем нулевые значения, которые могли остаться между просуммированными парами
            line = shift(line);

            // Вставляем обработанный одномерный массив на свое место в общий двумерный массив
            insertLine(updateState.Board, line, i);
        }

        return updateState;
    }

    // Добавление новых ячеек на игровое поле
    private static ulong[,] AddNewRandomSlots(ulong[,] board, int newDigits)
    {
        // Список всех свободных позиций
        var emptySlots = new List<(int x, int y)>();

        for (var x = 0; x < board.GetLength(0); x++)
            for (var y = 0; y < board.GetLength(1); y++)
                if (board[x, y] == 0)
                    emptySlots.Add((x, y));

        for (var i = 0; i < newDigits; i++)
        {
            var r = random.Next(emptySlots.Count);

            // Новая ячейка, появляющаяся на игровом поле после очередного хода,
            // должна быть двойкой с вероятностью 90 % или четверкой с вероятностью 10 %
            board[emptySlots[r].x, emptySlots[r].y] = random.NextDouble() < 90.0 / 100.0 ? 2ul : 4ul;

            emptySlots.RemoveAt(r);
        }

        return board;
    }

    private static bool TryToMove(ulong[,] board)
    {
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            var cloneIn = new GameState
            {
                Board = (ulong[,])board.Clone()
            };

            var cloneOut = Update(cloneIn, dir);

            if (!Utility.CompareMultidimensionalArrays(cloneIn.Board, cloneOut.Board))
                return true;
        }

        return false;
    }
}