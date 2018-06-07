using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaNova
{
    class Island
    {
        int[,] tiles = new int[80, 80];

        public int[,] Tiles { get { return tiles; } set { tiles = value; } }

        Random r = new Random();

        public Island()
        {
            PlotRandom();
            PlotHouses();
            FinalizeMap();
            ExportMap();
        }

        private void ExportMap()
        {
            Bitmap bmp = new Bitmap(80, 80);

            for(int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    switch(tiles[i, j])
                    {
                        case 0:
                            bmp.SetPixel(i, j, Color.Blue);
                            break;
                        case 1:
                            bmp.SetPixel(i, j, Color.Green);
                            break;
                        case 2:
                            bmp.SetPixel(i, j, Color.Gray);
                            break;
                        default:
                            bmp.SetPixel(i, j, Color.Black);
                            break;

                    }
                }
            }

            bmp.Save("map.png");
        }

        private void FinalizeMap()
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] == 1)
                    {
                        if (HasAdjacent(i, j, 1) || HasAdjacent(i, j, 2))
                        {
                            if (HasAdjacent(i, j, 0))
                            {
                                tiles[i, j] = 2;
                            }
                        }
                        else
                        {
                            tiles[i, j] = 0;
                        }
                    }
                }
            }
        }

        int landmass = 0;
        private void PlotRandom()
        {
            for (int i = 0; i < 1200; i++)
            {
                if (landmass < 3000)
                {
                    DrawTile(68, 68);
                    landmass++;
                }
                else
                {
                    break;
                }
            }
        }

        private void DrawTile(int mx, int my)
        {
            int rx = r.Next(mx) + 5;
            int ry = r.Next(my) + 5;

            if (tiles[rx, ry] == 1)
            {

                for (int i = 0; i < 15; i++)
                {
                    DrawAdjacentTile(rx, ry);
                }
            }

            int paint = 0;
            if (!HasAdjacent(rx, ry, 1))
            {
                paint = r.Next(5);
            }

            if (paint == 0)
            {
                UpdateTile(rx, ry, 1);
            }

        }

        private void DrawAdjacentTile(int rx, int ry)
        {

            rx = rx + r.Next(3) - 1;
            ry = ry + r.Next(3) - 1;

            if (ry > 0 && rx > 0 && rx < 79 && ry < 79)
            {
                if (tiles[rx, ry] == 1)
                {
                    DrawAdjacentTile(rx, ry);
                }
                else
                {
                    UpdateTile(rx, ry, 1);
                }
            }
        }

        private void UpdateTile(int x, int y, int ground)
        {
            landmass++;
            tiles[x, y] = ground;
        }

        private bool HasAdjacent(int rx, int ry, int type)
        {

            if (tiles[rx + 1, ry] == type)
            {
                return true;
            }
            else if (tiles[rx - 1, ry] == type)
            {
                return true;
            }
            else if (tiles[rx, ry + 1] == type)
            {
                return true;
            }
            else if (tiles[rx, ry - 1] == type)
            {
                return true;
            }

            return false;

        }

        private void PlotHouses()
        {
            for (int i = 0; i < 5; i++)
            {
                PlotHouse();
            }
        }

        private void PlotHouse()
        {
            int sx = r.Next(tiles.GetLength(0) - 5);
            int sy = r.Next(tiles.GetLength(1) - 5);

            bool validhouse = true;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {

                    if (tiles[sx + i, sy + j] != 1 || tiles[sx + i, sy + j] == 3 || HasAdjacent(sx + i, sy + j, 0) || HasAdjacent(sx + i, sy + j, 3))
                    {
                        validhouse = false;
                        break;
                    }
                }
            }

            if (validhouse)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        tiles[sx + i, sy + j] = 3;
                    }
                }
            }
            else
            {
                PlotHouse();
            }
        }

        public void GetSpawn(out int x, out int y)
        {
            

            int sx = r.Next(tiles.GetLength(0));
            int sy = r.Next(tiles.GetLength(1));

            if (tiles[sx, sy] == 2 && HasAdjacent(sx, sy, 0))
            {
                x = sx;
                y = sy;
            }
            else
            {
                GetSpawn(out x, out y);
            }
        }
    }
}
