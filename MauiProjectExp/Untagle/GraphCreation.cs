namespace MauiProjectExp.Untagle
{
    public static class GraphCreation
    {
        //Вершины минус ребра плюс грани = 2  - формула связного плоского (планарного) графа
        
        public static int[,] CreateMatrix(int n) //Создаем граф с Н вершинами , задаем матрицей смежности размером Н на Н,заполняем матрицу нулями , а по главной диагонали -1 
        {
            int[,] a = new int[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        a[i, j] = -1;
                    else
                        a[i, j] = 0;
                }
            return a;
        }

        // Проверка на принадлежность к к33 и к5. Если принадлежит хоть одному из типов, граф не планарный
        public static bool IsPlanar(int[,] a, int n) => IsKFiveFull(a, n) != true && IsKThirtyThreeFull(a, n) != true;

        public static void RandomAll(int[,] a, int n)// Там где единицы в треугольной матрице генерируем случайные числа, это путь из вершины i в вершину j. Сгенерированный граф готов
        {
            RandomConnection(a, n);
            Duplicate(a, n);
        }

        private static void GenerateConnections(int[,] a, int n) // Справа от главной диагонали в трегольной матрице генерируем числа ноль или один
        {
            Random rnd = new();

            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    a[i, j] = rnd.Next(0, 2);
        }
        private static void CheckRow(int[,] a, int n, int k)// Проверяем есть ли в трегольной матрице справа , как минимум одна единица в строке, если нет то генерируем единицу в этой строке
        {
            bool haveOne = false;

            for (int i = k + 1; i < n; i++)
            {
                if (a[k, i] != 0)
                {
                    haveOne = true;
                    break;
                }
            }

            if (!haveOne)
            {
                Random rnt = new();
                int p = rnt.Next(k + 1, n);
                a[k, p] = 1;
            }

        }
        private static void CheckCollumn(int[,] a, int n, int k)// Проверяем есть ли в треугольной матрице справа, как минимум одна единица в столбце, если нет то генерируем единицу в этом столбце
        {
            bool haveOne = false;

            for (int i = 0; i < k; i++)
            {
                if (a[i, k] != 0)
                {
                    haveOne = true;
                    break;
                }
            }

            if (!haveOne)
            {
                Random rnt = new();
                int p = rnt.Next(0, k);
                a[p, k] = 1;
            }

        }
        private static void RandomConnection(int[,] a, int n)// Использвуем две проверки , описанные выше
        {
            GenerateConnections(a, n);
            for (int i = 0; i < n - 1; i++)
            {
                CheckRow(a, n, i);
            }
            for (int j = 1; j < n; j++)
                CheckCollumn(a, n, j);
        }
        private static void Duplicate(int[,] a, int n)// Отображаем правую трегольную матрицу влево от главной диагонали
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (a[i, j] == 1)
                        a[j, i] = 1;
        }
        private static bool IsKFive(int[,] a, int k1, int k2)// Проверка подграфа на граф k5 .к1 - Индекс строки, к2 - индекс столбца
        {
            bool b = false;
            int kol1 = 0;
            int kol2 = 0;
            for (int i = k1; i < (k1 + 5); i++)
            {
                kol1 = 0;
                for (int j = k2; j < (k2 + 5); j++)
                    if (a[i, j] != 0 && a[i, j] != -1)
                        kol1++;
                if (kol1 == 4)
                    kol2++;
            }
            if (kol2 == 5)
                b = true;
            return b;

        }
        private static bool IsKThirtyThree(int[,] a, int k1, int k2)// Проверка подграфа на граф k33 .к1 - Индекс строки, к2 - индекс столбца
        {
            bool b = false;
            int kol1 = 0;
            int kol2 = 0;
            for (int i = k1; i < (k1 + 6); i++)
            {
                kol1 = 0;
                for (int j = k2; j < (k2 + 6); j++)
                    if (a[i, j] != 0 && a[i, j] != -1)
                        kol1++;
                if (kol1 == 3)
                    kol2++;
            }
            if (kol2 == 6)
                b = true;
            return b;

        }
        private static bool IsKFiveFull(int[,] a, int n)// Полная проверка всевозможных подграфов ,сгенерированного графа на к5 
        {
            bool b = false;
            for (int i = 0; i < (n - 5); i++)
            {
                for (int j = 0; j < (n - 5); j++)

                    if (IsKFive(a, i, j) == true)
                    {

                        b = true;
                        break;
                    }
                if (b == true)
                    break;
            }
            return b;
        }
        private static bool IsKThirtyThreeFull(int[,] a, int n)// Полная проверка всевозможных подграфов ,сгенерированного графа на к33
        {
            bool b = false;
            for (int i = 0; i < (n - 5); i++)
            {
                for (int j = 0; j < (n - 5); j++)
                    if (IsKThirtyThree(a, i, j) == true)
                    {
                        b = true;
                        break;
                    }
                if (b == true)
                    break;
            }
            return b;
        }
    }
}
