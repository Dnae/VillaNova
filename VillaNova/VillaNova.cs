using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaNova
{
    public class VillaNova
    {
        Island island;
        int panelWidth, panelHeight, tileWidth, tileHeight;

        int px = 0;
        int py = 0;

        Image[] groundImages = new Image[3];
        Image[,] playerImages = new Image[2,4];

        public VillaNova()
        {
            island = new Island();
            this.panelWidth = Properties.Settings.Default.Width;
            this.panelHeight = Properties.Settings.Default.Height;
            tileWidth = Properties.Settings.Default.TileWidth;
            tileHeight = Properties.Settings.Default.TileHeight;

            int x = 0;
            int y = 0;
            island.GetSpawn(out x, out y);
            px = x * tileWidth + tileWidth / 2;
            py = y * tileHeight + tileHeight / 2;

            LoadImages();
        }

        public void MovePlayer(int x, int y)
        {
            this.px = px + x;
            this.py = py + y;
        }

        private void LoadImages()
        {
            // GROUND
            for(int i = 0; i < groundImages.Length; i++)
            {
                groundImages[i] = ImageHelper.GetGround("g" + i);
            }

            // PLAYER
            for (int i = 0; i < playerImages.GetLength(0); i++)
            {
                for(int j = 0; j < playerImages.GetLength(1); j++)
                {
                    playerImages[i,j] = ImageHelper.GetPlayer($"p{i}{j}");
                }
            }
        }

        public Bitmap GetImage()
        {
            Bitmap bmp = new Bitmap(panelWidth, panelHeight);
            
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                //g.SmoothingMode = SmoothingMode.None;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                //g.CompositingQuality = CompositingQuality.HighQuality;
                for (int i = 0; i * tileWidth < panelWidth + panelWidth; i++)
                {
                    for (int j = 0; j * tileHeight < panelHeight + panelHeight; j++)
                    {
                        if (true)
                        {
                            int x = ((px - panelWidth/2) + i * tileWidth) / tileWidth;
                            int y = ((py - panelHeight/2) + (j - 2) * tileHeight) / tileHeight;

                            int offsetx = (px % tileWidth) * -1;
                            int offsety = (py % tileHeight) * -1;

                            if (x >= 0 && y >= 0 && x < island.Tiles.GetLength(0) && y < island.Tiles.GetLength(1)) {
                                int val = this.island.Tiles[x, y];
                                if (val < 3)
                                {
                                    g.DrawImage(groundImages[this.island.Tiles[x, y]], offsetx + i * tileWidth, offsety + j * tileHeight, tileWidth, tileHeight);
                                }
                            }
                            else
                            {
                                g.DrawImage(groundImages[0], offsetx + i * tileWidth, offsety + j * tileHeight, tileWidth, tileHeight);
                            }
                        }
                    }
                }

                int playerx = px / tileWidth;
                int playery = py / tileHeight;

                int pimg = 0;
                if (playerx >= 0 && playery >= 0 && playerx < island.Tiles.GetLength(0) && playery < island.Tiles.GetLength(1))
                {
                    int val = this.island.Tiles[playerx, playery];
                    switch (val)
                    {
                        case 0:
                            pimg = 0;
                            break;
                        case 1:
                            pimg = 1;
                            break;
                        case 2:
                            pimg = 1;
                            break;
                        default:
                            break;
                    }
                }

                g.DrawImage(playerImages[pimg,0], (panelWidth - tileWidth) / 2, panelHeight / 2 - tileHeight + 2, tileWidth, tileHeight * 3);
            }

            return bmp;
        }
    }
}
